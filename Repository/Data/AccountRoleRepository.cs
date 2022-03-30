using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
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
                var roles = new List<string>();
                var query= (from emp in myContext.Employees
                             join accrole in myContext.AccountRoles on emp.NIK equals accrole.NIK
                             join role in myContext.Roles on accrole.RoleID equals role.ID
                             where emp.Email == emailVM.Email
                             select new
                             {
                                 roles = role.Name
                             }).ToList();
                foreach (var item in query)
                {
                    roles.Add(item.roles);
                }

                if (!roles.Contains("Manager"))
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
                    return 2;
                }
               
            }
            else
            {
                return 1;
            }
        }
    }
}
