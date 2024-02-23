﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsingTask
{
    class TaskResult
    {
        public void primenumber(long from, long to, int taskCount)
        {
            Func<object, List<long>> FindPrimeFunc =
                (objRange) =>
                {
                    long[] range = (long[])objRange;
                    List<long> found = new List<long>();

                    for (long i = range[0]; i < range[1]; i++)
                    {
                        if (IsPrime(i))
                            found.Add(i);
                    }

                    return found;
                };

            Task<List<long>>[] tasks = new Task<List<long>>[taskCount];
            long currentFrom = from;
            long currentTo = to / tasks.Length;
            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine("Task[{0}] : {1} ~ {2}",
                    i, currentFrom, currentTo);

                tasks[i] = new Task<List<long>>(FindPrimeFunc,
                    new long[] { currentFrom, currentTo });
                currentFrom = currentTo + 1;

                if (i == tasks.Length - 2)
                    currentTo = to;
                else
                    currentTo = currentTo + (to / tasks.Length);
            }

            Console.WriteLine("Please press enter to start...");
            Console.ReadLine();
            Console.WriteLine("Started...");

            DateTime startTime = DateTime.Now;

            foreach (Task<List<long>> task in tasks)
                task.Start();

            List<long> total = new List<long>();

            foreach (Task<List<long>> task in tasks)
            {
                task.Wait();
                total.AddRange(task.Result.ToArray());
            }
            DateTime endTime = DateTime.Now;

            TimeSpan ellapsed = endTime - startTime;

            Console.WriteLine("Prime number count between {0} and {1} : {2}",
                                                        from, to, total.Count);
            Console.WriteLine("Ellapsed time : {0}", ellapsed);
        }
        static bool IsPrime(long number)
        {
            if (number < 2) return false;
            if (number % 2 == 0) return number == 2;

            long sqrt = (long)Math.Sqrt(number);
            for (long i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
