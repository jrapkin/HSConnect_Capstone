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
                if (socialWorker.Charts != null)
                {
                    socialWorker.Charts.Where(c => c.SocialWorkerId == socialWorker.Id);
                }
                return View(socialWorker);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
        public IActionResult Create()
        {
            string email = this.User.FindFirstValue(ClaimTypes.Email);
            SocialWorker socialWorker = new SocialWorker();
            socialWorker.Email = email;
            return View(socialWorker);
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
            MemberViewModel viewModel = new MemberViewModel();
            viewModel.ManagedCareOrganizations = new SelectList(_repo.ManagedCareOrganization.GetAllManagedCareOrganizations().ToList(), "Id", "Name");
            Dictionary<int, string> genderDictionary = CreateBoolDictionary("Not Applicable", "Male", "Female");
            viewModel.Gender = new SelectList(genderDictionary, "Key", "Value");

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMember(MemberViewModel viewModelFromForm)
        {
            try
            {
                if (_repo.Address.GetByAddress(viewModelFromForm.Address) ==null)
                {
                    _repo.Address.CreateAddress(viewModelFromForm.Address);
                    _repo.Save();
                }
                else
                {
                    Address addressFromDb = _repo.Address.GetByAddress(viewModelFromForm.Address);
                    viewModelFromForm.Address.Id = addressFromDb.Id;
                }
                viewModelFromForm.Member.IsMale = ConvertToNullableBool(viewModelFromForm.GenderSelection);
                viewModelFromForm.Member.AddressId = viewModelFromForm.Address.Id;
                viewModelFromForm.Member.ManagedCareOrganizationId = viewModelFromForm.ManagedCareOrganizationId;              
                _repo.Member.CreateMember(viewModelFromForm.Member);
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModelFromForm);
            }
        }

        public async Task<IActionResult> EditMember(int? id)
        {
            MemberViewModel viewModel = new MemberViewModel();

            viewModel.Member = await _repo.Member.GetMemberByIdIncludeAll(id);
            viewModel.Address = await _repo.Address.GetAddressByIdAsync(viewModel.Member.AddressId);
            viewModel.ManagedCareOrganizations = new SelectList(_repo.ManagedCareOrganization.GetAllManagedCareOrganizations().ToList(), "Id", "Name");
            if (id == null)
            {
                return NotFound();
            }
           

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMember(MemberViewModel viewModelFromForm)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModelFromForm.SocialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);

            if (_repo.Address.FindByCondition(a => a.Id == viewModelFromForm.Address.Id).Any())
            {
                _repo.Address.Update(_repo.Address.GetAddressById(viewModelFromForm.Address.Id));
            }
            //Address address = _repo.Address.GetAddressById(viewModelFromForm.Address.Id);
            viewModelFromForm.Member.AddressId = viewModelFromForm.Address.Id;
            viewModelFromForm.Member.ManagedCareOrganizationId = viewModelFromForm.ManagedCareOrganizationId;
            _repo.Member.Update(viewModelFromForm.Member);
            _repo.Save();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DeleteMember(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var member = await _repo.Member.GetMemberByIdIncludeAll(id);
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
        public async Task<IActionResult> Resources()
        {
            var servicesOffered = await _repo.ServiceOffered.GetServiceOfferedIncludeAllAsync();
            return View(servicesOffered);
        }
        private Dictionary<int, string> CreateBoolDictionary(string nullValue, string trueValue, string falseValue)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>()
            {
                { 0, nullValue },
                { 1, trueValue },
                { 2, falseValue }
            };

            return dictionary;
        }
        private bool? ConvertToNullableBool(int resultFromForm)
        {
            switch (resultFromForm)
            {
                case 0:
                    return null;
                case 1:
                    return true;
                case 2:
                    return false;
            }

            return ConvertToNullableBool(resultFromForm);
        }
    }
}