using RandomUser.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomUser.Business.Contract.Services
{
    public interface IUserAccountService
    {
        UserAccount Authenticate(string username, string password);
        IEnumerable<UserAccount> GetAll();
    }
}
