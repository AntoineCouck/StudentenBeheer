using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace StudentenBeheer.Models
{

    // tussenklasse voor relatie m:m student → module

    [Authorize]
    public class Inschrijvingen
    {

        public int Id { get; set; }

        public Module? Module { get; set; }
        public int ModuleId { get; set; }

        public Student? Student { get; set; }
        public int StudentId { get; set; }

        
        [DataType(DataType.Date)]
        [Required]
        
        public DateTime InschrijvingsDatum { get; set; }
     
        [DataType(DataType.Date)]
       
        public DateTime ? AfgelegdOp { get; set; }
       
        public string ? Resultaat { get; set; }


    }
}
