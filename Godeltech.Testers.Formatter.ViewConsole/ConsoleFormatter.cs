using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Data.Structures;
using Godeltech.Testers.Formatter.Contract;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Formatter.ViewConsole
{
    public  class ConsoleFormatter<T> : IFormatter<T> where T:ThreadInfo
    {
        public string GetName()
        {
            return "console";
        }

        public void Format (T threadInfo, string savePath)
        {
            var result = new StringBuilder();
            Traverse(threadInfo.MethodsInfo, 0, true, result);
            result.Append(Environment.NewLine);
            result.AppendFormat(
                $"ThreadId {threadInfo.ThreadId} Executing time {threadInfo.Time}");
            if (savePath != null)
            {
                Save(result.ToString(), savePath);
                return;
            }
            Console.WriteLine(result.ToString());
        }

        public void Traverse(TreeNode<TraceResult> tn, int level, bool isRoot, StringBuilder result)
        {
            if (!isRoot)
            {
                if (tn.Data == null)
                {
                    return;
                }
                for (int i = 0; i < level; i++)
                {
                    result.Append(" ");
                }
                result.Append(tn.Data.ToString());
                level++;
            }
            foreach (var kid in tn.Children)
            {
                result.Append(Environment.NewLine);
                Traverse(kid, level, false, result);
            }
        }

        public void Save(string str, string path)
        {
            using (var stream = new StreamWriter(path, false))
            {
                stream.Write(str);
            }
        }
    }
}
