using ApprenticeApplications.Models;

namespace ForekRequisition.Models.ViewModel
{
    public class ApplicantsViewModel
    {
        public IEnumerable<ApprenticeResult> QualifiedApplicants { get; set; }
        public IEnumerable<ApprenticeResult> NotQualified { get; set; }

    }
}
