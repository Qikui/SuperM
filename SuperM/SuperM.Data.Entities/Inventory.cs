using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Entities
{
    public class Inventory
    {
        public Product Product { get; set; }

        public int Qty { get; set; }
    }
}
