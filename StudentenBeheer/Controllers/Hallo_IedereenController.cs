using Microsoft.AspNetCore.Mvc;

namespace StudentenBeheer.Controllers
{
    public class Hallo_IedereenController : Controller
    {
<<<<<<< HEAD

        public Hallo_IedereenController(ApplicationContext context, IHttpContextAccessor httpContextAccessor, ILogger<ApplicationController> logger) : base(context, httpContextAccessor, logger)
        {
            //
        }

=======
>>>>>>> parent of 9439f98 (Add one général controller)
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
