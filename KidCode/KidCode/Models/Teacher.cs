using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string TeacherLastName { get; set; }
        [Required(ErrorMessage = "Prenumele este obligatoriu")]
        public string TeacherFirstName { get; set; }

        

        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public IEnumerable<SelectListItem> Schools { get; set; }
       // public virtual ICollection<TextChallenge> TextChallenges { get; set; } 

        public string FullDesc
        {
            get
            {
                return TeacherFirstName + " " + TeacherLastName + ", " + School.SchoolName.ToString();
            }
        }
    }
}