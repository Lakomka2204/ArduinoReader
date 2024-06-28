using System.Runtime.InteropServices;

namespace ArduinoReaderDesktop
{
    public partial class IconExtractor
    {

        public static Icon? Extract(int number, bool largeIcon)
        {
            ExtractIconEx("shell32.dll", number, out nint large, out nint small, 1);
            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }

        }
        [LibraryImport("Shell32.dll", EntryPoint = "ExtractIconExW", StringMarshalling = StringMarshalling.Utf16)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static partial int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);
    }
}
