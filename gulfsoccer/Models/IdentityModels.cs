using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DAL.Database;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace gulfsoccer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Post> Posts { get; set;}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag>Tags { get; set; }
        public DbSet<HeadAttr> HeadAttrs { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<PermaLink> PermaLinks { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating( modelBuilder);
            modelBuilder.Entity<PermaLink>().HasKey(L => new { L.PostId });
            modelBuilder.Entity<PostCategory>().HasKey(PC => new { PC.PostId, PC.CategoryId });
            modelBuilder.Entity<PostTag>().HasKey(PT => new { PT.PostId, PT.TagId });
            modelBuilder.Entity<AlbumMedia>().HasKey(AM => new { AM.AlbumId, AM.MediaId});
        }
    }
}