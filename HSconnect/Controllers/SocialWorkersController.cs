using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Security.Claims;
using HSconnect.Models;

namespace HSconnect.Controllers
{
    [Authorize(Roles = "Social Worker")]
    public class SocialWorkersController : Controller
    {
        private IRepositoryWrapper _repo;
        public SocialWorkersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_repo.SocialWorker.FindByCondition(p => p.IdentityUserId == userId).Any())
            {
                SocialWorker socialWorker = _repo.SocialWorker.GetSocialWorker(userId);

                return View();
            }
            else
            {
                return RedirectToAction("Create");
            }

        }
    }
}