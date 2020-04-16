using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RandomUser.Business.Concrete.Utils;
using RandomUser.Business.Model;
using System.IO;
using System.Linq;

namespace RandomUser.Business.Entity.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var usersDataPath = Path.Combine(AssemblyUtils.AssemblyDirectory, @"Data\Users.json");
            var users = UserDataMock.GetUsersWithAutoIncrement(usersDataPath);
            builder.HasData(users.ToArray());
        }
    }
}
