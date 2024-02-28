using Quiz2_Passport.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2_Passport
{
    public class PassportDbContext : DbContext
    {
        public DbSet<Passport> passports {  get; set; }
    }
}
