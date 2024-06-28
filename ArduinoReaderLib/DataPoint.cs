using System.Text;

namespace ArduinoReaderLib
{
    public struct DataPoint
    {
        public int Index;
        public double Value;
        public long Time;
        public readonly byte[] Bytes
        {
            get
            {
                IEnumerable<byte> bytes = [];
                bytes = bytes.Concat(BitConverter.GetBytes(Index));
                bytes = bytes.Concat(BitConverter.GetBytes(Value));
                bytes = bytes.Concat(BitConverter.GetBytes(Time));
                return bytes.ToArray();
            }
        }
        public static DataPoint FromBytes(byte[] bytes)
        {
            int index = BitConverter.ToInt32(bytes, 0);
            double value = BitConverter.ToDouble(bytes, sizeof(int));
            long time = BitConverter.ToInt64(bytes, sizeof(int)+sizeof(long));
            return new DataPoint { Index = index, Value = value, Time = time};
        }
        public static int SizeOf
        {
            get
            {
                int size = 0;
                foreach (var field in typeof(DataPoint).GetFields())
                {
                    size += System.Runtime.InteropServices.Marshal.SizeOf(field.FieldType);
                }
                return size;
            }
        }
        public override readonly string ToString()
        {
            StringBuilder sb = new();
            foreach (var fieldInfo in GetType().GetFields())
            {
                sb.Append(fieldInfo.Name);
                sb.Append(": ");
                sb.Append(fieldInfo.GetValue(this));
                sb.Append(' ');
            }
            return sb.ToString();
        }
    }
}
