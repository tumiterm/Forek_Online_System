using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace ForekRequisition.Models
{
    public class ApprenticeResult : Base
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string LastName { get; set; }

        [Display(Name = "RSA ID Number")]

        [MaxLength(13)]
        public string IdNumber { get; set; }

        [MaxLength(10)]
        public string? Cellphone { get; set; }

        [Display(Name = "Does Applicant Qualify")]
        public eDecision DoesApplicantQualify { get; set; }
        public eProgramme Programme { get; set; }

        [Display(Name = "Programme Type")]
        public eProgrammeType ProgrammeType { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Recommendation { get; set; }

        [Display(Name = "Percentage(%) Obtained")]
        public double PercentageObtained { get; set; } = 0;

        [Display(Name = "Add More")]
        public bool AddMore { get; set; } = false;

        [Display(Name = "Municipality")]
        public eMunicipality District { get; set; }

        [Display(Name = "District")]
        public eDistrict? MunicipalDistrict { get; set; }
        public eGender Gender { get; set; }

        [Display(Name = "Approval Status")]
        public eApproval IsApproved { get; set; } = eApproval.NotApproved;


    }
}
