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
        private readonly AccountRepository _accountRepository;
        public AccountController(AccountRepository accountRepository) : base(accountRepository)
        {

        }

        [HttpPost]
        [Route("/Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            try
            {
                int insert = _accountRepository.Register(registerVM);
                return insert switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = registerVM, message = "Insert Data Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Insert Failed, NIK already exists!" }),
                    2 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Insert Failed, Email already exists!" }),
                    3 => BadRequest(new { status = HttpStatusCode.BadRequest, result = registerVM, message = "Insert Failed, Phone already exists!" })
                };

            }
            catch
            {
                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, result = registerVM, message = "Error Occured." });
            }
        }
    }
}
