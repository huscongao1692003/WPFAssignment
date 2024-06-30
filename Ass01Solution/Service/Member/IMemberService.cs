using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Service
{
    public interface IMemberService
    {
        Member? LoginUser(string email, string password);
        bool RegisterUser(Member user);
        bool UpdateUser(Member user);
        bool DeleteUser(int id);
        IEnumerable<Member> GetMembers();
        Member? GetMember(int id);
    }
}
