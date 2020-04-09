using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IMemberRepository : IRepositoryBase<Member>
	{
		public void CreateMember(Member member);
		public Member GetMemberById(int? memberId);

		public Task<Member> GetMemberIncludeAll(int? memberId);
		public Task<ICollection<Member>> GetMembersIncludeAll();

	}
}
