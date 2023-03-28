using JobPortal.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobPortal.Data
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions options) : base(options)
        {

        }
        DbSet<User> Users { get; set; } 
        DbSet<Recruiter> Recruiters { get; set;}
        DbSet<Admin> Admins { get; set; }   
        DbSet<Candidate> Candidates { get; set; }
        DbSet<Job> Jobs { get; set;}
        DbSet<UserRoleMapping> RoleMappings { get; set; }
        DbSet<Role> Roles { get; set; }


    }
}
