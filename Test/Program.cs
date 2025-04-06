using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("HELLO");
            sb.Append("WORLD");

            string str = sb[0].ToString();
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}
