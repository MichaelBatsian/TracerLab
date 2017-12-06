using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Testers.Interfaces;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Impl
{
    public class ThreadBuilder
    {
        private ITracer _tracer;
        private Dictionary<int, TreeBuilder> _builders;

        public ThreadBuilder(ITracer tracer)
        {
            _tracer = tracer;
            _builders = new Dictionary<int, TreeBuilder>();
        }

        public void Tracing(TraceResult tr)
        {
            if (_builders.ContainsKey(tr.ThreadId))
            {
                _builders[tr.ThreadId].SetTraceResult(tr);
                return;
            }
            var curBuilder = new TreeBuilder();
            curBuilder.SetTraceResult(tr);
            _builders.Add(tr.ThreadId,curBuilder);
        }

        public List<TreeBuilder> GetBuilders()
        {
            return _builders.Values.ToList();
        }
    }
}
