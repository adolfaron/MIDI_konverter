using System.IO.Ports;

namespace MIDI_Konverter
{
    public static class SerialSettings
    {
        public static int BaudRate = 38400;
        public static int DataBits = 8;
        public static Parity Parity = Parity.None;
        public static StopBits StopBits = StopBits.One;
        public static Handshake Handshake = Handshake.None;
    }

    public static class elozmeny
    {
        public static int comIndex = -1;
        public static int midiInIndex = -1;
        public static int midiOutIndex = -1;
        public static bool bekapcsolva = false;
    }

    // Adattároló osztály a serializáláshoz
    public class SerialSettingsData
    {
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public Handshake Handshake { get; set; }
    }

    public class ElojmenyData
    {
        public int comIndex { get; set; }
        public int midiInIndex { get; set; }
        public int midiOutIndex { get; set; }
        public bool bekapcsolva { get; set; }
    }
}
