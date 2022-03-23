using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : BaseController<University, UniversityRepository, int>
    {
        public UniversityController(UniversityRepository universityRepository) : base(universityRepository)
        {

        }
    }
}
