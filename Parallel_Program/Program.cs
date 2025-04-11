
using System;
using System.Threading;

namespace threaddemo
{
    class Program
    {
        private volatile static bool[] canStop;  
        private static object lockObj = new object(); 
        private static int threadCount = 8; 

        static void Main(string[] args)
        {
            (new Program()).Start();
        }

        void Start()
        {
            canStop = new bool[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                int threadIndex = i;
                new Thread(() => Calcuator(threadIndex)).Start();
                new Thread(() => Stoper(threadIndex)).Start();
            }
        }

        void Calcuator(int threadIndex)
        {
            long sum = 0;
            long iterations = 0;

            while (!canStop[threadIndex])
            {
                sum += 1; 
                iterations++;
            }

            
            lock (lockObj)
            {
                Console.WriteLine($"Потiк {threadIndex + 1}: Сума = {sum}, Iтерацiй = {iterations}");
            }
        }

        void Stoper(int threadIndex)
        {
            int waitTime = 25 * 1000; 
            Thread.Sleep(waitTime);
            canStop[threadIndex] = true;
        }
    }
}
