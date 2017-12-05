﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Godeltech.Testers.Impl;

namespace Godeltecth.Applications.Consoles.TracerTester
{
    class Program
    {
        private static Tracer tracer = Tracer.GetInstance();
        TreeBuilder mainBuilder = new TreeBuilder();

        static void Main(string[] args)
        {
            var p = new Program();
            p.RunTestMethods();
            
            

            
        }

        public void ConsoleInterface(string[] args)
        {
            var result = new FormatResult<TracedMethodInfo>(tracer.GetTree(), tracer);
            var plugins = result.GetPlugins();
            var formatsNames = result.GetFormatsNames(plugins);
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "--f":
                        string[] outputParams = args.Skip(1).Take(3).ToArray();
                        if (outputParams.Length == 0)
                        {
                            DefaultErrorView();
                            break;
                        }
                        try
                        {
                            if (outputParams.Length == 1 && outputParams[0].Equals("console"))
                            {
                                result.ToConsole();
                                Consoles.WriteLine("Done.");
                                break;
                            }
                            if (outputParams.Length == 3 && outputParams[1].Equals("--o"))
                            {
                                if (result.ToSpecialFormat(plugins, outputParams[0], outputParams[2]) == null)
                                {
                                    DefaultErrorView();
                                    break;
                                }
                                Consoles.WriteLine("Done.");
                                break;
                            }
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Consoles.WriteLine("Wrong way to save  file");
                        }
                        DefaultErrorView();
                        break;
                    case "--h":
                        Consoles.WriteLine("--f - format to introduce tracer results");
                        Consoles.WriteLine("--f [format] --o [path\\file name] - set the path to output in file");
                        Consoles.WriteLine("formats:");
                        foreach (var name in formatsNames)
                        {
                            Consoles.WriteLine(name);
                        }
                        break;
                    default:
                        DefaultErrorView();
                        break;
                }
            }
        }

        public void DefaultErrorView()
        {
            Consoles.WriteLine("Wrong command or format.");
            Consoles.WriteLine("To view the list of commands choose key --h");
        }

        private void RunTestMethods()
        {
            MethodLevel1();
            MethodLevel3(5);
            MethodLevel2(5, 3);
        }


        /* Methods to test tracer work*/
        public void MethodLevel1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            MethodLevel2(1, 2);
            MethodLevel3(5);
            MethodLevel3(5);
            MethodLevel2(1, 2);
            MethodLevel2(1, 2);
            tracer.StopTrace();
            mainBuilder.SetTraceResult(tracer.GetTraceResult());
        }

        public void MethodLevel2(int a, int b)
        {
            tracer.StartTrace();
            Thread.Sleep(250);
            MethodLevel3(5);
            tracer.StopTrace();
            mainBuilder.SetTraceResult(tracer.GetTraceResult());
        }
        public void MethodLevel3(int a)
        {
            tracer.StartTrace();
            Thread.Sleep(150);
            tracer.StopTrace();
            mainBuilder.SetTraceResult(tracer.GetTraceResult());
        }
    }
}
