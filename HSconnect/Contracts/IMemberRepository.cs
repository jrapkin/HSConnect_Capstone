using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IMemberRepository : IRepositoryBase<Member>
	{
		void CreateMember(Member member);
		Member GetMemberById(int? memberId);

		Task<Member> GetMemberByIdIncludeAll(int? memberId);

		Task<ICollection<Member>> GetMembersIncludeAll();

	}
}
