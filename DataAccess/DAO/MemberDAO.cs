using BusinessObject;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetMemberList()
        {
            var mem = new List<Member>();
            try
            {
                using var context = new SalesDBContext();
                mem = context.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public Member GetMemberById(int id)
        {
            Member mem = null;
            try
            {
                using var context = new SalesDBContext();
                mem = context.Members.SingleOrDefault(c => c.MemberId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public Member GetMemberByEmail(string email)
        {
            Member mem = null;
            try
            {
                using var context = new SalesDBContext();
                mem = context.Members.SingleOrDefault(c => c.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return mem;
        }

        public void Add(Member member)
        {
            try
            {
                Member mem = GetMemberById(member.MemberId);
                if (mem == null)
                {
                    using var context = new SalesDBContext();
                    context.Members.Add(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Member member)
        {
            try
            {
                Member mem = GetMemberById(member.MemberId);
                if (mem != null)
                {
                    using var context = new SalesDBContext();
                    context.Members.Update(member);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                Member mem = GetMemberById(id);
                if (mem != null)
                {
                    using var context = new SalesDBContext();
                    context.Members.Remove(mem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Member Login(string email, string password)
        {
            Member member = null;
            try
            {
                IEnumerable<Member> members = GetMemberList().Append(GetDefaultMember());
                member = members.SingleOrDefault(mb => mb.Email.Equals(email) && mb.Password.Equals(password));
                if (member == null)
                {
                    throw new Exception("Login failed! Please check your email and password!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return member;
        }

        public Member GetDefaultMember()
        {
            Member Default = null;
            using (StreamReader r = new StreamReader("appsettings.json"))
            {
                string json = r.ReadToEnd();
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
                string email = config["account:defaultAccount:email"];
                string password = config["account:defaultAccount:password"];
                Default = new Member
                {
                    MemberId = 0,
                    Email = email,
                    CompanyName = "",
                    City = "",
                    Country = "",
                    Password = password,
                };
            }
            return Default;
        }
    }
}
