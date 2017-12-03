using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Testers.DataStractures;

namespace Godeltech.Testers.Interfaces
{
    interface IFormatter<T>
    {
        string GetName();
        void Format(TreeNode<T> tree);
    }
}
