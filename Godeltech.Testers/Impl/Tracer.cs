using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Godeltech.Testers.Interfaces;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Impl
{
    public class Tracer:ITracer
    {
        private Dictionary<int, Stack<Stopwatch>> _threads;
        private TraceResult _result;
        private static Tracer _tracer;

        public static Tracer GetInstance()
        {
            if (_tracer == null)
            {
                return _tracer = new Tracer();
            }
            return _tracer;
        }

        private Tracer()
        {
            _threads = new Dictionary<int, Stack<Stopwatch>>();
            _result = new TraceResult();
        }

        public void StartTrace()
        {
            var threadStack = new Stack<Stopwatch>();
            threadStack.Push(Stopwatch.StartNew());
            _threads.Add(Thread.CurrentThread.ManagedThreadId, threadStack);
        }

        public void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            _result.Level = _threads[threadId].Count;
            var timer = _threads[threadId].Pop();
            timer.Stop();
            var st = new StackTrace();
            var invokationFrame = (st.GetFrames())?[1];
            var declaringType = invokationFrame?.GetMethod().DeclaringType;

            if (invokationFrame != null && declaringType != null)
            {
                _result.ClassName = declaringType.Name;
                _result.MethodName = invokationFrame.GetMethod().Name;
                _result.ParamCountInMethod = invokationFrame
                    .GetMethod()
                    .GetParameters()
                    .Count();
            }
            _result.Time  = (int)timer.ElapsedMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return _result;
        }

     }
}
