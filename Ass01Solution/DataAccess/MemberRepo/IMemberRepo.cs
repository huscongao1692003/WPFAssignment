using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MemberRepo
{
    public interface IMemberRepo
    {
        bool CreateMember(Member member);
        bool UpdateMember(Member member);
        bool DeleteMember(int id);
        Member? GetMemberById(int id);
        Member? GetMemberByEmailAndPassword(string email, string password);
        IEnumerable<Member> GetAllMembers();
    }
}
