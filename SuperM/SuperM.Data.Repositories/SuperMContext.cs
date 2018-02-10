using SuperM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperM.Data.Repositories
{
    public class SuperMContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public SuperMContext() : base() { }

        public SuperMContext(string connectingString) : base(connectingString) { }
        
    }
}
