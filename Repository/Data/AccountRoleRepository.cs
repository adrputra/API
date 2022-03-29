using API.Context;
using API.Models;
using API.ViewModel;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRoleRepository : GeneralRepository<MyContext,AccountRole,int>
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int SignManager(EmailVM emailVM)
        {
            var checkEmail = myContext.Employees.FirstOrDefault(e => e.Email == emailVM.Email);
            if (checkEmail != null)
            {
                AccountRole regAccountRole = new AccountRole
                {
                    NIK = checkEmail.NIK,
                    RoleID = 2
                };
                myContext.AccountRoles.Add(regAccountRole);
                myContext.SaveChanges();
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
