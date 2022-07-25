using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new StockApp();
        }
        public class StockApp
        {
            static ManualResetEvent stocGol = new ManualResetEvent(true);
            static ManualResetEvent stocPlin = new ManualResetEvent(false);
            int contor;
            public StockApp()
            {
                Thread producator = new Thread(new ThreadStart(Add));
                Thread consumator = new Thread(new ThreadStart(Sub));
                producator.Start();
                consumator.Start();
                Console.ReadLine();
            }
            public void Add()
            {
                while (true)
                {
                    stocGol.WaitOne();
                    for (contor = 0; contor <= 10; contor++)
                    {
                        Console.WriteLine("contor = " + contor);
                        Console.WriteLine("consumator: sleeping");
                        Thread.Sleep(1000);
                        //stocGol.WaitOne();
                    }
                    Console.WriteLine("Consumator: ready");
                    if (contor == 10)
                    {
                        stocPlin.Set();
                        stocGol.Reset();
                    }
                }
                
            }
            public void Sub()
            {
                while (true)
                {
                    stocPlin.WaitOne();

                    for (contor = 10; contor >= 0; contor--)
                    {
                        Console.WriteLine("contor = " + contor);
                        Console.WriteLine("producator: sleeping");
                        Thread.Sleep(1000);
                        //stocPlin.WaitOne();
                    }
                    Console.WriteLine("Producator: ready");
                    if (contor == 0)
                    {
                        stocGol.Set();
                        stocPlin.Reset();
                    }
                }
                
            }
        }
    }
}
