using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HSconnect.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProvidersController : Controller
    {
        private IRepositoryWrapper _repo;
        public ProvidersController (IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}