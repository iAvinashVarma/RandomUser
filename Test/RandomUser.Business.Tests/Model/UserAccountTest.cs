using NUnit.Framework;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUser.Business.Tests.Model
{
    [TestFixture]
    public class UserAccountTest
    {
        private readonly List<UserAccount> UserAccounts = null;

        public UserAccountTest()
        {
            var userAccountsDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Sample\Data\UserAccounts.json");
            UserAccounts = UserDataMock.GetUserAccountsWithAutoIncrement(userAccountsDataPath).ToList();
        }

        [Test, Order(1)]
        public void CheckIfUserAccountsDataCanBeLoadedAndAvailable()
        {
            var hasUserAccounts = UserAccounts.Any();
            Assert.IsTrue(hasUserAccounts);
        }
    }
}
