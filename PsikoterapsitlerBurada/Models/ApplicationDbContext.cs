using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PsikoterapsitlerBurada.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasRequired(u => u.WhoAsked)
                .WithMany(q => q.AskedQuestions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(u => u.AskedToWhom)
                .WithMany(q => q.QuestionsAsked)
                .Map(m => m.ToTable("QuestionsAskedWhom"));

            base.OnModelCreating(modelBuilder);
        }
    }
}