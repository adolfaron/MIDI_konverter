using System.IO.Ports;

namespace MIDI_Konverter
{
    public partial class beallitasok : Form
    {
        public beallitasok()
        {
            InitializeComponent();
            InitSerialSettingsUI();
        }

        private void InitSerialSettingsUI()
        {
            // Baud rate
            buildRat.DropDownStyle = ComboBoxStyle.DropDownList;
            buildRat.Items.AddRange(new object[]
            {
                "9600","19200","38400","57600","115200"
            });

            // Data bits
            DataBits.DropDownStyle = ComboBoxStyle.DropDownList;
            DataBits.Items.AddRange(new object[] { "5", "6", "7", "8" });

            // Parity
            Patity.DropDownStyle = ComboBoxStyle.DropDownList;
            Patity.Items.AddRange(Enum.GetNames(typeof(Parity)));

            // Stop bits
            StopBit.DropDownStyle = ComboBoxStyle.DropDownList;
            StopBit.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            // Handshake
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.Items.AddRange(Enum.GetNames(typeof(Handshake)));

            // alapértékek
            buildRat.SelectedItem = SerialSettings.BaudRate.ToString();
            DataBits.SelectedItem = SerialSettings.DataBits.ToString();
            Patity.SelectedItem = SerialSettings.Parity.ToString();
            StopBit.SelectedItem = SerialSettings.StopBits.ToString();
            comboBox5.SelectedItem = SerialSettings.Handshake.ToString();

            // események
            buildRat.SelectedIndexChanged += Save;
            DataBits.SelectedIndexChanged += Save;
            Patity.SelectedIndexChanged += Save;
            StopBit.SelectedIndexChanged += Save;
            comboBox5.SelectedIndexChanged += Save;
        }

        private void Save(object sender, EventArgs e)
        {
            if (buildRat.SelectedItem != null)
                SerialSettings.BaudRate = int.Parse(buildRat.SelectedItem.ToString());

            if (DataBits.SelectedItem != null)
                SerialSettings.DataBits = int.Parse(DataBits.SelectedItem.ToString());

            if (Patity.SelectedItem != null)
                SerialSettings.Parity = Enum.Parse<Parity>(Patity.SelectedItem.ToString());

            if (StopBit.SelectedItem != null)
                SerialSettings.StopBits = Enum.Parse<StopBits>(StopBit.SelectedItem.ToString());

            if (comboBox5.SelectedItem != null)
                SerialSettings.Handshake = Enum.Parse<Handshake>(comboBox5.SelectedItem.ToString());
        }
    }
}
