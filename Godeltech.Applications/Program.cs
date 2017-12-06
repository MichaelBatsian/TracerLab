using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Godeltech.Testers.Impl;
using Godeltech.Testers.Models;

namespace Godeltech.Applications
{
    class Program
    {
        private static Tracer tracer = Tracer.GetInstance();
        TreeBuilder threadBuilder = new TreeBuilder();
        TreeBuilder threadBuilder2 = new TreeBuilder();

        static void Main(string[] args)
        {
            var run = true;
            var first = true;
            var p = new Program();
            p.RunTestMethods();

            new Thread(()=>p.RunTestMethods2()).Start();

            while (run)
            {
                var line = Console.ReadLine();
                if (line == null)
                {
                    Console.WriteLine("Wrong command.");
                    continue;
                }
                var inputArgs = first ? args : line.Split(' ');
                p.ConsoleInterface(inputArgs);
                Console.WriteLine("Continue y/n?");
                if (Console.ReadLine().Equals("n"))
                {
                    run = false;
                }
                first = false;
                Console.WriteLine("Enter command:");
            }
            Console.ReadKey();
        }

        public void ConsoleInterface(string[] args)
        {
            var result = new Formatter<ThreadInfo>(new List<ThreadInfo>()
            {
                threadBuilder.GetThreadInfo(),
                threadBuilder2.GetThreadInfo()
            });
            var plugins = result.GetPlugins();
            var formatsNames = result.GetFormatsNames(plugins);
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "--f":
                        var outputParams = args.Skip(1).Take(3).ToArray();
                        if (outputParams.Length == 0)
                        {
                            DefaultErrorView();
                            break;
                        }
                        try
                        {
                            if (outputParams.Length == 1 && outputParams[0].Equals("console"))
                            {
                                if (result.ToSpecialFormat(plugins, outputParams[0]) == null)
                                {
                                    DefaultErrorView();
                                    break;
                                }
                                Console.WriteLine("Done.");
                                break;
                            }
                            if (outputParams.Length == 3 && outputParams[1].Equals("--o"))
                            {
                                if (result.ToSpecialFormat(plugins, outputParams[0], outputParams[2]) == null)
                                {
                                    DefaultErrorView();
                                    break;
                                }
                                Console.WriteLine("Done.");
                                break;
                            }
                        }
                        catch (DirectoryNotFoundException)
                        {
                            Console.WriteLine("Wrong way to save  file");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Wrong path name");
                        }
                        DefaultErrorView();
                        break;
                    case "--h":
                        Console.WriteLine("--f - format to introduce tracer results");
                        Console.WriteLine("--f [format] --o [path\\file name] - set the path to output in file");
                        Console.WriteLine("formats:");
                        foreach (var name in formatsNames)
                        {
                            Console.WriteLine(name);
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
            Console.WriteLine("Wrong command or format.");
            Console.WriteLine("To view the list of commands choose key --h");
        }

        private void RunTestMethods()
        {
            MethodLevel1();
            MethodLevel3(5);
            MethodLevel2(5, 3);
        }

        /* Methods to test tracer work thread1*/
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
            threadBuilder.SetTraceResult(tracer.GetTraceResult());
        }

        public void MethodLevel2(int a, int b)
        {
            tracer.StartTrace();
            Thread.Sleep(250);
            MethodLevel3(5);
            tracer.StopTrace();
            threadBuilder.SetTraceResult(tracer.GetTraceResult());
        }
        public void MethodLevel3(int a)
        {
            tracer.StartTrace();
            Thread.Sleep(150);
            tracer.StopTrace();
            threadBuilder.SetTraceResult(tracer.GetTraceResult());
        }
        private void RunTestMethods2()
        {
            MethodLevel4();
            MethodLevel6(5);
            MethodLevel5(5, 3);
        }

        /* Methods to test tracer work thread2*/
        public void MethodLevel4()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            MethodLevel5(1, 2);
            MethodLevel6(5);
            MethodLevel6(5);
            MethodLevel5(1, 2);
            MethodLevel5(1, 2);
            tracer.StopTrace();
            threadBuilder2.SetTraceResult(tracer.GetTraceResult());
        }

        public void MethodLevel5(int a, int b)
        {
            tracer.StartTrace();
            Thread.Sleep(250);
            MethodLevel6(5);
            tracer.StopTrace();
            threadBuilder2.SetTraceResult(tracer.GetTraceResult());
        }

        public void MethodLevel6(int a)
        {
            tracer.StartTrace();
            Thread.Sleep(150);
            tracer.StopTrace();
            threadBuilder2.SetTraceResult(tracer.GetTraceResult());
        }
    }
}
