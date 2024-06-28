using ArduinoReaderLib;

const string device = "COM5";
string folder = Directory.GetCurrentDirectory();
var reader = new ArduinoReader(device,folder, 115200);
var command = Console.ReadLine()?.ToLower();
switch(command)
{
    case "read":
        reader.NewDataReceived += (sender, args) =>
        {
            Console.WriteLine("Data: {0}", args);
        };
        reader.Start();
        await Task.Delay(5000);
        reader.Stop();
        await reader.SaveToFile();
        break;
    case "file":
        var files = Directory.GetFiles(folder,"*.graph");
        foreach (var file in files)
        {
            var dpr = new DataPointReader(file);
            await dpr.Read();
            var dict = dpr.TransformToDictionary();
            Console.WriteLine("File: {0}",file);
            foreach (var key in dict)
            {
                Console.WriteLine("\tIndex: {0}", key.Key);
                foreach (var arr in key.Value)
                    Console.WriteLine("\t\t{0}", arr.ToString());
            }
        }
        break;
}
