using System.Collections.Generic;
using Godeltech.Testers.DataStractures;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Impl
{
    public class TreeBuilder
    {
        
        private Stack<TraceResult> _methodsStack;
        private Dictionary<int, TreeNode<TraceResult>> _leaves;
        public TreeNode<TraceResult> TreeRoot { get; private set; }

        public TreeBuilder()
        {
            _methodsStack = new Stack<TraceResult>();
        }

        public void SetTraceResult(TraceResult tr)
        {
            _methodsStack.Push(tr);

            if (tr.Level == 0)
            {
                Build();
            }
        }

        private void Build()
        {
            _leaves = new Dictionary<int,TreeNode<TraceResult>>();
            TreeRoot = new TreeNode<TraceResult>();
            var current = TreeRoot;

            foreach (var tr in _methodsStack)
            {
                if (_leaves.ContainsKey(tr.Level))
                {
                    _leaves[tr.Level].Parent.AddChild(tr);
                    break;
                }
                current = current.AddChild(tr);
                _leaves.Add(tr.Level, current);
            }
        }

        public ThreadInfo GetThreadInfo()
        {
            if (_leaves.ContainsKey(0))
            {
                return new ThreadInfo
                {
                    Time = _leaves[0].Data.Time,
                    ThreadId = _leaves[0].Data.ThreadId
                };
            }
            return null;
        }

    }
}
