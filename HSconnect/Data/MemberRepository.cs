using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class MemberRepository : RepositoryBase<Member>, IMemberRepository
	{
		public MemberRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
		public void CreateMember(Member member) => Create(member);
		public Member GetMemberById(int memberId) => FindByCondition(m => m.Id == memberId).FirstOrDefault();

	}
}