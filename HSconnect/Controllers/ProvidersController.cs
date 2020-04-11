using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HSconnect.Contracts;
using HSconnect.Models;
using HSconnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MimeKit;

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
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int providerId = _repo.Provider.GetProviderByUserId(userId).Id;

            //if there are charts that ties to this provider by services provided 
            IEnumerable<Chart> providerCharts = _repo.Chart.GetChartsByProvider(providerId);

            return View(providerCharts);
        }
        public IActionResult DisplayReferrals(bool? referralStatus)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int providerId = _repo.Provider.GetProviderByUserId(userId).Id;

            //if there are charts that ties to this provider by services provided 
            IEnumerable<Chart> providerCharts = _repo.Chart.GetChartsByProvider(providerId).Where(c => c.ReferralAccepted == referralStatus);

            return View(providerCharts);
        }
        public IActionResult Index()
        {
            //link to services offered (add/edit services) **MOVED THIS LINK into DETAILS**
            //link to access to the partnerships (managed care orgs) **Will add this link in the header)

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_repo.Provider.FindByCondition(p => p.IdentityUserId == userId).Any())
            {
                var provider = _repo.Provider.FindByCondition(p => p.IdentityUserId == userId).FirstOrDefault();
                provider.Charts = _repo.Chart.GetChartsByProvider(provider.Id);
                return View(provider);
            }
            else
            {
                return RedirectToAction("Create");
            }

        }
        public IActionResult Details()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Provider provider = _repo.Provider.GetProviderByUserId(userId);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public IActionResult Edit()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Provider provider = _repo.Provider.GetProviderByUserId(userId);

            return View(provider);
        }
        [HttpPost]
        public IActionResult Edit(Provider provider)
        {
            _repo.Provider.Update(provider);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> DisplayServices()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int providerId = _repo.Provider.GetProviderByUserId(userId).Id;
            IEnumerable<ServiceOffered> servicesOffered = await _repo.ServiceOffered.GetServicesOfferedIncludeAllAsync(providerId);

            return View(servicesOffered);
        }
        public IActionResult DisplayServiceOfferedDetails(int id)
        {
            ServiceOffered serviceOffered = _repo.ServiceOffered.GetServicesOfferedIncludeAll().Where(s => s.Id == id).FirstOrDefault();
            return View(serviceOffered);
        }
        public IActionResult CreateServiceOffered()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ServiceOfferedViewModel viewModel = new ServiceOfferedViewModel();

            viewModel.Provider = _repo.Provider.GetProviderByUserId(userId);
            _repo.Category.GetAllCategories();
            //_repo.Demographic.GetAllDemographics();
            _repo.Service.GetAllServices();

            ViewData["Categories"] = new SelectList(_repo.Category.GetAllCategories(), "Id", "Name");

            Dictionary<int, string> genderDictionary = CreateNullableBoolDictionary("Co-ed", "Male", "Female");
            ViewData["Genders"] = new SelectList(genderDictionary, "Key", "Value");
            Dictionary<int, string> familyFriendly = CreateNullableBoolDictionary("Not Applicable", "Family Friendly", "Individual");
            ViewData["FamilySize"] = new SelectList(familyFriendly, "Key", "Value");
            Dictionary<int, string> smokingAllowed = CreateNullableBoolDictionary("Not Applicable", "Smoking Allowed", "No Smoking");
            ViewData["Smoking"] = new SelectList(smokingAllowed, "Key", "Value");
            Dictionary<int, string> ageSensitive = CreateNullableBoolDictionary("Not Applicable", "Above 60", "18 and up");
            ViewData["AgeSensitive"] = new SelectList(ageSensitive, "Key", "Value");

            ViewData["Services"] = new SelectList(_repo.Service.GetAllServices(), "Id", "Name");

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateServiceOffered(ServiceOfferedViewModel resultsFromForm)
        {
            try
            {
                if (_repo.Address.GetByAddress(resultsFromForm.Address) == null)
                {
                    _repo.Address.CreateAddress(resultsFromForm.Address);
                    _repo.Save();
                }
                else
                {
                    resultsFromForm.Address = _repo.Address.GetByAddress(resultsFromForm.Address);
                }
                Demographic demographicToAdd = new Demographic();
                demographicToAdd.IsMale = ConvertToNullableBool(resultsFromForm.IsMale);
                demographicToAdd.FamilyFriendly = ConvertToNullableBool(resultsFromForm.FamilySelection);
                demographicToAdd.SmokingIsAllowed = ConvertToNullableBool(resultsFromForm.SmokingSelection);
                demographicToAdd.IsAgeSensitive = ConvertToNullableBool(resultsFromForm.AgeSensitive);
                _repo.Demographic.CreateDemographic(demographicToAdd);
                _repo.Save();

                //if(_repo.Service.FindByCondition(s => s.Id == service.Id) == null)
                //create new serviceOffered 
                _repo.ServiceOffered.CreateServiceOffered(resultsFromForm.Cost, resultsFromForm.Provider, resultsFromForm.Category, resultsFromForm.Address, demographicToAdd, resultsFromForm.Service);
                _repo.Save();


                return RedirectToAction(nameof(DisplayServices));
            }
            catch
            {
                return View(resultsFromForm);
            }
        }

    
        public IActionResult EditServiceOffered(int id)
        {
            ServiceOffered serviceOffered = _repo.ServiceOffered.GetServicesOfferedIncludeAll(id).FirstOrDefault();
            ServiceOfferedViewModel serviceOfferedViewModel = new ServiceOfferedViewModel();
            serviceOfferedViewModel.ServiceOfferedId = serviceOffered.Id;
            serviceOfferedViewModel.Address = serviceOffered.Address;
            serviceOfferedViewModel.Category = serviceOffered.Category;
            serviceOfferedViewModel.Demographic = serviceOffered.Demographic;
            serviceOfferedViewModel.Service = serviceOffered.Service;
            serviceOfferedViewModel.AgeSensitive = ConvertNullableBoolToInt(serviceOffered.Demographic.IsAgeSensitive);
            serviceOfferedViewModel.FamilySelection = ConvertNullableBoolToInt(serviceOffered.Demographic.FamilyFriendly);
            serviceOfferedViewModel.GenderSelection = ConvertNullableBoolToInt(serviceOffered.Demographic.IsMale);
            serviceOfferedViewModel.SmokingSelection = ConvertNullableBoolToInt(serviceOffered.Demographic.SmokingIsAllowed);
            serviceOfferedViewModel.Cost = serviceOffered.Cost;
            ViewData["Categories"] = new SelectList(_repo.Category.GetAllCategories(), "Id", "Name");
            Dictionary<int, string> genderDictionary = CreateNullableBoolDictionary("Co-ed", "Male", "Female");
            ViewData["Genders"] = new SelectList(genderDictionary, "Key", "Value");
            Dictionary<int, string> familyFriendly = CreateNullableBoolDictionary("Not Applicable", "Family Friendly", "Individual");
            ViewData["FamilySize"] = new SelectList(familyFriendly, "Key", "Value");
            Dictionary<int, string> smokingAllowed = CreateNullableBoolDictionary("Not Applicable", "Smoking Allowed", "No Smoking");
            ViewData["Smoking"] = new SelectList(smokingAllowed, "Key", "Value");
            Dictionary<int, string> ageSensitive = CreateNullableBoolDictionary("Not Applicable", "Above 60", "18 and up");
            ViewData["AgeSensitive"] = new SelectList(ageSensitive, "Key", "Value");
            ViewData["Services"] = new SelectList(_repo.Service.GetAllServices(), "Id", "Name");
            serviceOffered.Provider = new Provider();
            return View(serviceOfferedViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditServiceOffered(ServiceOfferedViewModel serviceOfferedViewModel)
        {
            ServiceOffered serviceOffered = new ServiceOffered();
            serviceOffered.AddressId = serviceOfferedViewModel.Address.Id;
            serviceOffered.Address = serviceOfferedViewModel.Address;
            serviceOffered.CategoryId = serviceOfferedViewModel.Category.Id;
            serviceOffered.Category = _repo.Category.FindByCondition(c => c.Id == serviceOfferedViewModel.Category.Id).FirstOrDefault();
            serviceOffered.Cost = serviceOfferedViewModel.Cost;
            serviceOffered.DemographicId = serviceOfferedViewModel.Demographic.Id;
            serviceOffered.Demographic = new Demographic();
            serviceOffered.Demographic.Id = serviceOfferedViewModel.Demographic.Id;
            serviceOffered.Demographic.FamilyFriendly = ConvertToNullableBool(serviceOfferedViewModel.FamilySelection);
            serviceOffered.Demographic.IsAgeSensitive = ConvertToNullableBool(serviceOfferedViewModel.AgeSensitive);
            serviceOffered.Demographic.IsMale = ConvertToNullableBool(serviceOfferedViewModel.GenderSelection);
            serviceOffered.Demographic.SmokingIsAllowed = ConvertToNullableBool(serviceOfferedViewModel.SmokingSelection);
            serviceOffered.Id = serviceOfferedViewModel.ServiceOfferedId;
            serviceOffered.Provider = _repo.Provider.GetProviderByUserId(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            serviceOffered.ProviderId = serviceOffered.Provider.Id;
            serviceOffered.ServiceId = serviceOfferedViewModel.Service.Id;
            serviceOffered.Service = _repo.Service.FindByCondition(s => s.Id == serviceOfferedViewModel.Service.Id).FirstOrDefault();
            _repo.ServiceOffered.Update(serviceOffered);
            _repo.Address.Update(serviceOffered.Address);
            _repo.Demographic.Update(serviceOffered.Demographic);
            _repo.Save();
            return RedirectToAction(nameof(DisplayServices));
        }
        public IActionResult DeleteServiceOffered(int id)
        {
            ServiceOffered serviceOffered = _repo.ServiceOffered.GetServiceOffered(id);
            return View(serviceOffered);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteServiceOffered(ServiceOffered serviceOffered)
        {
            ServiceOffered serviceOfferedToBeDeleted = _repo.ServiceOffered.GetServicesOfferedIncludeAll().FirstOrDefault(s => s.Id == serviceOffered.Id);
            _repo.ServiceOffered.Delete(_repo.ServiceOffered.GetServiceOffered(serviceOfferedToBeDeleted.Id));
            _repo.Address.Delete(_repo.Address.GetAddressById(serviceOfferedToBeDeleted.AddressId.Value));
            _repo.Demographic.Delete(_repo.Demographic.FindByCondition(d => d.Id == serviceOfferedToBeDeleted.DemographicId).FirstOrDefault());
            _repo.Save();
            return RedirectToAction(nameof(DisplayServices));

        }
        public IActionResult DisplayPartnerships()
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int providerId = _repo.Provider.GetProviderByUserId(userId).Id;
            var partnerships = _repo.Partnership.GetPartnershipsTiedToProviderIncludeAll(providerId);
            return View(partnerships);
        }
        public IActionResult CreatePartnership()
        {
            PartnershipViewModel partnershipViewModel = new PartnershipViewModel();
            partnershipViewModel.ManagedCareOrganizations = _repo.ManagedCareOrganization.GetAllManagedCareOrganizations().ToList();
            return View(partnershipViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePartnership(PartnershipViewModel partnershipViewModel)
        {
            if (ModelState.IsValid)
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Provider provider = _repo.Provider.GetProviderByUserId(userId);
                ManagedCareOrganization managedCareOrganization = _repo.ManagedCareOrganization.GetAllManagedCareOrganizations().Where(m => m.Id == partnershipViewModel.ManagedCareOrganizationSelectionId).FirstOrDefault();
                managedCareOrganization.Address = _repo.Address.GetAddressById(managedCareOrganization.AddressId.Value);
                try
                {
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
            PartnershipViewModel partnershipViewModel = new PartnershipViewModel();
            partnershipViewModel.PartnershipId = id;
            partnershipViewModel.ManagedCareOrganizations = _repo.ManagedCareOrganization.GetAllManagedCareOrganizations().ToList();
            return View(partnershipViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPartnership(PartnershipViewModel partnershipViewModel)
        {
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int providerId = _repo.Provider.GetProviderByUserId(userId).Id;
            ManagedCareOrganization managedCareOrganization = _repo.ManagedCareOrganization.GetAllManagedCareOrganizations().Where(m => m.Id == partnershipViewModel.ManagedCareOrganizationSelectionId).FirstOrDefault();
            Partnership partnership = new Partnership()
            {
                Id = partnershipViewModel.PartnershipId,
                ProviderId = providerId,
                Provider = _repo.Provider.GetProvider(providerId),
                ManagedCareOrganizationId = managedCareOrganization.Id,
                ManagedCareOrganization = managedCareOrganization
            };
            _repo.Partnership.Update(partnership);
            _repo.Save();
            return RedirectToAction(nameof(DisplayPartnerships));
        }
        public IActionResult DeletePartnership(int id)
        {
            Partnership partnership = _repo.Partnership.GetPartnership(id);
            partnership.ManagedCareOrganization = _repo.ManagedCareOrganization.GetAllManagedCareOrganizations().Where(m => m.Id == partnership.ManagedCareOrganizationId).FirstOrDefault();
            partnership.ManagedCareOrganization.Address = _repo.Address.GetAddressById(partnership.ManagedCareOrganization.AddressId.Value);
            return View(partnership);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePartnership(Partnership partnership)
        {
            try
            {
                _repo.Partnership.Delete(partnership);
                _repo.Save();
                return RedirectToAction(nameof(DisplayPartnerships));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult SendMassMessage()
        {
            Message message = new Message();
            message.UserFromID = this.User.FindFirstValue(ClaimTypes.Name);
            return View(message);
        }
        [HttpPost]
        public IActionResult SendMassMessage(Message message)
        {
            List<string> recipients = message.UserToId.Split(", ").ToList();
            MimeMessage emailToSend = MailKitAPI.CreateEmail(message.UserFromID, recipients, message.MessageContent);
            MailKitAPI.SendEmail(emailToSend, API_Keys.EmailAddress, API_Keys.EmailPassword);
            _repo.Message.Update(message);
            _repo.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult ToggleReferralStatus(int referralId)
        {
            Chart referral = new Chart();
            if (!referral.ReferralAccepted.HasValue)
            {
                referral.ReferralAccepted = true;
            }
            else if (referral.ReferralAccepted.Value == true)
            {
                referral.ReferralAccepted = false;
            }
            else
            {
                referral.ReferralAccepted = null;
            }
            _repo.Chart.Update(referral);
            _repo.Save();
            return RedirectToAction(nameof(DisplayReferrals), referralId);
        }
        //MOVE TO BOTTOM WHEN DONE WITH CONTROLLER
        private List<Partnership> FindProvidersPartnerships(Provider provider)
        {
            return _repo.Partnership.FindByCondition(p => p.ProviderId == provider.Id).ToList();
        }
        private Dictionary<int, string> CreateNullableBoolDictionary(string nullValue, string trueValue, string falseValue)
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
            switch(resultFromForm)
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
        private int ConvertNullableBoolToInt(bool? nullableBool)
        {
            if (!nullableBool.HasValue)
            {
                return 0;
            }
            else if (nullableBool.Value == true)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}