using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : BaseController<AccountRole, AccountRoleRepository, int>
    {
        public AccountRoleRepository _accountRoleRepository;
        private readonly MyContext myContext;
        public AccountRolesController(AccountRoleRepository accountRoleRepository, MyContext myContext) : base(accountRoleRepository)
        {
            this._accountRoleRepository = accountRoleRepository;
            this.myContext = myContext;
        }

        [Authorize(Roles = "Director")]
        [HttpPost("SignManager")]
        public ActionResult SignManager(EmailVM emailVM)
        {
            try
            {
                var sign = _accountRoleRepository.SignManager(emailVM);
                return sign switch
                {
                    0 => Ok(new { status = HttpStatusCode.OK, result = emailVM, message = "Sign Manager Successfull" }),
                    1 => BadRequest(new { status = HttpStatusCode.BadRequest, result = emailVM, message = "Sign Manager Failed, Email Not Found!" }),
                    _ => BadRequest(new { status = HttpStatusCode.BadRequest, message = "Sign Manager Failed!" })
                };
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { status = HttpStatusCode.InternalServerError, message = ex.Message });
            }
        }
            
    }
}
