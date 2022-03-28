using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        public readonly AccountRepository _accountRepository;
        public AccountController(AccountRepository accountRepository) : base(accountRepository)
        {
            this._accountRepository = accountRepository;
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
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = registerVM, message = "Error Occured!" });
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
                    0 => Ok(new { status = HttpStatusCode.OK, result = loginVM, message = "Login Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Wrong Password!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Email Not Found!" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = loginVM, message = "Login Failed. Email Found But No Account!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Login Failed!" })

                };
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = loginVM, message = "Error Occured!" });
            }
        }

        [HttpGet("master/{NIK}")]
        public ActionResult GetMasterByID(string NIK)
        {
            try
            {
                var master = _accountRepository.GetMaster(NIK);
                return StatusCode(200, new { status = HttpStatusCode.OK, result = master, message = $"Get Master Data {NIK} Successfully!" });
            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Error Occured!" });
            }
        }

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
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Error Occured!" });
            }
        }

        [HttpPost("forgotpassword")]
        public ActionResult ForgotPassword(ForgotPasswordVM forgotPasswordVM)
        {
            //var entry = _accountRepository.ForgotPassword(forgotPasswordVM);
            return Ok(_accountRepository.ForgotPassword(forgotPasswordVM));

            //try
            //{
            //    var entry = _accountRepository.ForgotPassword(forgotPasswordVM);
            //    return entry switch
            //    {
            //        0 => Ok(new { status = HttpStatusCode.OK, result = forgotPasswordVM, message = "New Password Request Successfull. Verification email has been sent." }),
            //        1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = forgotPasswordVM, message = "Request Failed. No Email Found!" }),
            //        2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = forgotPasswordVM, message = "Request Failed. Email Found but cant send verification code!" }),
            //        _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Request Failed!" })

            //    };
            //}
            //catch
            //{
            //    return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = "Error Occured!" });
            //}
        }
    }
}
