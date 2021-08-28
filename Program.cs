using System;
using System.Diagnostics;

namespace Seccion2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            SearchPath.CreateMaze(10,10);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            SearchPath.BFS();
            SearchPath.CreatePath();
            stopwatch.Stop();
            Console.WriteLine("Path created in: " + stopwatch.Elapsed);
            SearchPath.PrintPath();
        }
    }
}