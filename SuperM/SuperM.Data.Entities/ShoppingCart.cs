namespace SuperM.Data.Entities
{
    using System;
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset CheckoutTime { get; set; }

        public List<ShoppingItemDetails> ShoppingItems { get; set; } = new List<ShoppingItemDetails>();
        
    }
}
