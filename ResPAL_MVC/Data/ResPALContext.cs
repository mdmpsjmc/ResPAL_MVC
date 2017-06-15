using ResPAL_MVC.Models.ResPALModels;
using Microsoft.EntityFrameworkCore;

namespace ResPAL_MVC.Data
{
    public class ResPALContext : DbContext
    {
        public ResPALContext(DbContextOptions<ResPALContext> options) : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PrimaryDisease> Diseases { get; set; }
        public DbSet<Azole> Azoles { get; set; }
     }
}
