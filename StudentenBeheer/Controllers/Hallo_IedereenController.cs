using Microsoft.AspNetCore.Mvc;

namespace StudentenBeheer.Controllers
{
    public class Hallo_IedereenController : Controller
    {
        public string Index()
        {
            return "Dit is de standaard pagina om iedereen welkom te heten";
        }

        public string Welkom(string voornaam, string achternaam)
        {
            return " Welkom " + voornaam + " " + achternaam;
        }

    }
}
