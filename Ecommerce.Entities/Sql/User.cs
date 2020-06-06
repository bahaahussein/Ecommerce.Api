using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Entities.Sql
{
    public class User
    {
        public long UserId { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
