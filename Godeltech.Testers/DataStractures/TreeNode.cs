using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godeltech.Testers.DataStractures
{
    public class TreeNode<T>
    {

        public List<TreeNode<T>> Children { get; }
        public T Data { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode<T>>();
        }

        public TreeNode(T data)
        {
            this.Data = data;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> Parent { get; set; } = null;

        public TreeNode<T> AddChild(T data)
        {
            var node = new TreeNode<T>(data);
            node.Parent = this;
            Children.Add(node);
            return node;
        }

        public void Traverse(TreeNode<T> tn, int level, bool isRoot)
        {
            if (!isRoot)
            {
                if (tn.Data == null)
                {
                    return;
                }
                string result = "";

                for (int i = 0; i < level; i++)
                {
                    result += " ";
                }
                result += tn.Data.ToString();

                Console.WriteLine(result);
                level++;
            }
            foreach (var kid in tn.Children)
            {
                Traverse(kid, level, false);
            }

        }
    }
}
