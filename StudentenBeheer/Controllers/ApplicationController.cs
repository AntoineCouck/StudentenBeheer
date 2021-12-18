using Microsoft.AspNetCore.Mvc;
using StudentenBeheer.Areas.Identity.Data;
using StudentenBeheer.Data;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using StudentenBeheer.Services;

namespace StudentenBeheer.Controllers
{
    public class ApplicationController : Controller
    {
        protected readonly ApplicationUser _user;
        protected readonly ApplicationContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly ILogger<ApplicationController> _logger;

        protected ApplicationController(ApplicationContext context,
                                        IHttpContextAccessor httpContextAccessor,
                                        ILogger<ApplicationController> logger)
        {
            _context = context;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            //string? userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            //if (userName == null)
            //    userName = "-";
            //_user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            _user = SessionUser.GetUser(httpContextAccessor.HttpContext);

        }
    }
}


