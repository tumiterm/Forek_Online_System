using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekRequisition.Models.ViewModel
{
    public class EducationViewModel
    {

        [Required]
        [Display(Name = "Name of Last School Attended")]
        public string NameofHSchool { get; set; }
        public string Town { get; set; }
        public eMunicipality Municipality { get; set; }

        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DataType(DataType.Date)]
        public DateTime Till { get; set; }

        [Display(Name = "Application Status")]
        public eStatus AppStatus { get; set; } = eStatus.Submitted;

        [Display(Name = "Reason For Status")]
        public string? StatusDescription { get; set; }

        [Display(Name = "Highest Qualification")]
        public IFormFile? AttachHighQual { get; set; }

        [Display(Name = "RSA Identity Document")]
        public IFormFile? AttachID { get; set; }

        [Display(Name = "Apprentice CV")]
        public IFormFile? AttachCV { get; set; }

        [Display(Name = "Matric Results")]
        public IFormFile? AttachMatric { get; set; }

        [Display(Name = "Proof of Banking Details")]
        public IFormFile? AttachBanking { get; set; }

        [Display(Name = "Proof of Residential Address")]
        public IFormFile? AttachRes { get; set; }

        [Display(Name = "Affidavit In Support of proof of residence")]
        public IFormFile? AttachAffidavit { get; set; }

        [Display(Name = "Host Letter")]
        public IFormFile? AttachHostLetter { get; set; }

        [Display(Name = "Highest Grade Passed")]
        public eGradePassed GradePassed { get; set; }

        [Display(Name = "Math Type")]
        public eMathSubj SubjectOne { get; set; }

        [Display(Name = "Physics/Technical Science %")]

        //data type changed
        public double Science { get; }

        //Add to Db
        public double Math { get; }
        [ForeignKey("Applicant")]
        public Guid ApplicantId { get; set; }
        public virtual Applicant? Applicant { get; }

        [Display(Name = "Qualification Name")]
        public string? QualificationName { get; set; }
        public eQualificationType? QualificationType { get; set; }
        public eQualifLevel? QualificationLevel { get; set; }

        public ePassedSubj HasPassedSubjects { get; set; }

        [Display(Name = "From")]
        [DataType(DataType.Date)]
        public DateTime IsFrom { get; set; }
        [Display(Name = "FET College Attended")]
        public string? FETName { get; set; }

        [DataType(DataType.Date)]
        public DateTime? IsTill { get; set; }

    }
}
