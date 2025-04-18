﻿using CSV_Library;
using Optimize_Parallel_Processing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parallel_Processing
{

    public class Program
    {

        private static ConcurrentBag<DataModel> Bag = new ConcurrentBag<DataModel>() { };
        private static ConcurrentQueue<DataModel> Queue = new ConcurrentQueue<DataModel>() { };
        // async await 是一個必須同時存在的關鍵字
        static async Task Main(string[] args)
        {
            //Mutex mutex = new Mutex();
            //object lockObject = new object();
            ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
            // cancellation Token => 玩一下這個
            // lock  mutex concurrentBag/concurrentQueue/ReadWriteSlim/SemaphoreSlim(紅綠燈機制)

            string readPathDir = @"C:\Users\TUF\source\repos\Parallel_Processing\MockData_Read\MOCK_DATA";
            string writePathDir = @"C:\Users\TUF\source\repos\Parallel_Processing\MockData_Write\MOCK_DATA";

            List<double> readTimes = new List<double>();
            List<double> writeTimes = new List<double>();
            int batchCount = 3_000_000;
            int dataCount = 20_000_000; // 200 =>  i=0~200 , 0*50000 , 50000
            if (Directory.Exists(writePathDir))
                Directory.Delete(writePathDir, true);
            else
                Directory.CreateDirectory(writePathDir);
            //int batchCount = dataCount / 16;
            int quotient = dataCount / batchCount;
            int remainder = dataCount % batchCount;
            if (remainder > 0)
                quotient++;


            Stopwatch swTotal = new Stopwatch();
            swTotal.Start();

            await Parallel.ForAsync(0, quotient, new ParallelOptions { MaxDegreeOfParallelism = 5 }, async (index, token) =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Console.WriteLine($"任務{index + 1}啟動!");


                string path = $"{readPathDir}_{dataCount}.csv";
                rwLock.EnterReadLock();
                List<DataModel> result = CSV.Read<DataModel>(path
                    , index * batchCount, batchCount);
                rwLock.ExitReadLock();
                sw.Stop();
                double readTime = Math.Round((sw.ElapsedMilliseconds / 1000f), 2);
                readTimes.Add(readTime);


                sw.Restart();

                //lock (lockObject)
                //mutex.WaitOne();




                //mutex.ReleaseMutex();
                //sw.Stop();
                rwLock.EnterWriteLock();
                CSV.Write($"{writePathDir}_{dataCount}.csv", result);
                rwLock.ExitWriteLock();
                double writeTime = Math.Round((sw.ElapsedMilliseconds / 1000f), 2);

                writeTimes.Add(writeTime);
                Console.WriteLine("任務完成!");

                Console.WriteLine($"第{index + 1}任務完成! 讀取時間:{readTime} | 寫入時間:{writeTime} | 任務完成時間:{Math.Round(readTime + writeTime, 2)} s");

                result.Clear();
                result = null;
                GC.Collect();


            });


            swTotal.Stop();
            Console.WriteLine($"平均讀取時間:{readTimes.Median()} 平均寫入時間:{writeTimes.Median()} 總完成時間:{Math.Round((swTotal.ElapsedMilliseconds / 1000f), 2)} s");

            Console.ReadKey();
        }

    }

}