using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2_Passport.Domain
{
    public class Passport
    {
        public int Id { get; set; }
        
        [StringLength(100)]
        public string First { get; set; }

        
        [StringLength(100)]
        public string Last { get; set; }

        
        [StringLength(10)]
        public string PassNum { get; set; }

        
        public string Expiration { get; set; }

       
        public bool IsValid { get; set; }


        [NotMapped]
        public string ValString
        {
            get { return IsValid.ToString(); }
        }
    }
}
