using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monasapat.Models
{
    public class MonasabatContext:IdentityDbContext<User>
    {
        //public MonasapatContext(DbContextOptions options) : base(options)
        //{

        //}

        public DbSet<Comments>? Comments { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Place>? Places { get; set; }
        public DbSet<PlaceImage>? PlaceImages { get; set; }
        public DbSet<PlaceOwner>? PlaceOwners { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<SuggestedDesign>? SuggestedDesigns { get; set; }
        public DbSet<Governorate>? Governorates { get; set; }
        public DbSet<City>? Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new PlaceConfiguration().Configure(builder.Entity<Place>());
            new CommentsConfiguration().Configure(builder.Entity<Comments>());
            new PlaceImageConfiguration().Configure(builder.Entity<PlaceImage>());
            new PlaceOwnerConfiguration().Configure(builder.Entity<PlaceOwner>());
            new ReservationConfiguration().Configure(builder.Entity<Reservation>());
            new SuggestedDesignConfiguration().Configure(builder.Entity<SuggestedDesign>());

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source =.; Initial Catalog= Monasapat; Integrated Security = True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
