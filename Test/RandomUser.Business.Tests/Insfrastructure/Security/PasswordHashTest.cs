using NUnit.Framework;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Insfrastructure.Security;
using RandomUser.Business.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUser.Business.Tests.Insfrastructure.Security
{
    [TestFixture]
    public class PasswordHashTest
    {
        private readonly List<UserAccount> UserAccounts = null;

        public PasswordHashTest()
        {
            var userAccountsDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Sample\Data\UserAccounts.json");
            UserAccounts = UserDataMock.GetUserAccountsWithAutoIncrement(userAccountsDataPath).ToList();
        }

        [Test]
        public void GenerateHashPasswordFromTextForStorage()
        {
            var hash = new PasswordHash("Abcd1234");
            byte[] hashBytes = hash.ToArray();
            var savedPasswordHash = Convert.ToBase64String(hashBytes);

            Assert.IsNotNull(savedPasswordHash);
            Assert.IsNotEmpty(savedPasswordHash);
        }

        [Test]
        public void CheckPasswordAgainstAStoredHash()
        {

            var passwordHash = UserAccounts.Single(u => u.Username.Equals("Avi", StringComparison.OrdinalIgnoreCase)).Password;
            var hashBytes = Convert.FromBase64String(passwordHash);
            var hash = new PasswordHash(hashBytes);

            var isAuthorized = hash.Verify("Abcd1234");
            Assert.IsTrue(isAuthorized);
        }
    }
}
