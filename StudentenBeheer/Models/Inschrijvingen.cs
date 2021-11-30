using System.ComponentModel.DataAnnotations;

namespace StudentenBeheer.Models
{
    public class Inschrijvingen
    {
        [Required]
        public Module ?  Module { get; set; }
        [Required]
        public Student ? Student { get; set; }
        [Required]
        public DateTime InschrijvingsDatum { get; set; }
        [Required]
        public DateTime AfgelegdOp { get; set; }
        [Required]
        public string Resultaat { get; set; }


    }
}
