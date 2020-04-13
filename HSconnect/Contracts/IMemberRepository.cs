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
		Member GetMemberByIdIncludeAll(int? memberId);

		Task <Member> GetMemberByIdIncludeAllAsync(int? memberId);

		Task<ICollection<Member>> GetMembersIncludeAll();
	
		ICollection<Member> GetMemberBySocialWorkerId(int socialWorkerId);

	}
}
