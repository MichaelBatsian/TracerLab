using System.IO;
using Godeltech.Testers.Formatter.Contract;
using  System.Runtime.Serialization.Json;

namespace Godeltech.Testers.Formatter.Json
{
    public class JsonFormatter<T>:IFormatter<T>
    {
        public string GetName()
        {
            return "json";
        }

        public void Format(T obj, string savePath)
        {
            using (var fs = new FileStream(savePath, FileMode.Append))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                ser.WriteObject(fs, obj);
            }
        }
    }
}

