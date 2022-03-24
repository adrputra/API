using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gender = API.ViewModel.Gender;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            var checkEmail = myContext.Employees.SingleOrDefault(e => e.Email == loginVM.Email);
            if (checkEmail != null)
            {
                var checkPassword = myContext.Accounts.SingleOrDefault(e => e.NIK == checkEmail.NIK);
                if (checkPassword != null)
                {
                    if (checkPassword.Password == loginVM.Password)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 2;
            }
        }

        public int Register(RegisterVM registerVM)
        {
            string incNIK = GenerateNIK();
            var regEmployee = new Employee
            {
                NIK = incNIK,
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
                registerVM.NIK = incNIK;
                myContext.Employees.Add(regEmployee);
                myContext.Accounts.Add(regAccount);
                myContext.Educations.Add(regEducation);
                myContext.SaveChanges();

                var regProfiling = new Profiling
                {
                    NIK = regAccount.NIK,
                    EducationId = regEducation.ID
                };

                myContext.Profilings.Add(regProfiling);
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

        public IEnumerable GetMaster()
        {
            var masterData = (from emp in myContext.Employees
                             join acc in myContext.Accounts on emp.NIK equals acc.NIK
                             join pro in myContext.Profilings on acc.NIK equals pro.NIK
                             join edu in myContext.Educations on pro.EducationId equals edu.ID
                             join univ in myContext.Universities on edu.UniversityId equals univ.ID
                             select new
                             {
                                 NIK = emp.NIK,
                                 FullName = emp.FirstName + " " + emp.LastName,
                                 Phone = emp.Phone,
                                 Gender = ((Gender)emp.Gender).ToString(),
                                 Email = emp.Email,
                                 BirthDate = emp.BirthDate,
                                 Salary = emp.Salary,
                                 EducationId = pro.EducationId,
                                 GPA = edu.GPA,
                                 Degree = edu.Degree,
                                 UniversityName = univ.Name
                             }).ToList();
            return masterData;
        }

        public IEnumerable GetMaster(string NIK)
        {
            var masterData = (from emp in myContext.Employees
                              join acc in myContext.Accounts on emp.NIK equals acc.NIK
                              join pro in myContext.Profilings on acc.NIK equals pro.NIK
                              join edu in myContext.Educations on pro.EducationId equals edu.ID
                              join univ in myContext.Universities on edu.UniversityId equals univ.ID
                              where emp.NIK == NIK
                              select new
                              {
                                  NIK = emp.NIK,
                                  FullName = emp.FirstName + " " + emp.LastName,
                                  Phone = emp.Phone,
                                  Gender = ((Gender)emp.Gender).ToString(),
                                  Email = emp.Email,
                                  BirthDate = emp.BirthDate,
                                  Salary = emp.Salary,
                                  EducationId = pro.EducationId,
                                  GPA = edu.GPA,
                                  Degree = edu.Degree,
                                  UniversityName = univ.Name
                              }).ToList();
            return masterData;
        }
    }
}
