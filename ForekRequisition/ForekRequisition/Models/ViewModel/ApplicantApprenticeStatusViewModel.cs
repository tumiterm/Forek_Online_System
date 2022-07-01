namespace ForekRequisition.Models.ViewModel
{
    public class ApplicantApprenticeStatusViewModel
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }   
        public string Name { get; set; }
        public string LastName { get; set; }    
        public string Description { get; set; } 
   
    }
}
