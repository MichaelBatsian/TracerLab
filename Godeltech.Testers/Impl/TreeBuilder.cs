using System.Collections.Generic;
using Godeltech.Data.Structures;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Impl
{
    public class TreeBuilder
    {
        
        private Stack<TraceResult> _methodsStack;
        private Dictionary<int, TreeNode<TraceResult>> _leaves;
        public TreeNode<TraceResult> TreeRoot { get; private set; } = new TreeNode<TraceResult>();

        public TreeBuilder()
        {
            _methodsStack = new Stack<TraceResult>();
        }

        public void SetTraceResult(TraceResult tr)
        {
            _methodsStack.Push(tr);

            if (tr.Level == 1)
            {
                Build();
                _methodsStack.Clear();
            }
        }

        private void Build()
        {
            _leaves = new Dictionary<int,TreeNode<TraceResult>>();
            var current = TreeRoot;

            foreach (var tr in _methodsStack)
            {
                if (_leaves.ContainsKey(tr.Level))
                {
                    current =_leaves[tr.Level].Parent.AddChild(tr);
                    continue;
                }
                current = current.AddChild(tr);
                _leaves.Add(tr.Level, current);
            }
        }

        public ThreadInfo GetThreadInfo()
        {
            var threadTime = 0;
            foreach (var child in TreeRoot.Children)
            {
                threadTime += child.Data.Time;
            }
            return  new ThreadInfo
            {
                Time = threadTime,
                MethodsInfo = TreeRoot,
                ThreadId = _leaves[1].Data.ThreadId
            };
        }

    }
}
