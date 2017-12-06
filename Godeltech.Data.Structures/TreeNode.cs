using System.Collections.Generic;
using System.Xml.Serialization;

namespace Godeltech.Data.Structures
{
    public class TreeNode<T>
    {
        [XmlArray(ElementName = "method" )]
        public List<TreeNode<T>> Children { get; }
        
        public T Data { get; set; }

        public TreeNode()
        {
            Children = new List<TreeNode<T>>();
        }
        [XmlIgnore]
        public TreeNode<T> Parent { get; private set; }

        public TreeNode(T data)
        {
            this.Data = data;
            Children = new List<TreeNode<T>>();
        }

        public TreeNode<T> AddChild(T data)
        {
            var node = new TreeNode<T>(data) {Parent = this};
            Children.Add(node);
            return node;
        }
    }
}
