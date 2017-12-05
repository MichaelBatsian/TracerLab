using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, obj);
            byte[] json = ms.ToArray();
            ms.Close();
            var a = Encoding.UTF8.GetString(json, 0, json.Length);

            Save(a, savePath);
        }

        private void Save(string obj, string savePath)
        {
            using (var sw = new StreamWriter(savePath))
            {
                sw.Write(obj);
            }
        }
    }
}

