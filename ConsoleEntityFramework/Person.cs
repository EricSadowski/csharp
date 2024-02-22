using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEntityFramework
{
    class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Age { get; set; }

        public double Salary { get; set; }

    }

    class Home
    {
        public int Id { get; set; }
        public string Address { get; set; }
    }
}
