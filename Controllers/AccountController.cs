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
    }
}
