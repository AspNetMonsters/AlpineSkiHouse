using Microsoft.Azure.WebJobs;
using System;
using System.Linq;

namespace AlpineSkiHouse.WebJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            JobHost host = new JobHost();
            host.RunAndBlock();
        }
    }
}
