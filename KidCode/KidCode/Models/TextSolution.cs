using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KidCode.Models
{
    public class TextSolution
    {
        [Key]
        public int SolutionId { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int ChallengeId { get; set; }
        public virtual TextChallenge TextChallenge { get; set; }
        
     
        public string StudentAnswer { get; set; }

        public string TeacherAnswer { get; set; }

        [Display(Name = "Punctaj obtinut")]
        [Range(0, 100, ErrorMessage = "Punctajul poate avea valori intre 0 si 100")]
        public int SolutionScore { get; set; }

        public bool StatusStudent { get; set; }

        public bool StatusTeacher { get; set; }



    }
}