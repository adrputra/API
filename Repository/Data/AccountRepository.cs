using API.Context;
using API.Models;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        public AccountRepository(MyContext myContext) : base(myContext)
        {

        }
    }
}
