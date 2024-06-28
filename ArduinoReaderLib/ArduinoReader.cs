using System.Diagnostics;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace ArduinoReaderLib
{
    public partial class ArduinoReader(string device, string folder, int baud = 115200)
    {
        public LinkedList<DataPoint> DataPoints { get; init; } = [];
        public string Folder { get; set; } = folder;
        public string Device { get; set; } = device;
        public int Baud { get; set; } = baud;
        public async Task SaveToFile(IProgress<double>? progress = default, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            var filename = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".graph";
            var path = Path.Combine(Folder, filename);
            using var fs = new FileStream(path, FileMode.CreateNew);
            for (int i = 0; i < DataPoints.Count; i++)
            {
                progress?.Report(i / DataPoints.Count);
                await fs.WriteAsync(DataPoints.ElementAt(i).Bytes, token);
            }
            fs.Close();
        }
        private SerialPort? serialPort;
        private Thread? arduinoThread;
        private bool isRunning;
        private Stopwatch? stopwatch;
        public event EventHandler<DataPoint>? NewDataReceived;
        public void Start()
        {
            DataPoints.Clear();
            stopwatch ??= new Stopwatch();
            stopwatch.Reset();
            serialPort?.Dispose();
            serialPort = new SerialPort(Device, Baud);
            isRunning = true;
            arduinoThread = new Thread(Worker);
            arduinoThread.Start();
            stopwatch.Start();
        }
        public void Stop()
        {
            if (arduinoThread is null
                || serialPort is null
                || stopwatch is null
                ) throw new InvalidOperationException("No arduino thread");
            isRunning = false;
            arduinoThread.Join();
            stopwatch.Stop();
            serialPort.Close();
        }

        private void Worker()
        {
            stopwatch ??= Stopwatch.StartNew();
            serialPort ??= new SerialPort(Device, Baud);
            if (!serialPort.IsOpen)
                serialPort.Open();
            while (isRunning)
            {
                var line = serialPort.ReadLine();
                var match = FormatRegex().Match(line);
                if (!match.Success) continue;
                int index = int.Parse(match.Groups[1].Value);
                double value = double.Parse(match.Groups[2].Value);

                var dp = new DataPoint()
                {
                    Value = value,
                    Index = index,
                    Time = stopwatch.ElapsedMilliseconds
                };
                DataPoints.AddLast(dp);
                NewDataReceived?.Invoke(this, dp);
            }
        }

        [GeneratedRegex(@"P(\d+):(\d+(?:\.\d+)?)")]
        private static partial Regex FormatRegex();
    }
}