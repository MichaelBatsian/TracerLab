using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Interfaces
{
    public interface IBuilder
    {
        void SetTraceResult(TraceResult tr);
        ThreadInfo GetThreadInfo();
    }
}
