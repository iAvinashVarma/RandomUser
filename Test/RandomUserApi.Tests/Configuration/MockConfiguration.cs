using AutoMapper;
using Moq;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Contract.Repository;
using RandomUser.Business.Model;
using RandomUserApi.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUserApi.Tests.Configuration
{
    public static class MockConfiguration
    {
        public static Mock<IUserRepository> MockVehicleRepository
        {
            get
            {
                // Create mock users
                var usersDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Sample\Data\Users.json");
                var users = UserDataMock.GetUsersWithAutoIncrement(usersDataPath).ToList();

                // Mock user repository using Moq
                var mockVehicleRepository = new Mock<IUserRepository>();

                // Return all users
                mockVehicleRepository.Setup(m => m.GetAll())
                    .Returns(users.AsQueryable());

                // Return user by id
                mockVehicleRepository.Setup(m => m.GetById(It.IsAny<int>()))
                    .Returns((int o) => users.Where(v => v.Id.Equals(o)).Single());

                // Return user by first name
                mockVehicleRepository.Setup(m => m.GetUsersByFirstName(It.IsAny<string>()))
                    .Returns((string f) => users.Where(v => v.FirstName.Equals(f, StringComparison.OrdinalIgnoreCase)).AsQueryable());

                // Return user by last name
                mockVehicleRepository.Setup(m => m.GetUsersByLastName(It.IsAny<string>()))
                    .Returns((string l) => users.Where(v => v.LastName.Equals(l, StringComparison.OrdinalIgnoreCase)).AsQueryable());

                // Return user by limit
                mockVehicleRepository.Setup(m => m.GetUsersByLimit(It.IsAny<int>()))
                    .Returns((int l) => users.OrderByDescending(u => u.ModifiedDate).Take(l).AsQueryable());

                // Return user by id
                mockVehicleRepository.Setup(m => m.GetById(It.IsAny<int>()))
                    .Returns((int o) => users.Where(v => v.Id.Equals(o)).Single());

                // Allow to save user
                mockVehicleRepository.Setup(m => m.Add(It.IsAny<User>()))
                    .Returns((User target) =>
                    {
                        var createdDate = DateTime.Now;
                        var modifiedDate = DateTime.Now;
                        if (target.Id.Equals(default))
                        {
                            target.Id = users.Max(u => u.Id);
                            target.CreatedDate = createdDate;
                            target.ModifiedDate = modifiedDate;
                            users.Add(target);
                            return true;
                        }
                        else
                        {
                            var original = users.Where(q => q.Id.Equals(target.Id)).Single();
                            if (original == null)
                            {
                                return false;
                            }
                            else
                            {
                                users.Remove(original);
                                original = target;
                                original.ModifiedDate = modifiedDate;
                                users.Add(original);
                                return true;
                            }
                        }
                    });

                // Allow to update user
                mockVehicleRepository.Setup(m => m.Update(It.IsAny<User>()))
                    .Returns((User target) =>
                    {
                        var createdDate = DateTime.Now;
                        var modifiedDate = DateTime.Now;
                        if (target.Id.Equals(default))
                        {
                            target.Id = users.Max(u => u.Id) + 1;
                            target.CreatedDate = createdDate;
                            target.ModifiedDate = modifiedDate;
                            users.Add(target);
                            return true;
                        }
                        else
                        {
                            var original = users.Where(q => q.Id.Equals(target.Id)).Single();
                            if (original == null)
                            {
                                return false;
                            }
                            else
                            {
                                users.Remove(original);
                                original = target;
                                original.ModifiedDate = modifiedDate;
                                users.Add(original);
                                return true;
                            }
                        }
                    });


                // Allow to delete user
                mockVehicleRepository.Setup(m => m.Remove(It.IsAny<User>()))
                    .Returns((User u) =>
                    {
                        var original = users.Where(q => q.Id.Equals(u.Id)).Single();
                        if (original == null)
                        {
                            return false;
                        }
                        else
                        {
                            users.Remove(original);
                            return true;
                        }
                    });

                return mockVehicleRepository;
            }
        }
    }
}
