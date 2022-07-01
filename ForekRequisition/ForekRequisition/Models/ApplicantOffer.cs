using ApprenticeApplications.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ForekRequisition.Models
{
    public class ApplicantOffer : Base
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Applicant")]
        public Guid ApplicantId { get; set; }

        //[Display(Name = "Accept Plumber Offer")]
        [Display(Name = "Im Available")]
        public bool HasAcceptedPlumbingOffer { get; set; } = false;

        //[Display(Name = "Accept Welding Offer")]
        [Display(Name = "Im NO longer available")]
        public bool HasAcceptedWeldingOffer { get; set; } = false;
        public bool HasSignedOffer { get; set; } = false;
    }
}
