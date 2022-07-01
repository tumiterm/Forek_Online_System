using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekRequisition.Models
{
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Surname is required")]
        [MinLength(3, ErrorMessage = "Surname too short")]
        [MaxLength(20, ErrorMessage = "Surname too long")]
        public string Surname { get; set; }
        public eTitle Title { get; set; }

        [Display(Name = "Study Mode")]
        public eStudyMode StudyMode { get; set; }
        public eNationality Nationality { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [MinLength(3, ErrorMessage = "First Name too short")]
        [MaxLength(20, ErrorMessage = "First Name too long")]
        [Display(Name ="Full Name(s)")]
        public string FullNames { get; set; }

        [Required(ErrorMessage = "RSA ID Number is required")]
        [MinLength(13, ErrorMessage = "ID Number must be 13 numbers!")]
        [MaxLength(13, ErrorMessage = "ID Number must be 13 numbers!")]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        [Required(ErrorMessage = "Address Type is required!")]
        [Display(Name = "Address Type")]
        public eAddressType AddressType { get; set; }

        [Display(Name = "Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "Postal Code")]
        public string Code { get; set; } 
        public string? Telephone { get; set; }

        [Display(Name = "Alternative Cellphone Number")]
        public string? AlternativeNum { get; set; }

        [MaxLength(10,ErrorMessage = "Cellphone number cannot exceed 10 numbers")]
        [MinLength(10, ErrorMessage = "Cellphone number cannot be less than 10 numbers")]
        public string? Cellphone { get; set; }

        [DataType(DataType.EmailAddress)]   
        public string? Email { get; set; }   

        [Display(Name = "Highest Grade Passed")]
        public eGradePassed GradePassed { get; set; }

        [Display(Name = "Highest Qualification")]
        public eQualifLevel HighestQual { get; set; }

        [Display(Name = "Full Name(s)")]
        public string? GuardianName { get; set; }

        [Display(Name = "Last Name")]
        public string GuardianLastName { get; set; }
        public string? GuadianId { get; set; }

        [Display(Name = "Cellphone Number")]
        [MaxLength(10)]
        [MinLength(10)]
        [Required(ErrorMessage = "Cellphone number required!")]
        public string GuardianCell{ get; set; }

        [Display(Name = "Telephone")]
        [MinLength(10)]
        public string? GuardianTel { get; set; }
        public eRelationship Relationship { get; set; }
        public eCourse Course { get; set; }
        [Display(Name = "Course Description")]
        public string? CourseDescription { get; set; }

        [Display(Name = "Funding")]
        public eFunding HasFunding { get; set; }

        [Display(Name = "Funder/Sponsor Name")]
        public string? FunderNameSurname { get; set; }

        [Display(Name = "Funder/Sponsor Contact Details")]
        public string? FunderContact { get; set; } 

        [Display(Name = "RSA ID")]
        public string? RSAIDAttach { get; set; }

        [Display(Name = "Highest Qualification")]
        public string? HighestQualCertAttach { get; set; }

        [Display(Name = "Is Employed?")]
        public bool IsEmployed { get; set; }
        public string? ProofOfRes { get; set; }

        [NotMapped]
        [Display(Name = "Attach Proof of Residence")]
        public IFormFile AttachProofRes { get; set; }

        [NotMapped]
        [Display(Name = "Attach Highest Qualification")]
        public IFormFile HighestCert { get; set; }

        [NotMapped]
        [Display(Name = "RSA ID Document")]
        public IFormFile RSAID { get; set; }

   
        [NotMapped]
        [Display(Name = "Guardian RSA ID / Passport")]
        public IFormFile GuardianID { get; set; }

    }
}
