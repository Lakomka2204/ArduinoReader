using ArduinoReaderLib;

namespace ArduinoReaderDesktop
{
    public class DataPointCollection : List<DataPoint>
    {
        public Color Color { get; set; }
        public DataPointCollection() : base()
        {
            Color = GetRandomColor();
        }
        private static Color GetRandomColor()
        {
            Random random = new();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
        public static DataPointDictionary TransformToDictionary(IList<DataPoint> dataPoints)
        {
            DataPointDictionary dictionary = [];

            foreach (var dataPoint in dataPoints)
            {
                if (!dictionary.TryGetValue(dataPoint.Index, out DataPointCollection? value))
                {
                    value = [];
                    dictionary[dataPoint.Index] = value;
                }

                value.Add(dataPoint);
            }

            return dictionary;
        }
    }
}
