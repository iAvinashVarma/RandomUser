using RandomUser.Business.Contract;
using RandomUser.Business.Enum;
using System;

namespace RandomUser.Business.Entity.Dto
{
    public class UserModel : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Balance { get; set; }

        public string ProfileImage { get; set; }
    }
}
