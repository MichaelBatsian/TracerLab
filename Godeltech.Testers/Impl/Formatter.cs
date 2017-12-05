using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Godeltech.Data.Structures;
using Godeltech.Formatters.Plugins;
using Godeltech.Testers.Formatter.Contract;
using Godeltech.Testers.Models;

namespace Godeltech.Testers.Impl
{
    public class Formatter<T>
    {
        private T _info;
        public const string PathToJson = "\\JsonFormatter\\bin\\Debug\\JsonFormatter.dll";
        public const string PathToYaml = "\\YamlFormatter\\bin\\Debug\\YamlFormatter.dll";
        public const string PathToPlugins = "\\Plugins";

        public Formatter(T info)
        {
            _info = info;
        }

        public string ToSpecialFormat(ICollection<IFormatter<T>> plugins, string formatName, string output=null)
        {
            var pathToPlugins = GetAbsolutePath("Plugins");
            foreach (var plugin in plugins)
            {
                if (plugin.GetName().Equals(formatName,StringComparison.OrdinalIgnoreCase))
                {
                    plugin.Format(_info, output);
                    return formatName;
                }
            }
            return null;
        }

        public List<string> GetFormatsNames(ICollection<IFormatter<T>> plugins)
        {
            var names = new List<string>();
            foreach (var plugin in plugins)
            {
               names.Add(plugin.GetName());
            }
            return names;
        }

        public ICollection<IFormatter<T>> GetPlugins()
        {
            return PluginsLoader.LoadPlugins<T>(GetAbsolutePath(PathToPlugins));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = assemblyPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
            var newPath = new string[path.Length - 3];
            Array.Copy(path, newPath, path.Length - 3);
            return String.Join("\\", newPath) + relativePath;
        }
    }
}
