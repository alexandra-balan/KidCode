using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string StudentLastName { get; set; }
        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        public string StudentFirstName { get; set; }

        public int Score { get; set; }

        

        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

       // public int SchoolId { get; set; }
       // public virtual School School { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public IEnumerable<SelectListItem> Teachers { get; set; }
        //public IEnumerable<SelectListItem> Schools { get; set; }
        public virtual ICollection<TextSolution> TextSolutions { get; set; }

    }
}