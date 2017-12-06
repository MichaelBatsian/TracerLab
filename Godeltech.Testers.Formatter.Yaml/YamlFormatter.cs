using System.IO;
using Godeltech.Testers.Formatter.Contract;
using YamlDotNet.Serialization;

namespace Godeltech.Testers.Formatter.Yaml
{
    public class YamlFormatter<T>:IFormatter<T>
    {
        public string GetName()
        {
            return "yaml";
        }

        public void Format(T obj, string savePath)
        {
            using (var fs = new StreamWriter(savePath,true))
            {
                var serializer = new SerializerBuilder().Build();
                serializer.Serialize(obj);
                fs.Write(serializer.Serialize(obj));
            }
        }
    }
}
