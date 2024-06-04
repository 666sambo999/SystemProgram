using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ProcessDis
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Process[] a_process = Process.GetProcesses();
            Dictionary<int, Process> d_process = a_process.ToDictionary(key => key.Id, process => process);
            Console.WriteLine(d_process.GetType().Name);
            
            for (int i = 0; i < d_process.Count; i++)
            {
                KeyValuePair<int, Process> process = d_process.ElementAt(i);
                Console.WriteLine($"{process.Key}\t {process.Value}");
            }
        }
    }
}
