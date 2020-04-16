using Newtonsoft.Json;
using NUnit.Framework;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUser.Business.Tests.Model
{
    [TestFixture]
    public class UserTest
    {
        private readonly List<User> Users = null;

        public UserTest()
        {
            var usersDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Sample\Data\Users.json");
            Users = UserDataMock.GetUsersWithAutoIncrement(usersDataPath).ToList();
        }

        [Test, Order(1)]
        public void CheckIfUsersAreLoadedAvailable()
        {
            var hasUsers = Users.Any();
            Assert.IsTrue(hasUsers);
        }
    }
}
