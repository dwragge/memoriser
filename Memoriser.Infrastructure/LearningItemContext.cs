using Memoriser.ApplicationCore.LearningItems;
using Microsoft.EntityFrameworkCore;

namespace Memoriser.Infrastructure
{
    public class LearningItemContext : DbContext
    {
        public LearningItemContext(DbContextOptions<LearningItemContext> options) : base(options)
        {
        }

        public DbSet<LearningItem> LearningItems { get; set; }
        public DbSet<RepetitionInterval> RepetitionIntervals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LearningItem>()
                .Ignore(x => x.AcceptedAnswers);

            modelBuilder.Entity<LearningItem>()
                .Property("_acceptedAnswers")
                .HasColumnName("AcceptedAnswers")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

            modelBuilder.Entity<RepetitionInterval>()
                .Property<int>("Interval")
                .HasField("_interval")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
