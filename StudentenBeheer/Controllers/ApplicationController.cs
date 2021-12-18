using Microsoft.AspNetCore.Mvc;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Data;

namespace StudentenBeheer.Controllers
{
    public class ApplicationController : Controller
    {
        protected readonly ApplicationUser _user;
        protected readonly ApplicationContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;


        public ApplicationController(ApplicationContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            string name = httpContextAccessor.HttpContext.User.Identity.Name;
           
            if (string.IsNullOrEmpty(name))

            {
                name = "-";       // ken de dummy-gebruiker toe
                _user = _context.Users.FirstOrDefault(u => u.UserName == name);

            }
                 
        }

    }
}


