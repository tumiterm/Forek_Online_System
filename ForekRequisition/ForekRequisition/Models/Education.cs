using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekRequisition.Models
{
    public class Education : Base
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Name of Last School Attended")]
        public string NameofHSchool { get; set; }
        public string Town { get; set; }
        public eMunicipality Municipality { get; set; }

        [DataType(DataType.Date)]
        public DateTime? From { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Till { get; set; }

        [Display(Name = "Highest Qualification")]
        public string? HighestQualDoc { get; set; }

        [Display(Name = "RSA Identity Document")]
        public string? IdentityPDF { get; set; }

        [Display(Name = "Apprentice CV")]
        public string? ApprenticeCV { get; set; }

        [Display(Name = "Matric Results")]
        public string? MatricResults { get; set; }

        [Display(Name = "Proof of Banking Details")]
        public string? BankingDocs { get; set; }

        [Display(Name = "Proof of Residential Address")]
        public string? ProofRes { get; set; }

        [Display(Name = "Affidavit In Support of proof of residence")]
        public string? AffidavitInSupport { get; set; }

        [Display(Name = "Host Letter")]
        public string? HostEmpNot { get; set; }

        [Display(Name = "Highest Grade Passed")]
        public eGradePassed GradePassed { get; set; }

        [Display(Name = "Math Type")]
        public eMathSubj SubjectOne { get; set; }

        [ForeignKey("Applicant")]
        public Guid ApplicantId { get; set; }
        public virtual Applicant? Applicant { get;}

        [Display(Name = "Qualification Name")]
        public string? QualificationName { get; set; }
        public eQualificationType? QualificationType { get; set; }
        public eQualifLevel? QualificationLevel { get; set; }

        [Display(Name = "Passed All Modules?")]
        public ePassedSubj? HasPassedSubjects { get; set; }
      
        [Display(Name = "From")]
        [DataType(DataType.Date)]
        public DateTime? IsFrom { get; set; }
        [Display(Name = "FET College Attended")]
        public string? FETName { get; set; }    

        [DataType(DataType.Date)]
        public DateTime? IsTill { get; set; }

        [Required(ErrorMessage = "RSA ID required")]
        [Display(Name = "RSA ID Documents")]
        [NotMapped]
        public IFormFile RSAIDDoc { get; set; }

        [Display(Name = "Grade 12 Results")]
        [NotMapped]
        [Required(ErrorMessage = "Matric results required")]

        public IFormFile Gr12 { get; set; }

        [Display(Name = "Residence Proof")]
        [NotMapped]
        public IFormFile ResidenceDoc { get; set; }

        [Display(Name = "Banking Details Upload")]
        [NotMapped]
        public IFormFile BankDocs { get; set; }

        [Display(Name = "Math/Maths Lit %")]
        public string MathSubject { get; set; }

        [Display(Name = "Physics/Technical Science %")]
        public string ScienceSubject { get; set; }

        [NotMapped]
        [Display(Name = "Updated CV")]
        public IFormFile CVDocs { get; set; }

        [Display(Name = "Medical Certificate")]
        public string? MedicalCert { get; set; }

        [NotMapped]
        [Display(Name = "Medical Certificate")]
        public IFormFile MedCertificate { get; set; } 

        [NotMapped]
        [Display(Name = "Host Letter")]
        public IFormFile? HostFile { get; set; }

        [NotMapped]
        [Display(Name = "Affidavit")]
        public IFormFile? AffidavitFile { get; set; }

        public bool IsApplicationComplete { get; set; }
        public bool IsPartiallyComplete { get; set; }  
        public bool IsIncomplete { get; set; }




    }
}
