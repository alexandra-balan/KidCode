using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KidCode.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }

        [Required(ErrorMessage = "Numele scolii este obligatoriu")]
        public string SchoolName { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }
        //public virtual ICollection<Student> Students { get; set; }

    }
}