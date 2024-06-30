using BusinessObject;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MemberRepo
{
    public class MemberRepo : IMemberRepo
    {
        private readonly ShoppingCartContext _context = new ShoppingCartContext();

        public MemberRepo()
        {
        }

        public bool CreateMember(Member member)
        {
            if (_context.Members.Any(m => m.Email.Equals(member.Email)))
            {
                return false;
            }

            _context.Members.Add(member);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateMember(Member member)
        {
            var existingMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);

            if (existingMember == null)
            {
                return false;
            }

            existingMember.CompanyName = member.CompanyName;
            existingMember.Country = member.Country;
            existingMember.City = member.City;
            existingMember.Password = member.Password;

            _context.Members.Update(existingMember);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteMember(int id)
        {
            var member = _context.Members.FirstOrDefault(m => m.MemberId == id);
            if (member == null)
            {
                return false;
            }

            _context.Members.Remove(member);
            return _context.SaveChanges() > 0;
        }
        
        public Member? GetMemberById(int id)
        {
            return _context.Members.AsNoTracking().FirstOrDefault(x => x.MemberId == id);
        }

        public Member? GetMemberByEmailAndPassword(string email, string password)
        {
            return _context.Members.FirstOrDefault(m => m.Email.Equals(email) && m.Password.Equals(password));
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.AsNoTracking().ToList();
        }
    }
}
