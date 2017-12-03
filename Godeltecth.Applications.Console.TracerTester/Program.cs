using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Godeltech.Testers.Impl;

namespace Godeltecth.Applications.Console.TracerTester
{
    class Program
    {
        private static Tracer tracer = Tracer.GetInstance();

        static void Main(string[] args)
        {
            var p = new Program();
            p.RunTestMethods();
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
        }

        public void MethodLevel2(int a, int b)
        {
            tracer.StartTrace();
            Thread.Sleep(250);
            MethodLevel3(5);
            tracer.StopTrace();
        }
        public void MethodLevel3(int a)
        {
            tracer.StartTrace();
            Thread.Sleep(150);
            tracer.StopTrace();
        }
    }
}
