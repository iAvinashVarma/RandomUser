using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Contract.Services;
using RandomUser.Business.Entity.Model;
using RandomUser.Business.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RandomUser.Business.Concrete.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly List<UserAccount> UserAccounts = null;
        private readonly AppSettings AppSettings;

        public UserAccountService(IOptions<AppSettings> appSettings)
        {
            var userAccountsDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Data\UserAccounts.json");
            UserAccounts = UserDataMock.GetUserAccountsWithAutoIncrement(userAccountsDataPath).ToList();
            AppSettings = appSettings.Value;
        }

        public UserAccount Authenticate(string username, string password)
        {
            var userAccount = UserAccounts.SingleOrDefault(x => x.Username == username && x.Password == password);

            // Return null if user not found
            if (userAccount == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAccount.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            userAccount.Token = tokenHandler.WriteToken(token);
            return GetUserAccountByHashingPassword(userAccount);
        }

        public IEnumerable<UserAccount> GetAll()
        {
            return GetUserAccountsByHashingPassword(UserAccounts);
        }

        private IEnumerable<UserAccount> GetUserAccountsByHashingPassword(IEnumerable<UserAccount> userAccounts)
        {
            return userAccounts.Select(a => GetUserAccountByHashingPassword(a));
        }

        private UserAccount GetUserAccountByHashingPassword(UserAccount userAccount)
        {
            userAccount.Password = new string('*', userAccount.Password.Length);
            return userAccount;
        }
    }
}
