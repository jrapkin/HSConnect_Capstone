using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HSconnect.Controllers
{
    [Authorize(Roles = "Social Worker")]
    public class SocialWorkersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}