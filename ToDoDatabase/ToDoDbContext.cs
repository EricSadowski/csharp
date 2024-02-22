using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDatabase.Domain;

namespace ToDoDatabase
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> todos {  get; set; }
    }
}
