using Newtonsoft.Json;
using RandomUser.Business.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RandomUser.Business.Concrete.Utils
{
    public static class UserDataMock
    {
        public static IEnumerable<User> GetUsersWithAutoIncrement(string usersDataPath)
        {
            var users = JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(usersDataPath));
            return users.Select((u, i) => new User
            {
                Id = i + 1,
                Title = u.Title,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender = u.Gender,
                Email = u.Email,
                Address = u.Address,
                DateOfBirth = u.DateOfBirth,
                CreatedDate = u.CreatedDate,
                ModifiedDate = u.ModifiedDate,
                Balance = u.Balance,
                ProfileImage = u.ProfileImage
            });
        }
    }
}
