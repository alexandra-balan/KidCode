using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidCode.Models
{

    

    public class TextChallenge
    {
        [Key]
        public int ChallengeId { get; set; }

        [Required(ErrorMessage = "Enuntul este obligatoriu")]
        [Display(Name = "Enuntul problemei")]
        public string Question { get; set; }

        [Required(ErrorMessage = "Punctajul este obligatoriu")]
        [Display(Name = "Punctaj maxim")]
        [Range(0,100, ErrorMessage = "Punctajul poate avea valori intre 0 si 100")]
        public int ChallengeScore { get; set; }

        
       // public string Type { get; set; }

        //Profesorul care a postat problema (doar pt info, problema va putea fi vazuta si accesata de oricine)
        public int TeacherId { get; set; }
       // public virtual Teacher Teacher { get; set; }

       public virtual ICollection<TextSolution> TextSolutions { get; set; }

    }
}