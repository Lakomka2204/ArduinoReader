namespace ArduinoReaderLib
{
    public class DataPointReader(string filename)
    {
        public string Filename { get; set; } = filename;
        public List<DataPoint> DataPoints { get; init; } = [];
        public async Task Read(IProgress<double>? progress = default, CancellationToken token = default)
        {
            FileInfo info = new(Filename);
            token.ThrowIfCancellationRequested();
            using FileStream fs = new FileStream(Filename, FileMode.Open);
            int dataSize = DataPoint.SizeOf;
            byte[] buffer = new byte[dataSize];
            double read = 0;
            double fileLength = info.Length;
            while (await fs.ReadAsync(buffer, token) > 0)
            {
                read += buffer.Length;
                var dp = DataPoint.FromBytes(buffer);
                DataPoints.Add(dp);
                progress?.Report(read / fileLength);
            }
        }
    }
}
