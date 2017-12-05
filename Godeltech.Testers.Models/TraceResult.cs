using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Data.Structures;
using System.Runtime.Serialization.Json;

namespace Godeltech.Testers.Models
{
    [DataContract(Name = "method")]
    public class TraceResult
    {
        public int ThreadId { get; set; }
        [DataMember (Name ="time") ]
        public int Time { get; set; }
        [DataMember(Name = "class")]
        public string ClassName { get; set; }
        [DataMember(Name = "name")]
        public string MethodName { get; set; }
        [DataMember(Name = "params")]
        public int ParamCountInMethod { get; set; }

        public override string ToString()
        {
            return $"{nameof(Time)}: {Time}, {nameof(ClassName)}: {ClassName}, {nameof(MethodName)}: {MethodName}, {nameof(ParamCountInMethod)}: {ParamCountInMethod}";
        }

        public int Level { get; set; }
    }
}
