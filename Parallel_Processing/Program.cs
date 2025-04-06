using CSV_Library;
using StreamLibarary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Processing
{

    internal class Program
    {



        // async await 是一個必須同時存在的關鍵字
        static async Task Main(string[] args)
        {
            #region 非同步本身就是一個假議題，不是所有的東西都可以用非同步來解決
            //1.只有獨立不相干的任務且耗時你可以使用非同步完成
            //2.當有一個以上的非同步任務需要執行(無相關)，可以採取先讓短的先跑，後面再等待耗時久的
            //3.當你想在背景處理，且不需要拿回結果時 => 本身非同步就是執行續所以可以直接在背景處理
            //只有上述三種情況使用非同步可以有效率解決問題，其他狀況下都還是同步進行
            //Stopwatch sw = Stopwatch.StartNew();

            //sw.Start();
            //Task<string> t1 = WaitFor3S();
            //Task<string> t2 = WaitFor6S();
            //string res3 = await WaitFor9S();
            //sw.Stop();

            //Console.WriteLine(await t1);
            //Console.WriteLine(await t2);
            //Console.WriteLine(res3);
            //Console.WriteLine(sw.ElapsedMilliseconds);
            #endregion

            string readPathDir = @"C:\Users\TUF\source\repos\Parallel_Processing\MockData_Read\MOCK_DATA";
            string writePathDir = @"C:\Users\TUF\source\repos\Parallel_Processing\MockData_Write\MOCK_DATA";

            #region 一萬筆資料讀取+寫入
            int batchCount = 3_000_000;
            int dataCount = 10_000_000; // 200 =>  i=0~200 , 0*50000 , 50000
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
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < (batchCount <= dataCount ? quotient : 1); i++)
            {
                int count = i;
                Task task = new Task(() =>
                {

                    Console.WriteLine($"任務{count + 1}啟動!");
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string path = $"{readPathDir}_{dataCount}.csv";
                    List<DataModel> result = CSV.Read<DataModel>(path
                        , count * batchCount, batchCount);
                    Console.WriteLine($"第{count + 1}批讀取時間，第{count * batchCount}~{count * batchCount + batchCount}筆資料:{sw.ElapsedMilliseconds / 1000f} s" + "------");

                    //sw.Restart();
                    CSV.Write($"{writePathDir}_{dataCount}_{count}.csv", result);
                    Console.WriteLine($"第{count + 1}批寫入時間，第{count * batchCount}~{count * batchCount + batchCount}筆資料:{sw.ElapsedMilliseconds / 1000f} s");
                    Console.WriteLine($"任務{count + 1}完成!，該批任務耗時:{sw.ElapsedMilliseconds / 1000f} s");
                    result.Clear();
                    result = null;
                    GC.Collect();
                });
                task.Start();
                tasks.Add(task);

            }

            await Task.WhenAll(tasks);
            Console.WriteLine("任務完成!");
            swTotal.Stop();
            Console.WriteLine($"完成時間:{swTotal.ElapsedMilliseconds / 1000f} s");
            #endregion
            Console.ReadKey();
        }
    }
}
