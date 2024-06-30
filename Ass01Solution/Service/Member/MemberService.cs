using BusinessObject.Models;
using Repository.MemberRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Service
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepo _memberRepo = new MemberRepo();

        public MemberService()
        {
        }

        public Member? LoginUser(string email, string password)
        {
            var user = _memberRepo.GetMemberByEmailAndPassword(email, password);
            return user;
        }

        public bool RegisterUser(Member user)
        {
            return _memberRepo.CreateMember(user);
        }

        public bool UpdateUser(Member user)
        {
            return _memberRepo.UpdateMember(user);
        }

        public bool DeleteUser(int id)
        {
            return _memberRepo.DeleteMember(id);
        }

        public IEnumerable<Member> GetMembers()
        {
            return _memberRepo.GetAllMembers();
        }

        public Member? GetMember(int id)
        {
            return _memberRepo.GetMemberById(id);
        }
    }
}
