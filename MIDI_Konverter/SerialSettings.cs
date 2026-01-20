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
}
