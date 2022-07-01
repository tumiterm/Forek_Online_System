using ApprenticeApplications.Models;
using ApprenticeApplications.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekRequisition.Models
{
    public class ApplicantStatus : Base
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name="Set Application Status")]
        public eStatus ApplicationStatus { get; set; } = eStatus.Submitted;


        [DataType(DataType.MultilineText)]
        [Display(Name = "Describe Reason for Application Status")]
        public string? Description { get; set; }

        [ForeignKey("Applicant")]
        public Guid ApplicantId { get; set; }  
        public Applicant? Applicant { get; set; }    

    }
}
