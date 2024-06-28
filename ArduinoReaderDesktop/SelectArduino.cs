using System.IO.Ports;

namespace ArduinoReaderDesktop
{
    public partial class SelectArduino : Form
    {
        public SelectArduino()
        {
            InitializeComponent();
        }

        private void SelectArduino_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
            comboBox2.SelectedIndex = 12;
        }
        public string SelectedPort = string.Empty;
        public int SelectedBaud = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (SelectedPort != string.Empty && SelectedBaud != 0)
                DialogResult = DialogResult.OK;
            Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newPort = comboBox1.SelectedItem?.ToString();
            if (newPort is not null)
                SelectedPort = newPort;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            SelectedBaud = int.Parse(comboBox2.SelectedItem?.ToString() ?? "0");
            }
            catch{}

        }
    }
}
