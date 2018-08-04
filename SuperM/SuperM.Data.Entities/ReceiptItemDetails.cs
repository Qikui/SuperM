using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Entities
{
    public class ReceiptItemDetails
    {
        public int ReceiptItemDetailsId { get; set; }

        public int ReceiptProductId { get; set; }

        public String ProductName { get; set; }

        public int PurchasedQty { get; set; }

        public decimal Price { get; set; }

        public int ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
