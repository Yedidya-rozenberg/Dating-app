using System.Runtime.Intrinsics.Arm;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
 
        public DbSet<AppUser> Users { get; set; }

        public DbSet<UserLike> userLikes { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserLike>().HasKey(k => new { k.LikedUserId, k.SourceUserId });

            builder.Entity<UserLike>()
            .HasOne(u=>u.SourceUser)
            .WithMany(u=>u.LikedUsers)
            .HasForeignKey(u => u.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

             builder.Entity<UserLike>()
                .HasOne(u => u.LikedUser)
                .WithMany(u => u.LikedByUsers)
                .HasForeignKey(u => u.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade);

             builder.Entity<Message>()
             .HasOne(m=>m.Sender)
             .WithMany(s=>s.MassegesSent)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
             .HasOne(m=>m.Recipient)
             .WithMany(s=>s.MassegesRecived)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }
}