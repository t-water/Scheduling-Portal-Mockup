using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEServerTest.Models;

namespace TEServerTest.Data
{
    public class TEServerContext : IdentityDbContext<ApplicationUser>
    {
        public TEServerContext(DbContextOptions<TEServerContext> options) : base(options)
        {

        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<UserShift> UsersShifts { get; set; }
        public DbSet<TimeOff> TimeOff { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
    }
}
