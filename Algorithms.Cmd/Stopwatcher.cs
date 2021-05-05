using System;
using System.Diagnostics;

namespace Algorithms.Cmd
{
    public class Stopwatcher : IDisposable
    {
        private Stopwatch _stopwatch;
        
        public Stopwatcher()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            Console.WriteLine($"用时：{_stopwatch.Elapsed}");
        }
    }
}