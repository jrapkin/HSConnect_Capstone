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
		public async Task<Member> GetMemberByIdIncludeAll(int? memberId)
		{
			return await FindByCondition(m => m.Id == memberId).Include(a => a.Address)
				.Include(c => c.Chart)
				.Include(m => m.ManagedCareOrganization).FirstOrDefaultAsync();
		}

		public async Task<ICollection<Member>> GetMembersIncludeAll()
		{
			return await FindAll().Include(a => a.Address)
								  .Include(m => m.ManagedCareOrganization)
								  .Include(c => c.Chart)
								  .ThenInclude(s => s.ServiceOffered).ToListAsync();
		}
		public ICollection<Member> GetMemberBySocialWorkerId(int socialWorkerId)
		{
			return FindByCondition(m => m.Chart.SocialWorkerId == socialWorkerId).ToList();
		}
	}
}