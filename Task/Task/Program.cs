using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UsingTask
{
    class Program
    {
        

        static void Main(string[] args)
        {
            TaskResult taskResult = new TaskResult();
            taskResult.primenumber(2,100,4);
        }

        private void BasicTask()
        {
            // 1. Task.Start
            Action someAction = () =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Printed asynchronously");
            };
            Task myTask = new Task(someAction);
            myTask.Start();

            Console.WriteLine("Printed synchronously");

            myTask.Wait();

            // 2. Task.Run 메소드는 생성과 시작을 단번에 함
            Task myTask2 = Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Printed async");
            });
            Console.WriteLine("Printed sync");
            myTask2.Wait();

            // 3. Task<TResult>
            List<int> myList = new List<int> { 0, 1, 2 };

            Task<List<int>> myTask3 = Task.Run(() =>
            {
                Thread.Sleep(1000); // 시뮬레이션을 위해 1초 대기
                List<int> list = new List<int> { 3, 4, 5 };
                return list;
            });

            myTask3.Wait();
            myList.AddRange(myTask3.Result);
        }

    }
}
