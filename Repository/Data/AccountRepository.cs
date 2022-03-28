using API.Context;
using API.Models;
using API.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
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
                    if (BCrypt.Net.BCrypt.Verify(loginVM.Password,checkPassword.Password))
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
                Password = BCrypt.Net.BCrypt.HashPassword(registerVM.Password,BCrypt.Net.BCrypt.GenerateSalt(12))
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

        public int ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            var checkEmail = myContext.Employees.SingleOrDefault(e => e.Email == forgotPasswordVM.Email);
            if (checkEmail != null)
            {
                Random random = new Random();
                int OTP = random.Next(1000,9999);
                var account = myContext.Accounts.Find(checkEmail.NIK);
                account.OTP = OTP;
                account.ExpiredToken = DateTime.Now.AddMinutes(5);
                myContext.Entry(account).State = EntityState.Modified;
                myContext.SaveChanges();
                SendEmail();
                //SendEmail(forgotPasswordVM.Email, OTP);
                //if (SendEmail(forgotPasswordVM.Email, OTP))
                //{
                //    return 0;
                //}
                //else
                //{
                //    return 2;
                //}
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public void SendEmail()
        {
            string to = "ricardoandra17@gmail.com";
            string from = "8andraputra@gmail.com";
            MailMessage message = new MailMessage(from, to);

            string mailbody = $"You have requested new password. Do not give this code authentication to anyone. OTP : 5555";
            message.Subject = "Forgot Password OTP";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("8andraputra@gmail.com", "gmail@adr");
            //client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
