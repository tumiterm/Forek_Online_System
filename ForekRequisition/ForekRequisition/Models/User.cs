using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ForekRequisition.Models
{
    public partial class User : Base
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsEmailVerified { get; set; }
        public Guid ActivationCode { get; set; }
        public string? ResetPasswordCode { get; set; }
        public string? LastLoginDate { get; set; }
        public eRole? Role { get; set; } 

    }
}
