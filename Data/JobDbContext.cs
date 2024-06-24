using System;
using Microsoft.EntityFrameworkCore;
using TechJobsAuthentication.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TechJobsAuthentication.Data
{
    public class JobDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<Job> Jobs { get; set; } 

        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options)
        {
        }
    }
}
