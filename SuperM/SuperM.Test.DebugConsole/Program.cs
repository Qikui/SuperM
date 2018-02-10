using SumperM.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Test.DebugConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start test the application");
            ProductService ps = new ProductService();
            ps.Add(new Data.Entities.Product()
            {
                Name = "Apple",
                Price = 1.23m
            });

            Console.WriteLine("test done");
            Console.Read();
        }
    }
}
