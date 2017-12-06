using System.IO;
using Godeltech.Testers.Formatter.Contract;
using System.Xml.Serialization;


namespace Godeltech.Testers.Formatter.Xml
{
 
    public class XmlFormatter<T>:IFormatter<T>
    {
        public string GetName()
        {
            return "xml";
        }

        public void Format(T obj, string savePath)
        {
            using (var sw = new StreamWriter(savePath,true))
            {
                var ser = new XmlSerializer(typeof(T));
                ser.Serialize(sw, obj);
            }
        }
    }
}
