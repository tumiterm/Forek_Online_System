using ApprenticeApplications.Models;
using Microsoft.EntityFrameworkCore;
using ForekRequisition.Models;

namespace ForekRequisition.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<ApplicantStatus> ApprenticeStatus { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<ApprenticeResult> AppResults { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ApplicantOffer>? ApplicantOffer { get; set; }

        

    }
}
