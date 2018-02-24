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
            InventoryService ps = new InventoryService();
            ps.Add(new Data.Entities.Inventory()
            {
                ProductId = 1,
                InventoryId = 1000,
                Qty = 7
            });

            Console.WriteLine("test done");
            Console.Read();
        }
    }
}
