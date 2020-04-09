using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HSconnect.Data
{
	public class MemberRepository : RepositoryBase<Member>, IMemberRepository
	{
		public MemberRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
		public void CreateMember(Member member) => Create(member);
		public Member GetMemberById(int? memberId) => FindByCondition(m => m.Id == memberId).FirstOrDefault();
		public Member GetMemberIncludeAll(int? memberId) => FindByCondition(m => m.Id == memberId).Include(a => a.Address)
																	  .Include(m => m.ManagedCareOrganization)
																	  .Include(d => d.Demographic)
																	  .Include(c => c.Chart).FirstOrDefault();
	
		public ICollection<Member> GetMembersIncludeAll() => FindAll().Include(a => a.Address).Include(m => m.ManagedCareOrganization)
																	  .Include(d => d.Demographic)
																	  .Include(c => c.Chart)
																	  .ThenInclude(s => s.ServiceOffered).ToList();
	}
}