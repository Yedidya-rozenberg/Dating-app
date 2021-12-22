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
 
        public virtual DbSet<AppUser> AppUsers { get; set; }

    }
}