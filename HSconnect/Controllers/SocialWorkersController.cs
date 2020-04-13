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
using HSconnect.Models.ViewModels;
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
            SocialWorkerViewModel viewModel = new SocialWorkerViewModel();
            if (_repo.SocialWorker.FindByCondition(s => s.IdentityUserId == userId).Any())
            {
                viewModel.SocialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
                if (_repo.Chart.FindByCondition(c => c.SocialWorkerId == viewModel.SocialWorker.Id).Any())
                {
                    viewModel.Charts = _repo.Chart.GetChartsBySocialWorkerIdIncludeAll(viewModel.SocialWorker.Id).ToList();
                }
                
                return View(viewModel);
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
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                viewModelFromForm.SocialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
                if (_repo.Address.GetByAddress(viewModelFromForm.Address) == null)
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
                Chart newChart = new Chart()
                {
                    MemberId = viewModelFromForm.Member.Id,
                    SocialWorkerId = viewModelFromForm.SocialWorker.Id,
                    ServiceIsActive = viewModelFromForm.Member.IsActiveMember
                };
                _repo.Chart.CreateChart(newChart);
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
            if (id == null)
            {
                return NotFound();
            }
            MemberViewModel viewModel = new MemberViewModel();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            viewModel.SocialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
            viewModel.Member = await _repo.Member.GetMemberByIdIncludeAll(id);
            viewModel.Address = await _repo.Address.GetAddressByIdAsync(viewModel.Member.AddressId);
            viewModel.ManagedCareOrganizations = new SelectList(_repo.ManagedCareOrganization.GetAllManagedCareOrganizations().ToList(), "Id", "Name");

           

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

        public IActionResult DisplayMembers()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            SocialWorker socialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
            var charts = _repo.Chart.GetChartsBySocialWorkerIdIncludeAll(socialWorker.Id).ToList();
            List<Member> listOfMembersById = FilterMembersBySocialWorker(charts, socialWorker.Id).ToList();
            return View(listOfMembersById);
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
            Address addressToDelete = _repo.Address.GetAddressById(member.AddressId);
            _repo.Member.Delete(member);
            _repo.Save();
            _repo.Address.Delete(addressToDelete);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateReferral(int serviceOfferedId)
        {
            Chart chart = new Chart();
            chart.ServiceOfferedId = serviceOfferedId;
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var socialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
            ViewData["Members"] = new SelectList(_repo.Member.GetMemberBySocialWorkerId(socialWorker.Id), "Id", "Name");
            return View(chart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReferral(Chart chartFromForm)
        {
            try
            {
                Chart chart;
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var socialWorker = _repo.SocialWorker.GetSocialWorkerByUserId(userId);
                var memberCharts = _repo.Chart.GetChartsByMemberAndSocialWorkerId(socialWorker.Id, chartFromForm.MemberId).ToList();
                if (memberCharts.Count == 1)
                {
                    chart = memberCharts.First();
                }
                else
                {
                    chart = new Chart();
                }
                //set properties
                chart.MemberId = chartFromForm.MemberId;
                chart.ServiceOfferedId = chartFromForm.ServiceOfferedId;
                chart.ServiceIsActive = chartFromForm.ServiceIsActive;
                chart.SocialWorkerId = socialWorker.Id;
                chart.Date = DateTime.Now;

                if(memberCharts.Count == 1)
                {
                    _repo.Chart.CreateChart(chart);
                }
                else
                {
                    _repo.Chart.Update(chart);
                }
                _repo.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(chartFromForm);
            }
        }
        public async Task<IActionResult> ViewMemberReferrals(int? id)
        {
            var charts = await _repo.Chart.GetChartsByMemberId(id);
            return View(charts);
        }
        private Chart IsNewReferral(List<Chart> memberCharts)
        {
            foreach( var chart in memberCharts)
            {
                if (chart.ServiceOfferedId == null)
                {

                    return chart;
                }
            }
            return IsNewReferral(memberCharts);
        }
        public async Task<IActionResult> Resources()
        {
            ServiceOfferedViewModel viewModel = new ServiceOfferedViewModel();
            var servicesOfferedTemp = await _repo.ServiceOffered.GetServicesOfferedIncludeAllAsync();
            IEnumerable<ServiceOffered> servicesOffered = servicesOfferedTemp.ToList();
            viewModel.Providers = _repo.Provider.FindAll().ToList();
            viewModel.Providers.Insert(0, (new Provider()));
            viewModel.ServicesOffered = servicesOffered.ToList();
            viewModel.Categories = _repo.Category.GetAllCategories().ToList();
            viewModel.Categories.Insert(0, new Category());
            viewModel.Services = _repo.Service.GetAllServices().ToList();
            viewModel.Services.Insert(0, new Service());
            viewModel.GenderOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Co-Ed" }, { 2, "Male Only" }, { 3, "Female Only" } };
            viewModel.FamilyFriendlyOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Not Specified" }, { 2, "Family Friendly" }, { 3, "Individual" } };
            viewModel.SmokingOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Not Specified" }, { 2, "Smoking Is Allowed" }, { 3, "No Smoking" } };
            return View(viewModel);
        }
        public async Task<IActionResult> FilteredResources(ServiceOfferedViewModel filterResults)
        {
            ServiceOfferedViewModel viewModel = new ServiceOfferedViewModel();
            var servicesOfferedTemp = await _repo.ServiceOffered.GetServicesOfferedIncludeAllAsync();
            IEnumerable<ServiceOffered> servicesOffered = servicesOfferedTemp.ToList();
                if (filterResults.CategoryId != 0)
                {
                    servicesOffered = servicesOffered.Where(s => s.CategoryId == filterResults.CategoryId);
                }
                if (filterResults.ProviderId != 0)
                {
                    servicesOffered = servicesOffered.Where(s => s.ProviderId == filterResults.ProviderId);
                }
                if (filterResults.ServiceId != 0)
                {
                    servicesOffered = servicesOffered.Where(s => s.ServiceId == filterResults.ServiceId);
                }
                if (filterResults.GenderSelection != 0)
                {
                    switch (filterResults.GenderSelection)
                    {
                        case 1:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.IsMale == null);
                            break;
                        case 2:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.IsMale == true);
                            break;
                        case 3:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.IsMale == false);
                            break;
                        default:
                            break;
                    }
                }
                if (filterResults.FamilySelection != 0)
                {
                    switch (filterResults.FamilySelection)
                    {
                        case 1:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.FamilyFriendly == null);
                            break;
                        case 2:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.FamilyFriendly == true);
                            break;
                        case 3:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.FamilyFriendly == false);
                            break;
                        default:
                            break;
                    }
                }
                if (filterResults.SmokingSelection != 0)
                {
                    switch (filterResults.SmokingSelection)
                    {
                        case 1:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.SmokingIsAllowed == null);
                            break;
                        case 2:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.SmokingIsAllowed == true);
                            break;
                        case 3:
                            servicesOffered = servicesOffered.Where(s => s.Demographic.SmokingIsAllowed == false);
                            break;
                        default:
                            break;
                    }
                }
                try
                {
                    if (double.Parse(filterResults.MinCost) != 0)
                    {
                        servicesOffered = servicesOffered.Where(s => double.Parse(s.Cost) >= double.Parse(filterResults.MinCost));
                    }
                }
                catch
                {

                }
                try
                {
                    if (double.Parse(filterResults.MaxCost) != 0)
                    {
                        servicesOffered = servicesOffered.Where(s => double.Parse(s.Cost) <= double.Parse(filterResults.MaxCost));
                    }
                }
                catch
                {

                }
            viewModel.Providers = _repo.Provider.FindAll().ToList();
            viewModel.Providers.Insert(0, (new Provider()));
            viewModel.ServicesOffered = servicesOffered.ToList();
            viewModel.Categories = _repo.Category.GetAllCategories().ToList();
            viewModel.Categories.Insert(0, new Category());
            viewModel.Services = _repo.Service.GetAllServices().ToList();
            viewModel.Services.Insert(0, new Service());
            viewModel.GenderOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Co-Ed" }, { 2, "Male Only" }, { 3, "Female Only" } };
            viewModel.FamilyFriendlyOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Not Specified" }, { 2, "Family Friendly" }, { 3, "Individual" } };
            viewModel.SmokingOptions = new Dictionary<int, string>() { { 0, "" }, { 1, "Not Specified" }, { 2, "Smoking Is Allowed" }, { 3, "No Smoking" } };
            return View("Resources", viewModel);
        }
        private List<Member> FilterMembersBySocialWorker(List<Chart> charts, int socialWorkerId)
        {
            var members = charts.Where(m => m.SocialWorkerId == socialWorkerId).Select( m => m.Member).ToList();
            return members;
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