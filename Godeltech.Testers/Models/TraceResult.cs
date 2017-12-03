using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godeltech.Testers.Models
{
    public class TraceResult
    {
        public int ThreadId { get; set; }
        public int Time { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ParamCountInMethod { get; set; }
        public int Level { get; set; }
    }
}
