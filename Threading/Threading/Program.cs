using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threading
{
    class Program
    {
        static  void Main(string[] args)
        {
            Main();
        }

        public static async Task Main()
        {
            // Execute the antecedent.
            Task<DayOfWeek> taskA = Task.Run(() => DateTime.Today.DayOfWeek);

            // Execute the continuation when the antecedent finishes.
            await taskA.ContinueWith(antecedent => Console.WriteLine("Today is {0}.", antecedent.Result));
        }
    }
}
