﻿using Hope.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hope.Infrastructure
{

    public class AppDBContext : IdentityDbContext<User>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            


             builder.Entity<User>().HasMany(b=>b.lostPeople).WithOne(b=>b.User).HasForeignKey(i=>i.UserId);
             builder.Entity<User>().HasMany(b=>b.lostThings).WithOne(b=>b.User).HasForeignKey(i=>i.UserId);
            builder.Entity<User>().HasMany(b => b.HiddingPeoples).WithMany(i => i.HiddenPeoples).UsingEntity(i => i.ToTable("HiddenPeoplePost"));
            builder.Entity<User>().HasMany(b => b.HiddingThings).WithMany(i => i.HiddenThings).UsingEntity(i => i.ToTable("HiddenThingsPost"));

            builder.Entity<User>().HasMany(b => b.PinningPeoples).WithMany(i => i.PinnedPeoples).UsingEntity(i => i.ToTable("PinnedPeoplePost"));
            builder.Entity<User>().HasMany(b => b.PinningThings).WithMany(i => i.PinnedThings).UsingEntity(i => i.ToTable("PinnedThingsPost"));
            

             //builder.Entity<User>().HasMany(b=>b.lostThings).WithMany(b=>b.Users);    


        }

         public DbSet<User> Users { get; set; }
         public DbSet<PostOfLostPeople> postOfLostPeoples { get; set; }
         public DbSet<PostOfLostThings> postOfLostthings { get; set; }
        
    }
    
}
