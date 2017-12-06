using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Godeltech.Testers.Models
{
    [Serializable]
    [XmlRoot("method")]
    [DataContract(Name = "method")]
    public class TraceResult
    {
        [XmlIgnore]
        public int ThreadId { get; set; }

        [XmlAttribute(AttributeName = "time")]
        [DataMember (Name ="time") ]
        public int Time { get; set; }

        [XmlAttribute(AttributeName = "class")]
        [DataMember(Name = "class")]
        public string ClassName { get; set; }

        [XmlAttribute(AttributeName = "name")]
        [DataMember(Name = "name")]
        public string MethodName { get; set; }

        [XmlAttribute(AttributeName = "params")]
        [DataMember(Name = "params")]
        public int ParamCountInMethod { get; set; }

        public override string ToString()
        {
            return $"{nameof(Time)}: {Time}, {nameof(ClassName)}: {ClassName}, {nameof(MethodName)}: {MethodName}, {nameof(ParamCountInMethod)}: {ParamCountInMethod}";
        }
        [XmlIgnore]
        public int Level { get; set; }
    }
}
