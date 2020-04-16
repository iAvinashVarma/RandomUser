using NUnit.Framework;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Model;
using RandomUserApi.Tests.Configuration;
using System;
using System.Globalization;

namespace RandomUserApi.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private readonly IUserRepository userRepository = null;

        public UserControllerTest()
        {
            userRepository = MockConfiguration.MockVehicleRepository.Object;
        }

        [Test, Order(1)]
        public void CheckIfAbleToReturnUserById()
        {
            var userId = 1;
            var user = userRepository.GetById(userId);

            Assert.IsNotNull(userId);
            Assert.IsInstanceOf<User>(user);
            Assert.AreEqual(DateTime.ParseExact("2020-04-15", "yyyy-MM-dd", new CultureInfo("en-US")), user.CreatedDate.Date);
            Assert.AreEqual(DateTime.ParseExact("2020-04-16", "yyyy-MM-dd", new CultureInfo("en-US")), user.ModifiedDate.Date);
        }

    }
}
