using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godeltech.Data.Structures
{
    public class TreeNode<T>
    {
        public List<TreeNode<T>> Children { get; }
        public T Data { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> Parent { get; private set; }

        public TreeNode(T data)
        {
            this.Data = data;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> AddChild(T data)
        {
            var node = new TreeNode<T>(data);
            node.Parent = this;
            Children.Add(node);
            return node;
        }
    }
}
