using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Entities
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }

        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
