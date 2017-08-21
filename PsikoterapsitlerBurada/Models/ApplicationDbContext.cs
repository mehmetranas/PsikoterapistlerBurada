using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace PsikoterapsitlerBurada.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Following> Followings { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create() => new ApplicationDbContext();

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasRequired(u => u.WhoAsked)
                .WithMany(q => q.AskedQuestions)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.UsersTrack)
                .WithMany(u => u.FavoriteQuestions)
                .Map(m => m.ToTable("FavoriteQuestions"));

            modelBuilder.Entity<Question>()
                .HasMany(u => u.AskedToWhom)
                .WithMany(q => q.QuestionsAsked)
                .Map(m => m.ToTable("QuestionsAskedWhom"));

            modelBuilder.Entity<Answer>()
                .HasMany(a => a.Likes)
                .WithMany(u => u.LikesAnswers)
                .Map(m => m.ToTable("Likes"));

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followees)
                .WithRequired(f => f.Followee)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithRequired(u => u.Follower)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}