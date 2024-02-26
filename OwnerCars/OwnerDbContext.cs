using OwnerCars.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OwnerCars
{
    public class OwnerDbContext: DbContext
    {
         public DbSet<Owner> owners {  get; set; }
        public DbSet<Car> cars { get; set; }

    }
}
