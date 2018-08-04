using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Entities
{
    public class ShoppingItemDetails
    {
        public int ShoppingItemDetailsID { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Qty { get; set; }

        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

    }
}
