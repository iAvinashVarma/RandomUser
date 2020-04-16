using NUnit.Framework;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Enum;
using RandomUser.Business.Model;
using RandomUserApi.Tests.Configuration;
using System;
using System.Globalization;
using System.Linq;

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
        public void CheckIfUsersAreAvailableAndMatchingTheTestDataCount()
        {
            var users = userRepository.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Count() == 75);
            Assert.IsInstanceOf<IQueryable<User>>(users);
        }

        [Test, Order(2)]
        public void CheckIfAbleToReturnUserById()
        {
            var userId = 1;
            var user = userRepository.GetById(userId);

            Assert.IsNotNull(userId);
            Assert.IsInstanceOf<User>(user);
            Assert.AreEqual(DateTime.ParseExact("2020-04-15", "yyyy-MM-dd", new CultureInfo("en-US")), user.CreatedDate.Date);
            Assert.AreEqual(DateTime.ParseExact("2020-04-16", "yyyy-MM-dd", new CultureInfo("en-US")), user.ModifiedDate.Date);
        }

        [Test, Order(3)]
        public void CheckIfAbleToReturnUsersByFirstName()
        {
            var firstName = "Jose";
            var users = userRepository.GetUsersByFirstName(firstName);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Count() >= 1);
            Assert.IsInstanceOf<IQueryable<User>>(users);
        }

        [Test, Order(4)]
        public void CheckIfAbleToReturnUsersByLastName()
        {
            var lastName = "Kiehn";
            var users = userRepository.GetUsersByLastName(lastName);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Count() >= 1);
            Assert.IsInstanceOf<IQueryable<User>>(users);
        }

        [Test, Order(5)]
        public void CheckIfAbleToReturnUsersByLimit()
        {
            var limit = 10;
            var users = userRepository.GetUsersByLimit(limit);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Count() == limit);
            Assert.IsInstanceOf<IQueryable<User>>(users);
        }

        [Test, Order(6)]
        public void CheckIfAbleToAddNewUser()
        {
            var user = new User
            {
                Title = "Mr",
                FirstName = "Avi",
                LastName = "Varma",
                Gender = Gender.Male,
                Email = "iavi@gmail.com",
                Address = "NZ",
                Balance = "$100209.99",
                DateOfBirth = new DateTime(1990, 4, 2)
            };
            userRepository.Add(user);

            var users = userRepository.GetUsersByFirstName("Avi");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Select(u => u.Email).Any());
            Assert.IsTrue(users.Select(u => u.Email).Any(e => e.Equals("iavi@gmail.com", StringComparison.OrdinalIgnoreCase)));
        }

        [Test, Order(7)]
        public void CheckIfAbleToUpdateUser()
        {
            var user = new User
            {
                Id = 75,
                Title = "Mr",
                FirstName = "Pruthvi",
                LastName = "Varma",
                Gender = Gender.Male,
                Email = "ipru@gmail.com",
                Address = "IN",
                Balance = "$129000.99",
                DateOfBirth = new DateTime(1988, 4, 13)
            };
            userRepository.Update(user);

            var users = userRepository.GetUsersByFirstName("Pruthvi");
            Assert.IsNotNull(users);
            Assert.IsTrue(users.Select(u => u.Email).Any());
            Assert.IsTrue(users.Select(u => u.Email).Any(e => e.Equals("ipru@gmail.com", StringComparison.OrdinalIgnoreCase)));
            Assert.IsTrue(users.Select(u => u.Address).Any());
            Assert.IsTrue(users.Select(u => u.Address).Any(e => e.Equals("IN", StringComparison.OrdinalIgnoreCase)));
        }

        [Test, Order(8)]
        public void CheckIfAbleToRemoveUser()
        {
            var userId = 1;
            var user = userRepository.GetById(userId);
            userRepository.Remove(user);

            var userIds = userRepository.GetAll().Select(u => u.Id);
            Assert.IsTrue(userIds.Any());
            Assert.IsFalse(userIds.Any(u => u == userId));
        }
    }
}
