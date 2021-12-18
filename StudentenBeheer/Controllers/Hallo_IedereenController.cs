using Microsoft.AspNetCore.Mvc;
using StudentenBeheer.Data;

namespace StudentenBeheer.Controllers
{
    public class Hallo_IedereenController : ApplicationController
    {

        public Hallo_IedereenController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger) : base(context, httpContextAccessor, logger)
        {
            //
        }

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
