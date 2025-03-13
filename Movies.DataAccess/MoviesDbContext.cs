using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Models;

namespace Movies.DataAccess
{
    public class MoviesDbContext(DbContextOptions<MoviesDbContext> options) : IdentityDbContext<User>(options)
    {
        // Profile-related data
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; }

        // Content related (Films and Series)
        public DbSet<Content> Contents { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // Genre and its join table
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ContentGenre> ContentGenres { get; set; }

        // Group quiz related entities
        public DbSet<GroupQuiz> GroupQuizzes { get; set; }
        public DbSet<GroupQuizParticipant> GroupQuizParticipants { get; set; }
        public DbSet<QuizSession> QuizSessions { get; set; }
        public DbSet<QuizVote> QuizVotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite key for ContentGenre (Many-to-Many)
            modelBuilder.Entity<ContentGenre>()
                .HasKey(cg => new { cg.ContentId, cg.GenreId });

            // Configure composite key for Favorites (Many-to-Many)
            modelBuilder.Entity<Favorite>()
                .HasKey(f => new { f.UserId, f.ContentId });

            // Configure composite key for WatchlistItem (Many-to-Many)
            modelBuilder.Entity<WatchlistItem>()
                .HasKey(w => new { w.UserId, w.ContentId });

            // Self-referencing relationship for Comments (allowing nested comments)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);

            // GroupQuizParticipant configuration: each participant belongs to one GroupQuiz
            modelBuilder.Entity<GroupQuizParticipant>()
                .HasOne(gqp => gqp.GroupQuiz)
                .WithMany(gq => gq.Participants)
                .HasForeignKey(gqp => gqp.GroupQuizId);

            // QuizSession configuration: one GroupQuiz can have multiple quiz sessions
            modelBuilder.Entity<QuizSession>()
                .HasOne(qs => qs.GroupQuiz)
                .WithMany(gq => gq.QuizSessions)
                .HasForeignKey(qs => qs.GroupQuizId);

            // QuizVote configuration: one GroupSession can have multiple QuizVotes
            modelBuilder.Entity<QuizVote>()
                .HasOne(qv => qv.QuizSession)
                .WithMany(qs => qs.QuizVotes)
                .HasForeignKey(qv => qv.QuizSessionId);

            // User configuration: Restricting cascade deletion of GroupQuizParticipants, otherwise
            // that would create multiple cascade paths to GQP
            modelBuilder.Entity<User>()
                .HasMany(u => u.GroupQuizParticipants)
                .WithOne(gqp => gqp.User)
                .OnDelete(DeleteBehavior.Restrict);

            // Same config with GroupQuizVotes
            modelBuilder.Entity<User>()
                .HasMany(u => u.GroupQuizVotes)
                .WithOne(gqp => gqp.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
