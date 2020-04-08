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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HSconnect.Controllers
{
    [Authorize(Roles = "Provider")]
    public class ProvidersController : Controller
    {
        private IRepositoryWrapper _repo;

        public IdentityUser IdentityUser { get; private set; }

        public ProvidersController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult DisplayReferrals()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var provider = _repo.Provider.FindByCondition(p => p.IdentityUserId == userId).SingleOrDefault();

            //if there are charts that ties to this provider by services provided 
            var providerCharts = _repo.Chart.GetChartsIncludeAll().ToList();
            providerCharts = GetChartsByProvider(providerCharts, provider.Id);

            return View(providerCharts);

        }
        public IActionResult Index()
        {
            //link to services offered (add/edit services) **MOVED THIS LINK into DETAILS**
            //link to access to the partnerships (managed care orgs) **Will add this link in the header)

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_repo.Provider.FindByCondition(p => p.IdentityUserId == userId).Any())
            {
                var provider = _repo.Provider.FindByCondition(p => p.IdentityUserId == userId).SingleOrDefault();

                //if there are charts that ties to this provider by services provided 
                var providerCharts = _repo.Chart.GetChartsIncludeAll().ToList();
                providerCharts = GetChartsByProvider(providerCharts, provider.Id);

                


                return View(providerCharts);
            }
            else
            {
                return RedirectToAction("Create");
            }
            
        }
        public IActionResult Details(int id)
        {
            var provider = _repo.Provider.GetProvider(id);
            //link to add/edit services offered
            var servicesOffering = _repo.ServiceOffered.FindByCondition(s => s.ProviderId == id);
            return View(provider);
        }
        public IActionResult CreateProvider()
        {
            Provider provider = new Provider();
            {
                IdentityUser = IdentityUser;
            }
            return View(provider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProvider(Provider provider)
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
        public IActionResult EditProvider(int id)
        {
            Provider provider = new Provider();
            provider.Id = id;

            return View(provider);
        }
        [HttpPost]
        public IActionResult EditProvider(int id, Provider provider)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Provider providerFromDB = _repo.Provider.GetProvider(id);
            providerFromDB.ProviderName = provider.ProviderName;
            providerFromDB.PhoneNumber = provider.PhoneNumber;
            providerFromDB.Email = provider.Email;
            providerFromDB.IdentityUserId = userId;

            _repo.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult DisplayServices(int id)//providerId
        {
            var servicesOffered = _repo.ServiceOffered.GetServicesOfferedByProvider(id);

            return View(servicesOffered);
        }
        public IActionResult CreateServiceOffered(int id)
        {
            Provider provider = _repo.Provider.GetProvider(id);
            _repo.Category.GetAllCategories();
            _repo.Demographic.GetAllDemographics();
            _repo.Service.GetAllServices();

            ViewData["Categories"] = new SelectList(_repo.Category.GetAllCategories(), "Id", "Name");

            Dictionary<bool?, string> genderDictionary = CreateNullableBoolDictionary("Co-ed", "Male", "Female");
            ViewData["Genders"] = new SelectList(genderDictionary, "Key", "Value");
            Dictionary<bool?, string> familyFriendly = CreateNullableBoolDictionary("Not Applicable", "Family Friendly", "Individual");
            ViewData["FamilySize"] = new SelectList(familyFriendly, "Key", "Value");
            Dictionary<bool?, string> smokingAllowed = CreateNullableBoolDictionary("Not Applicable", "Smoking Allowed", "No Smoking");
            ViewData["Smoking"] = new SelectList(smokingAllowed, "Key", "Value");

            ViewData["Services"] = new SelectList(_repo.Service.GetAllServices(), "Id", "Name");
                       
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateServiceOffered(Provider provider, Category category, Address address, Demographic demographic, Service service, string cost)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    if(_repo.Address.FindByCondition(a => a.Id == address.Id) == null)
                    {
                        _repo.Address.CreateAddress(address);
                        _repo.Save();
                    }
                    //create new serviceOffered 
                    _repo.ServiceOffered.CreateServiceOffered(provider, category, address, demographic, service);
                    _repo.Save();

                    return RedirectToAction(nameof(DisplayServices));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                //if we got this far, something went wrong
                return View();
            }
            
        }
        public IActionResult EditServiceOffered(int id)
        {
            ServiceOffered serviceOffered = new ServiceOffered();
            serviceOffered.Id = id;

            ViewData["Categories"] = new SelectList(_repo.Category.GetAllCategories(), "Id", "Name");

            Dictionary<bool?, string> genderDictionary = CreateNullableBoolDictionary("Co-ed", "Male", "Female");
            ViewData["Genders"] = new SelectList(genderDictionary, "Key", "Value");
            Dictionary<bool?, string> familyFriendly = CreateNullableBoolDictionary("Not Applicable", "Family Friendly", "Individual");
            ViewData["FamilySize"] = new SelectList(familyFriendly, "Key", "Value");
            Dictionary<bool?, string> smokingAllowed = CreateNullableBoolDictionary("Not Applicable", "Smoking Allowed", "No Smoking");
            ViewData["Smoking"] = new SelectList(smokingAllowed, "Key", "Value");

            ViewData["Services"] = new SelectList(_repo.Service.GetAllServices(), "Id", "Name");

            return View(serviceOffered);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditServiceOffered(int id, Provider provider)//serviceOfferedId and Provider
        {
            var serviceOffered = _repo.ServiceOffered.FindByCondition(s => s.ProviderId == provider.Id).FirstOrDefault();

            ServiceOffered updatedService = _repo.ServiceOffered.GetServiceOffered(id);

            //update category?
            updatedService.Category.Name = serviceOffered.Category.Name;
            
            //updated Address?
            updatedService.Address.StreetAddress = serviceOffered.Address.StreetAddress;
            updatedService.Address.City = serviceOffered.Address.City;
            updatedService.Address.County = serviceOffered.Address.County;
            updatedService.Address.State = serviceOffered.Address.State;
            updatedService.Address.ZipCode = serviceOffered.Address.ZipCode;

            //update demographic?
            updatedService.Demographic = serviceOffered.Demographic;

            //updated service 
            updatedService.Service.Name = serviceOffered.Service.Name;

            _repo.Save();
            return RedirectToAction(nameof(DisplayServices));
        }
        public IActionResult DeleteServiceOffered(int id)
        {
            ServiceOffered serviceOffered = new ServiceOffered();
            serviceOffered.Id = id;
            return View(serviceOffered);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteServiceOffered(int id, ServiceOffered serviceOffered)
        {
            try
            {
                _repo.ServiceOffered.Delete(serviceOffered);
                _repo.Save();
                return RedirectToAction(nameof(DisplayServices));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult DisplayPartnerships(int id)//providerId
        {
            var partnerships = _repo.Partnership.GetPartnershipsTiedToProvider(id);
            return View(partnerships);
        }
        public IActionResult CreatePartnership(int id)
        {
            Provider provider = _repo.Provider.GetProvider(id);
            _repo.ManagedCareOrganization.GetAllManagedCareOrganizations();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePartnership(Provider provider, ManagedCareOrganization managedCareOrganization)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //add new partnership to managedCareOrganization
                    _repo.Partnership.CreatePartnership(provider, managedCareOrganization);

                    _repo.Save();

                    return RedirectToAction(nameof(DisplayPartnerships));
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                //if we got this far, something went wrong
                return View();
            }
            
        }
        public IActionResult EditPartnership(int id)
        {
            Partnership partnership = new Partnership();
            partnership.Id = id;
            return View(partnership);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPartnership(int id, Provider provider)
        {
            var partnership = _repo.Partnership.FindByCondition(p => p.ProviderId == provider.Id).FirstOrDefault();

            Partnership updatedPartnership = _repo.Partnership.GetPartnership(id);
            updatedPartnership.ManagedCareOrganization.Name = partnership.ManagedCareOrganization.Name;

            _repo.Save();
            return RedirectToAction(nameof(Details));
        }
        public IActionResult DeletePartnership(int id)
        {
            Partnership partnership = new Partnership();
            partnership.Id = id;
            return View(partnership);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePartnership(int id, Partnership partnership)
        {
            try
            {
                _repo.Partnership.Delete(partnership);
                return RedirectToAction(nameof(DisplayPartnerships));
            }
            catch
            {
                return View();
            }
        }
        //MOVE TO BOTTOM WHEN DONE WITH CONTROLLER
        private List<Chart> GetChartsByProvider(List<Chart> charts, int providerId)
        {
            charts = charts.Where(c => c.ServiceOffered.ProviderId == providerId).ToList();
            return charts;
        }
        //MOVE TO BOTTOM WHEN DONE WITH CONTROLLER
        private List<Partnership> FindProvidersPartnerships(Provider provider)
        {
            return _repo.Partnership.FindByCondition(p => p.ProviderId == provider.Id).ToList();
        }
        private Dictionary<bool?, string> CreateNullableBoolDictionary(string nullValue, string falseValue, string trueValue)
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