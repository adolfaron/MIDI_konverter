using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text.Json;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Midi;

namespace MIDI_Konverter
{
    public partial class Form1 : Form
    {
        private MidiIn midiIn;
        private MidiOut midiOut;
        private SerialPort serialPort;

        private bool bekapcsolva = false;
        private byte[] serialBuffer = new byte[3];
        private int serialBufferIndex = 0;
        private DateTime startTime;

        private const int WM_DEVICECHANGE = 0x0219;

        private List<MMDevice> hangkimenetek = new List<MMDevice>();
        private Dictionary<int, int> _lastCCValues = new Dictionary<int, int>();

        private Dictionary<string, int> kemenetVezerloTar = new Dictionary<string, int>();
        private ConcurrentDictionary<int, int> utolsoCCErtek = new ConcurrentDictionary<int, int>();

        private System.Windows.Forms.Timer ccTimer;

        public Form1()
        {
            InitializeComponent();

            logBe.Checked = true;

            BeallitasKezelo.Load();
            bekapcsolva = BeallitasKezelo.bekapcsolva;
            kemenetVezerloTar = BeallitasKezelo.kemenetVezerloTar ?? new Dictionary<string, int>();

            MidiEszkozBetolt();
            ComEszkozBetolt();
            HangkimenetBetolt();
            FrissitVezereltKimenetekLista();

            FormClosing += (s, e) =>
            {
                if (midiBeVal.SelectedIndex > 0)
                    BeallitasKezelo.midiInIndex = midiBeVal.SelectedIndex;
                if (midiKiVal.SelectedIndex > 0)
                    BeallitasKezelo.midiOutIndex = midiKiVal.SelectedIndex;
                if (ComVal.SelectedIndex > 0)
                    BeallitasKezelo.comIndex = ComVal.SelectedIndex;

                BeallitasKezelo.kemenetVezerloTar = kemenetVezerloTar;
                BeallitasKezelo.bekapcsolva = bekapcsolva;
                BeallitasKezelo.Save();
            };

            ccTimer = new System.Windows.Forms.Timer();
            ccTimer.Interval = 50;
            ccTimer.Tick += Tick;
            ccTimer.Start();

            if (bekapcsolva)
                StartBridge();
        }

        private void Tick(object sender, EventArgs e)
        {
            if (utolsoCCErtek.Count == 0 || kemenetVezerloTar.Count == 0)
                return;

            foreach (var entry in kemenetVezerloTar)
            {
                string devName = entry.Key;
                int targetCC = entry.Value;

                if (targetCC == -1)
                    continue;

                if (!utolsoCCErtek.TryGetValue(targetCC, out int ccValue))
                    continue;

                if (_lastCCValues.TryGetValue(targetCC, out int lastValue) && lastValue == ccValue)
                    continue;

                _lastCCValues[targetCC] = ccValue;

                var device = hangkimenetek.FirstOrDefault(d => d.FriendlyName == devName);
                if (device == null)
                    continue;

                try
                {
                    float volume = ccValue / 127f;
                    device.AudioEndpointVolume.MasterVolumeLevelScalar = volume;
                    Log(device + " -> " + (int)(volume * 100));
                }
                catch { }
            }
        }

        private void MidiEszkozBetolt()
        {
            midiBeVal.Items.Clear();
            midiKiVal.Items.Clear();

            midiBeVal.Items.Add("Not Connected");
            midiKiVal.Items.Add("Not Connected");

            for (int i = 0; i < MidiIn.NumberOfDevices; i++)
                midiBeVal.Items.Add(MidiIn.DeviceInfo(i).ProductName);

            for (int i = 0; i < MidiOut.NumberOfDevices; i++)
                midiKiVal.Items.Add(MidiOut.DeviceInfo(i).ProductName);

            midiBeVal.SelectedIndex =
                (BeallitasKezelo.midiInIndex >= 0 && BeallitasKezelo.midiInIndex < midiBeVal.Items.Count)
                    ? BeallitasKezelo.midiInIndex
                    : 0;

            midiKiVal.SelectedIndex =
                (BeallitasKezelo.midiOutIndex >= 0 && BeallitasKezelo.midiOutIndex < midiKiVal.Items.Count)
                    ? BeallitasKezelo.midiOutIndex
                    : 0;
        }

        private void ComEszkozBetolt()
        {
            ComVal.Items.Clear();
            ComVal.Items.Add("Not Connected");

            try
            {
                var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%)%'");
                foreach (ManagementObject obj in searcher.Get())
                {
                    string name = obj["Name"]?.ToString();
                    if (!string.IsNullOrEmpty(name))
                        ComVal.Items.Add(name);
                }
            }
            catch { }

            ComVal.SelectedIndex =
                (BeallitasKezelo.comIndex >= 0 && BeallitasKezelo.comIndex < ComVal.Items.Count)
                    ? BeallitasKezelo.comIndex
                    : 0;
        }

        private void HangkimenetBetolt()
        {
            hangkimenetek.Clear();
            kimenetValaszt.Items.Clear();

            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            foreach (var dev in devices)
            {
                hangkimenetek.Add(dev);
                kimenetValaszt.Items.Add(dev.FriendlyName);
            }

            if (kimenetValaszt.Items.Count > 0)
                kimenetValaszt.SelectedIndex = 0;
        }

        private void StartStop(object sender, EventArgs e)
        {
            if (!bekapcsolva) StartBridge();
            else StopBridge();
        }

        private void StartBridge()
        {
            try
            {
                startTime = DateTime.Now;

                if (midiBeVal.SelectedIndex > 0)
                {
                    midiIn = new MidiIn(midiBeVal.SelectedIndex - 1);
                    midiIn.MessageReceived += MidiIn_uzenetJott;
                    midiIn.Start();
                    Log("MIDI In megnyitva: " + midiBeVal.SelectedItem);
                }

                if (midiKiVal.SelectedIndex > 0)
                {
                    midiOut = new MidiOut(midiKiVal.SelectedIndex - 1);
                    Log("MIDI Out megnyitva: " + midiKiVal.SelectedItem);
                }

                if (ComVal.SelectedIndex > 0)
                {
                    string selected = ComVal.SelectedItem.ToString();
                    int start = selected.IndexOf("(COM");
                    int end = selected.IndexOf(")");

                    if (start >= 0 && end > start)
                    {
                        string comPort = selected.Substring(start + 1, end - start - 1);

                        serialPort = new SerialPort(comPort)
                        {
                            BaudRate = BeallitasKezelo.BaudRate,
                            DataBits = BeallitasKezelo.DataBits,
                            Parity = BeallitasKezelo.Parity,
                            StopBits = BeallitasKezelo.StopBits,
                            Handshake = BeallitasKezelo.Handshake,
                            DtrEnable = true,  // fontos Arduino miatt
                            RtsEnable = true
                        };

                        serialPort.DataReceived += SerialPort_uzenetJott;
                        serialPort.Open();
                        Log("Serial port megnyitva: " + comPort);
                    }
                }

                bekapcsolva = true;
                buttonStartStop.Text = "Stop";
                labelStatus.Text = "Running";
            }
            catch (Exception ex)
            {
                Log("Hiba indításkor: " + ex.Message);
                labelStatus.Text = "Error";
            }
        }

        private void StopBridge()
        {
            midiIn?.Stop();
            midiIn?.Dispose();
            midiIn = null;

            midiOut?.Dispose();
            midiOut = null;

            if (serialPort != null)
            {
                serialPort.Close();
                serialPort.Dispose();
                serialPort = null;
            }

            bekapcsolva = false;
            buttonStartStop.Text = "Start";
            labelStatus.Text = "Stopped";
            Log("Átvitel leállítva.");
        }

        private void MidiIn_uzenetJott(object sender, MidiInMessageEventArgs e)
        {
            Log(MidiEventSzovegge(e.MidiEvent, "MIDI In"));

            if (e.MidiEvent is ControlChangeEvent cc)
                utolsoCCErtek[(int)cc.Controller] = cc.ControllerValue;
        }

        private void SerialPort_uzenetJott(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort == null || !serialPort.IsOpen) return;

            int bytesToRead = serialPort.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
            serialPort.Read(buffer, 0, bytesToRead);

            foreach (byte b in buffer)
            {
                if ((b & 0x80) != 0) serialBufferIndex = 0;
                if (serialBufferIndex < 3) serialBuffer[serialBufferIndex++] = b;

                if (serialBufferIndex == 3)
                {
                    byte status = serialBuffer[0];
                    byte data1 = serialBuffer[1];
                    byte data2 = serialBuffer[2];

                    int channel = (status & 0x0F) + 1;
                    string type = status switch
                    {
                        >= 0x80 and < 0x90 => "Note Off",
                        >= 0x90 and < 0xA0 => "Note On",
                        >= 0xB0 and < 0xC0 => "Controller",
                        _ => "Unknown"
                    };

                    double elapsed = (DateTime.Now - startTime).TotalSeconds;

                    if (logBe.Checked)
                        Log($"+{elapsed:F3} - Serial In: Ch {channel}: {type} {data1} value {data2}");

                    if ((status & 0xF0) == 0xB0)
                        utolsoCCErtek[data1] = data2;

                    midiOut?.Send(status | (data1 << 8) | (data2 << 16));
                    serialBufferIndex = 0;
                }
            }
        }

        private string MidiEventSzovegge(MidiEvent midiEvent, string prefix = "")
        {
            return midiEvent switch
            {
                ControlChangeEvent cc => $"{prefix}: Ch {cc.Channel + 1}: Controller {(int)cc.Controller} value {cc.ControllerValue}",
                NoteOnEvent noteOn => $"{prefix}: Ch {noteOn.Channel + 1}: Note On {noteOn.NoteNumber} velocity {noteOn.Velocity}",
                NoteEvent note => $"{prefix}: Ch {note.Channel + 1}: Note {note.NoteNumber} value {note.Velocity}",
                _ => $"{prefix}: {midiEvent.CommandCode} raw {midiEvent.GetAsShortMessage()}"
            };
        }

        private void Log(string message)
        {
            if (!logBe.Checked) return;

            if (textBoxLog.InvokeRequired)
                textBoxLog.BeginInvoke(new Action(() => LogToBox(message)));
            else
                LogToBox(message);
        }

        private void LogToBox(string message)
        {
            if (textBoxLog.Lines.Length >= 50)
                textBoxLog.Lines = textBoxLog.Lines.Skip(1).ToArray();

            textBoxLog.AppendText(message + Environment.NewLine);
            textBoxLog.SelectionStart = textBoxLog.Text.Length;
            textBoxLog.ScrollToCaret();
        }

        private void beallitasokNyit(object sender, EventArgs e)
        {
            using var frm = new beallitasok();
            frm.ShowDialog();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_DEVICECHANGE)
            {
                BeginInvoke(new Action(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(800);
                    MidiEszkozBetolt();
                    ComEszkozBetolt();
                    HangkimenetBetolt();
                }));
            }
        }

        private void kimenetiTanitKatt(object sender, EventArgs e)
        {
            if (kimenetValaszt.SelectedIndex < 0) return;

            string devName = hangkimenetek[kimenetValaszt.SelectedIndex].FriendlyName;
            int ccNumber = (int)numericCC.Value;

            kemenetVezerloTar[devName] = ccNumber;
            FrissitVezereltKimenetekLista();
        }

        private void FrissitVezereltKimenetekLista()
        {
            kimenetBeallitasok.Clear();
            List<string> torlendo = new List<string>();

            foreach (var kvp in kemenetVezerloTar)
            {
                if (kvp.Value == -1)
                {
                    torlendo.Add(kvp.Key);
                    continue;
                }

                kimenetBeallitasok.AppendText($"{kvp.Key} -> CC {kvp.Value}{Environment.NewLine}");
            }

            foreach (var key in torlendo)
                kemenetVezerloTar.Remove(key);
        }

        private void eszkozTorleseKatt(object sender, EventArgs e)
        {
            int ment = (int)numericCC.Value;
            numericCC.Value = -1;
            kimenetiTanitKatt(sender, e);
            numericCC.Value = ment;
        }
    }

    public static class BeallitasKezelo
    {
        private static readonly string appDataUt =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MIDI_Konverter");

        private static readonly string beallitasokFajl =
            Path.Combine(appDataUt, "settings.json");

        public static int BaudRate = 38400;
        public static int DataBits = 8;
        public static Parity Parity = Parity.None;
        public static StopBits StopBits = StopBits.One;
        public static Handshake Handshake = Handshake.None;

        public static int comIndex = 0;
        public static int midiInIndex = 0;
        public static int midiOutIndex = 0;
        public static bool bekapcsolva = false;

        public static Dictionary<string, int> kemenetVezerloTar { get; set; } = new Dictionary<string, int>();

        public static void Load()
        {
            try
            {
                if (!File.Exists(beallitasokFajl)) return;

                string json = File.ReadAllText(beallitasokFajl);
                var loaded = JsonSerializer.Deserialize<SettingsData>(json);

                if (loaded == null) return;

                BaudRate = loaded.BaudRate;
                DataBits = loaded.DataBits;
                Parity = loaded.Parity;
                StopBits = loaded.StopBits;
                Handshake = loaded.Handshake;
                comIndex = loaded.comIndex;
                midiInIndex = loaded.midiInIndex;
                midiOutIndex = loaded.midiOutIndex;
                kemenetVezerloTar = loaded.kemenetVezerloTar ?? new Dictionary<string, int>();
                bekapcsolva = loaded.bekapcsolva;
            }
            catch { }
        }

        public static void Save()
        {
            try
            {
                if (!Directory.Exists(appDataUt)) Directory.CreateDirectory(appDataUt);

                var data = new SettingsData
                {
                    BaudRate = BaudRate,
                    DataBits = DataBits,
                    Parity = Parity,
                    StopBits = StopBits,
                    Handshake = Handshake,
                    comIndex = comIndex,
                    midiInIndex = midiInIndex,
                    midiOutIndex = midiOutIndex,
                    kemenetVezerloTar = kemenetVezerloTar,
                    bekapcsolva = bekapcsolva
                };

                File.WriteAllText(beallitasokFajl, JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
            }
            catch { }
        }

        private class SettingsData
        {
            public int BaudRate { get; set; }
            public int DataBits { get; set; }
            public Parity Parity { get; set; }
            public StopBits StopBits { get; set; }
            public Handshake Handshake { get; set; }
            public int comIndex { get; set; }
            public int midiInIndex { get; set; }
            public int midiOutIndex { get; set; }
            public Dictionary<string, int> kemenetVezerloTar { get; set; }
            public bool bekapcsolva { get; set; }
        }
    }
}
