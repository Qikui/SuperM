using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Entities
{
    public class Receipt
    {
        public int ReceiptId { get; set; }

        public List<ReceiptItemDetails> ReceiptItems { get; set; } = new List<ReceiptItemDetails>();

        public decimal SubTotal { get; set; }

        public decimal TaxRate { get; set; }

        public decimal Total { get; set; }
    }
}
