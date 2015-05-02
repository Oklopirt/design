using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfLogger
{

    internal class PerfLogger : IDisposable
    {

        public static  Stopwatch watch = new Stopwatch();
        public static  Action<int> act;
        public void Dispose()
        {

        }

        public static IDisposable Measure(Action<int>  action)
        {
            
            watch.Start();
            act = action;

        }
    }
}
