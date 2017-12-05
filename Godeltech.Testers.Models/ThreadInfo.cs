using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Godeltech.Data.Structures;

namespace Godeltech.Testers.Models
{
    [Serializable]
    [DataContract (Name = "thread")]
    [XmlRoot("thread")]
    public class ThreadInfo
    {  
        [XmlAttribute(AttributeName = "id" )]
        [DataMember (Name = "id")]
        public int ThreadId { get; set; }

        [XmlAttribute(AttributeName = "time")]
        [DataMember(Name = "time")]
        public int Time { get; set; }

        [XmlElement (ElementName = "methods")]
        [DataMember (Name = "methods")]
        public TreeNode<TraceResult> MethodsInfo { get; set; }
    }
}
