using DataAccess.DAO;
using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public IEnumerable<Member> GetAllMember() => MemberDAO.Instance.GetMemberList();

        public Member GetmemberByEmail(string email) => MemberDAO.Instance.GetMemberByEmail(email);

        public Member GetmemberById(int id) => MemberDAO.Instance.GetMemberById(id);

        public void Insert(Member member) => MemberDAO.Instance.Add(member);

        public Member Login(string email, string password) => MemberDAO.Instance.Login(email, password);

        public void Remove(int id) => MemberDAO.Instance.Remove(id);

        public void Update(Member member) => MemberDAO.Instance.Update(member);
    }
}
