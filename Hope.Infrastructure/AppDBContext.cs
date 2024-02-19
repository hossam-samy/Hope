using Hope.Domain.Model;
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

            // builder.Entity<PostOfLostPeople>().ToTable("PostsOLostPeople");
            //builder.Entity<PostOfLostThings>().ToTable("PostsOLostThings");

             builder.Entity<User>().HasMany(b=>b.lostPeople).WithOne(b=>b.user).HasForeignKey(b=>b.UserId);    
             builder.Entity<User>().HasMany(b=>b.lostThings).WithOne(b=>b.user).HasForeignKey(b=>b.UserId);    


        }

        virtual public DbSet<User> Users { get; set; }
        //virtual public DbSet<Admin> Admins { get; set; }
        //virtual public DbSet<Post> Posts { get; set; }
        virtual public DbSet<PostOfLostPeople> postOfLostPeoples { get; set; }
        virtual public DbSet<PostOfLostThings> postOfLostthings { get; set; }
        //virtual public DbSet<Message> Messages { get; set; }
        //virtual public DbSet<Notification> Notifications { get; set; }

    }
    
}
