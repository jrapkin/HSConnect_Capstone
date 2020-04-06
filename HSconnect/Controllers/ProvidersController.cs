using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HSconnect.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProvidersController : Controller
    {
        private IRepositoryWrapper _repo;

        public IdentityUser IdentityUser { get; private set; }

        public ProvidersController (IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult Index(int id)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var provider = _repo.Provider.FindByCondition(p => p.IdentityUserId == userId);
            //if there are charts that ties to this provider
            //link to services offered (add/edit services)
            //link to access to the partnerships (managed care orgs)
                _repo.Provider.GetProvider(id);

            return View(provider);
        }
       public IActionResult Create()
       {
            Provider provider = new Provider();
            {
                IdentityUser = IdentityUser;
            }
            return View(provider);
       }
       public IActionResult Create(Provider provider)
       {
            //current user
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //assign customer FK to userIdentityUser
            provider.IdentityUserId = userId;
            //add provider to providers table in DB
            _repo.Provider.Create(provider);
            _repo.Save();
            return RedirectToAction(nameof(Index));
       }
        public IActionResult Edit(int id)
        {
            Provider provider = new Provider();
            provider.Id = id;

            return View(provider);
        }
    }
}