using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoDatabase.Domain
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Task { get; set; }

        [Required]
        public int Difficulty {  get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }

        public enum StatusEnum { Pending = 1, Done=2, Delegated=3}
    }
}
