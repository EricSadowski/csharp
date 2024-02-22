using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDatabase
{
    static class Global
    {
        public static ToDoDbContext context = new ToDoDbContext();
    }
}
