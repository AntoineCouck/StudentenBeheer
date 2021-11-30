using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentenBeheer.Models
{
    public class Student
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = "Voornaam")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Achternaam")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Geboortedatum")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        //[Display(Name = "Geslacht")]
        //public string Gender { get; set; }


        [ForeignKey("Gender")]
        public char GenderId { get; set; }

        public Gender? Gender { get; set; }
    }

  
}
