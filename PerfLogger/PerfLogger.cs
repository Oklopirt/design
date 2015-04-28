using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PerfLogger
{
    class PerfLogger : IDisposable
    {
        private Stopwatch sw;
        private Action<int> resultManager;

        private PerfLogger(Action<int> resultManager)
        {
            this.resultManager = resultManager;
            sw = new Stopwatch();
            sw.Start();
        }

        public static IDisposable Measure(Action<int> timeManager )
        {
            return new PerfLogger(timeManager);
        }

        public void Dispose()
        {
            resultManager(sw.Elapsed.Milliseconds);
        }
    }
}
