using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Data.Structures;
using System.Runtime.Serialization.Json; 

namespace Godeltech.Testers.Models
{
    [DataContract (Name = "thread")]
    public class ThreadInfo
    {  
        [DataMember (Name = "id")]
        public int ThreadId { get; set; }
        [DataMember(Name = "time")]
        public int Time { get; set; }
        [DataMember (Name = "methods")]
        public TreeNode<TraceResult> MethodsInfo { get; set; }
    }
}
