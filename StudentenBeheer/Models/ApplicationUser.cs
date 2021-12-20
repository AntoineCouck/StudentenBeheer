using Microsoft.AspNetCore.Identity;
using StudentenBeheer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentenBeheer.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }

    [ForeignKey("Language")]
    public string LanguageId { get; set; }
    public Language? Language { get; set; }

    public virtual Student ? Student { get; set; }
  

}

