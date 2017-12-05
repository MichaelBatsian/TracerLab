using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Godeltech.Testers.Formatter.Contract;


namespace Godeltech.Formatters.Plugins
{
    public class PluginsLoader
    {
        public static ICollection<IFormatter<T>> LoadPlugins<T>(string path)
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
                var assemblies = new List<Assembly>(dllFileNames.Length);

                foreach (string dllFile in dllFileNames)
                {
                    var an = AssemblyName.GetAssemblyName(dllFile);
                    var assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }
                var pluginType = typeof(IFormatter<>);
                var pluginTypes = new List<Type>();

                foreach (var assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        var types = assembly.GetTypes();

                        foreach (var type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }
                var plugins = new List<IFormatter<T>>(pluginTypes.Count);

                foreach (Type type in pluginTypes)
                {
                    var t = type.MakeGenericType(typeof(T));
                    var plugin = (IFormatter<T>)Activator.CreateInstance(t);
                    plugins.Add(plugin);
                }
                return plugins;
            }
            return null;
        }
    }
}
