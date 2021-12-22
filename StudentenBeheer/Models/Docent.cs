using System.ComponentModel.DataAnnotations;

namespace StudentenBeheer.Models
{
    public class Docent
    {

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }


        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedAt { get; set; }



    }
}
