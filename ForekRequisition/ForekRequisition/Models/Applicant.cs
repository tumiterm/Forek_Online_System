using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprenticeApplications.Models
{
    public class Applicant : Base
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Programme Type")]
        public eProgrammeType ProgrammeType { get; set; } = eProgrammeType.ForekMegaApprentice;

        [Display(Name = "Trade Applied For")]
        public eProgramme Trade { get; set; }

        [MaxLength(50)]
        [MinLength(3)]
        [StringLength(50)]
        public string Name { get; set; } 
        
        [Display(Name ="Last Name")]
        [MaxLength(50)]
        [MinLength(3)]
        [StringLength(50)]
        public string LastName { get; set; }
        public eProvince Province { get; set; }

        [Display(Name="Title")]
        public eTitle Initials { get; set; }

        [MaxLength(13)]
        [MinLength(13)]
        [StringLength(13)]
        [Display(Name = "RSA ID Number")]
        public string IDNumber { get; set; }    
        public eRace Race { get; set; }
        public eGender Gender { get; set; }
        public string? Disability { get; set; }
        public string? DisabilityAttachment { get; set; }
        public eMunicipality Municipality { get; set; }

        [Display(Name ="Same as Residential?")]
        public bool AddressIsSameAsPostal { get; set; }

        [Display(Name = "Unit/Complex/Stree Number")]
        public string AddressLine1 { get; set; }
        public string? City { get; set; }

        [Display(Name = "Postal Code")]
        public string Code { get; set; }

        [Display(Name = "Home Telephone")]
        public string? HomeTel { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        [StringLength(10)]
        public string Cellphone { get; set; }
        public string? ReferenceNumber { get; set; } = $"FIT" + DateTime.Now.Year;

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Alternative Contact Person")]
        public string? AlternativeContacPers { get; set; }

        [Display(Name = "Alternative Number")]
        [MaxLength(10)]
        [MinLength(10)]
        [StringLength(10)]
        public string? AlternativeNum { get; set; }

        [Display(Name = "Prospective Employer")]
        public string? ProspectiveEmployer { get; set; }

        [Display(Name = "IsCurrently Employed")]
        public bool IsCurrentlyEmployed { get; set; } = false;

        [Display(Name = "I Agree")]
        public bool HasSignedTerms { get; set; }
        public bool HasAcceptedOffer { get; set; } = false;

        [NotMapped]
        [Display(Name ="Upload Disability File")]
        public IFormFile? DisabilityAtt { get; set; }





    }
}
