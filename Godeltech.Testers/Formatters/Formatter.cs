using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Godeltech.Formatters.Plugins;
using Godeltech.Testers.Formatter.Contract;
using System.Configuration;

namespace Godeltech.Testers.Impl
{
    public class Formatter<T>
    {
        private ICollection<T> _info;
   
        public Formatter(ICollection<T> info)
        {
            _info = info;
        }

        public string ToSpecialFormat(ICollection<IFormatter<T>> plugins, string formatName, string output=null)
        {
            foreach (var plugin in plugins)
            {
                if (plugin.GetName().Equals(formatName,StringComparison.OrdinalIgnoreCase))
                {
                    foreach (var threadInfo in _info)
                    {
                        plugin.Format(threadInfo, output);
                    }
                 
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
            return PluginsLoader.LoadPlugins<T>(GetAbsolutePath(ConfigurationManager.AppSettings["PathToPlugins"]));
        }

        public static string GetAbsolutePath(string relativePath)
        {
            var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyPath != null)
            {
                var path = assemblyPath.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                var newPath = new string[path.Length - 3];
                Array.Copy(path, newPath, path.Length - 3);
                return String.Join("\\", newPath) + relativePath;
            }
            return null;
        }
    }
}
