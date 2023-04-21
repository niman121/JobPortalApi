using JobPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data
{
    public class JobDbContext : DbContext
    {
        
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {

        }
       
        public DbSet<User> Users { get; set; } 
        public DbSet<Recruiter> Recruiters { get; set;}
        public DbSet<Admin> Admins { get; set; }   
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Job> Jobs { get; set;}
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<JobPortalOtp> JobPortalOtps { get; set; }
        public DbSet<CandidateJob> CandidateJob { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CandidateJob>()
                .HasKey(cj => new { cj.CandidateId, cj.JobId });
        }
    }
}
