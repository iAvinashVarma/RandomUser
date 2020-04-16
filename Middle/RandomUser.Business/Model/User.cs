using RandomUser.Business.Contract;
using RandomUser.Business.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RandomUser.Business.Model
{
    [Table("User")]
    public class User : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Gender")]
        public Gender Gender { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Column("CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [Column("ModifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [Column("Balance")]
        public string Balance { get; set; }

        [Column("profileImage")]
        public string ProfileImage { get; set; }
    }
}
