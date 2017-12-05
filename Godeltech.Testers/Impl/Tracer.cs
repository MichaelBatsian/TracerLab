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
            Stack<Stopwatch> threadStack = null;
            var threadId = Thread.CurrentThread.ManagedThreadId;
            if (_threads.ContainsKey(threadId))
            {
                threadStack = _threads[threadId];
            }
            else
            {
                threadStack = new Stack<Stopwatch>();
                _threads.Add(threadId, threadStack);
            }
            threadStack.Push(Stopwatch.StartNew());
        }

        public void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            _result = new TraceResult {Level = _threads[threadId].Count};
            var timer = _threads[threadId].Pop();
            timer.Stop();
            _result.ThreadId = threadId;
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
