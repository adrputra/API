using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public AccountRepository _accountRepository;
        private readonly MyContext myContext;
        public AccountController(AccountRepository accountRepository, MyContext myContext) : base(accountRepository)
        {
            this._accountRepository = accountRepository;
            this.myContext = myContext;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            //return Ok(_accountRepository.Register(registerVM));
            try
            {
                int register = _accountRepository.Register(registerVM);
                return register switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = registerVM, message = "Register Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Failed, NIK already exists!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Failed, Email already exists!" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Register Failed, Phone already exists!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Register Failed!" })
                };

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult Login(LoginVM loginVM)
        {
            try
            {
                var login = _accountRepository.Login(loginVM);
                return login switch
                {
                    "1" => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Wrong Password!" }),
                    "2" => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Email Not Found!" }),
                    "3" => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Email Found But No Account!" }),
                    _ => Ok(new { status = HttpStatusCode.OK, login, message = "Login Successfull" })

                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpGet("master/{NIK}")]
        public ActionResult GetMasterByID(string NIK)
        {
            try
            {
                var master = _accountRepository.GetMaster(NIK);
                return StatusCode(200, new { status = HttpStatusCode.OK, result = master, message = $"Get Master Data {NIK} Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [Authorize(Roles = "Director,Manager")]
        [HttpGet("master")]
        public ActionResult GetMaster()
        {
            try
            {
                var master = _accountRepository.GetMaster();
                if(master == null)
                {
                    return StatusCode(400, new { status = HttpStatusCode.BadRequest, result = master, message = "No Data Found" });
                }
                else
                {
                    return StatusCode(200, new { status = HttpStatusCode.OK, result = master, message = "Get Master Data Successfully!" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(EmailVM emailVM)
        {
            //return Ok(_accountRepository.ForgotPassword(emailVM));

            try
            {
                var entry = _accountRepository.ForgotPassword(emailVM);
                return entry switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = emailVM, message = "New Password Request Successfull. Verification email has been sent." }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = emailVM, message = "Request Failed. Email Not Found!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = emailVM, message = "Request Failed. Email Found but cant send verification code!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Request Failed!" })

                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [HttpPost("changepassword")]
        public ActionResult ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var entry = _accountRepository.ChangePassword(changePasswordVM);
                return entry switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = changePasswordVM, message = "Password Changed Successfully" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePasswordVM, message = "Request Failed. Password Doesn't Match!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePasswordVM, message = "Request Failed. Token Already Expired!" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePasswordVM, message = "Request Failed. Token is Used!" }),
                    4 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePasswordVM, message = "Request Failed. Wrong Token!" }),
                    5 => BadRequest(new { status = HttpStatusCode.BadRequest, result = changePasswordVM, message = "Request Failed. Email Not Found!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Request Failed!" })

                };
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Success");
        }
    }
}
