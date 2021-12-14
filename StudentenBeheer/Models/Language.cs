using System.ComponentModel.DataAnnotations;

namespace StudentenBeheer.Models
{
    public class Language
    {
        public static Dictionary<string, Language> LanguageDictionary { get; set; }

        public static string[] SupportedLanguages { get; set; }

        public static List<Language> Systemlanguages { get; set; }

        public static List<Language> AllLanguages { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string Id { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        public string Cultures { get; set; }
        public Boolean IsSystemLanguage { get; set; }
    }
}
