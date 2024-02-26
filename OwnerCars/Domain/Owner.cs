using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnerCars.Domain
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public byte[] Image { get; set; }
        public List<Car> Cars { get; set; }

        // New property to store the total number of cars
        [NotMapped]
        public int TotalCars { get; set; }
        public override string ToString()
        {
            return TotalCars.ToString();
        }

    }

    public class Car
    {
        public int Id { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owner Owner { get; set; }

    }
}
