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
using Microsoft.AspNetCore.Mvc.Rendering;

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
            if (_repo.SocialWorker.FindByCondition(s => s.IdentityUserId == userId).Any())
            {
                SocialWorker socialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
                socialWorker.Charts.Where(c => c.SocialWorkerId == socialWorker.Id);
                return View();
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SocialWorker socialWorker)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    socialWorker.IdentityUserId = userId;
                    _repo.SocialWorker.CreateSocialWorker(socialWorker);
                    _repo.Save();
                }
                catch
                {
                    return View(socialWorker);
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateMember()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMember(Member member, Address address, Demographic demographic, ManagedCareOrganization managedCareOrganization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    member.AddressId = address.Id;
                    member.DemographicId = demographic.Id;
                    member.ManagedCareOrganizationId = managedCareOrganization.Id;
                    _repo.Member.CreateMember(member);
                    _repo.Save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(member);
                }
            }
            else
            {
                //if we got this far something went wrong
                return View();
            }
        }
        public IActionResult EditMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = _repo.Member.GetMemberIncludeAll(id);

            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMember(int? id, Member member)
        {
            _repo.Member.Update(member);
            _repo.Save();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = await _repo.Member.GetMemberIncludeAll(id);
            return View(member);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDeleteMember(Member member)
        {
            if (member == null)
            {
                return NotFound();
            }
            _repo.Member.Delete(member);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateReferral(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = _repo.Member.GetMemberById(id);
            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReferral(Chart chart, Member member, ServiceOffered selectedServices)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var socialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
                    chart.Member = member;
                    chart.ServiceOffered = selectedServices;
                    chart.SocialWorker = socialWorker;
                    _repo.Chart.CreateChart(chart);
                    _repo.Save();
                }
                catch
                {
                    return View(chart);
                }
            }
            //if we got here its broken
            return View();
        }
        public async Task<IActionResult> ViewMemberReferrals(int? id)
        {
            var charts = await _repo.Chart.GetChartsByMemberId(id);
            return View(charts);
        }
        public async Task<IActionResult> ViewResources()
        {
            var servicesOffered = await _repo.ServiceOffered.GetServiceOfferedIncludeAllAsync();
            return View(servicesOffered);
        }
        private Dictionary<bool?, string> CreateBoolDictionary(string nullValue, string falseValue, string trueValue)
        {
            Dictionary<bool?, string> dictionary = new Dictionary<bool?, string>()
            {
                { null, nullValue },
                { true, trueValue },
                { false, falseValue }
            };
            return dictionary;
        }
    }
}