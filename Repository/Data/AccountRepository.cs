using API.Context;
using API.Models;
using API.ViewModel;
using System;
using System.Linq;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {

        }

        public int Register(RegisterVM registerVM)
        {
            var regEmployee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Salary = registerVM.Salary,
                Email = registerVM.Email,
                Gender = (Models.Gender)registerVM.Gender
            };         

            var regAccount = new Account
            {
                NIK = regEmployee.NIK,
                Password = registerVM.Password
            };

            var regEducation = new Education
            {
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = registerVM.UniversityId
            };
            var cekNIK = myContext.Employees.Any(e => e.NIK == regEmployee.NIK);
            var cekEmail = myContext.Employees.Any(e => e.Email == regEmployee.Email);
            var cekPhone = myContext.Employees.Any(e => e.Phone == regEmployee.Phone);
            if (cekNIK)
            {
                return 1;
            }
            else if (cekEmail)
            {
                return 2;
            }
            else if (cekPhone)
            {
                return 3;
            }
            else
            {
                myContext.Employees.Add(regEmployee);
                myContext.Accounts.Add(regAccount);
                myContext.Educations.Add(regEducation);
                myContext.SaveChanges();
                return 0;
            }
            
        }

        public string GenerateNIK()
        {
            if (myContext.Employees.ToList().Count == 0)
            {
                string incNIK = DateTime.Now.Year.ToString() + "001";
                return incNIK;
            }
            else
            {
                int lastNIK = Convert.ToInt32(myContext.Employees.ToList().LastOrDefault().NIK) % DateTime.Now.Year + 1;
                string incNIK = DateTime.Now.ToString("yyyy") + lastNIK.ToString("D3");
                return incNIK;
            }
        }
    }
}
