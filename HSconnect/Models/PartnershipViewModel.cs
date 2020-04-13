using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
    public class PartnershipViewModel
    {
        public List<ManagedCareOrganization> ManagedCareOrganizations { get; set; }
        public int ManagedCareOrganizationSelectionId { get; set; }
        public int PartnershipId { get; set; }
    }
}
