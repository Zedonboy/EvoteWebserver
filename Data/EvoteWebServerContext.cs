using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EvoteWebServer.Models;

namespace EvoteWebServer.Models
{
    public class EvoteWebServerContext : DbContext
    {
        public EvoteWebServerContext(DbContextOptions<EvoteWebServerContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=0.0.0.0;database=evote;port=3306;user=root;password=");
        }
        public DbSet<EvoteWebServer.Models.Voter> Voter { get; set; }

        public DbSet<EvoteWebServer.Models.Election> Election { get; set; }

        public DbSet<EvoteWebServer.Models.Candidate> Candidate { get; set; }

        public DbSet<Voter_To_Candidate> votersCandidate { get; set; }

        public DbSet<Voters_to_ElectionType> votersELectionType{get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voter>()
                .HasMany(v => v.voterCandidates)
                .WithOne(v => v.voter);
            modelBuilder.Entity<Candidate>()
                .HasMany(c => c.voterCandidates)
                .WithOne(c => c.candidate);
            modelBuilder.Entity<Voter>()
            .HasMany(v => v.voterElectionTypes)
            .WithOne(ve => ve.voter);
        }
    }
}
