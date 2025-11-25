using Microsoft.EntityFrameworkCore;
using MovieHub.API.Constants;
using MovieHub.API.Models;

namespace MovieHub.API.Data
{
    public class MovieHubDbContext : DbContext
    {
        public MovieHubDbContext(DbContextOptions<MovieHubDbContext> options)
            : base(options) { }

        #region DbSets
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<ShowTime> ShowTimes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Booking
            // Configure relationships for Booking with ShowTime
            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.ShowTime)
                .WithMany(st => st.Bookings)
                .HasForeignKey(b => b.ShowTimeId)
                .OnDelete(DeleteBehavior.Cascade); // Delete all associated Bookings when a ShowTime is deleted

            // Configure relationships for Booking with User
            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of User if it has associated Bookings
            #endregion

            #region BookingSeat
            // Configure composite primary key for BookingSeat
            modelBuilder
                .Entity<BookingSeat>()
                .HasKey(bs => new { bs.BookingId, bs.SeatId });

            // Configure relationships for BookingSeat with Booking
            modelBuilder
                .Entity<BookingSeat>()
                .HasOne(bs => bs.Booking)
                .WithMany(b => b.BookedSeats)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Cascade); // Delete all associated BookingSeats when a Booking is deleted

            // Configure relationships for BookingSeat with Seat
            modelBuilder
                .Entity<BookingSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany(s => s.BookingSeats)
                .HasForeignKey(bs => bs.SeatId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Seat if it has associated BookingSeats
            #endregion

            #region ShowTime
            // Configure relationships for ShowTime and Movie
            modelBuilder
                .Entity<ShowTime>()
                .HasOne(st => st.Movie)
                .WithMany(m => m.ShowTimes)
                .HasForeignKey(st => st.MovieId)
                .OnDelete(DeleteBehavior.Cascade); // Delete all associated ShowTimes when a Movie is deleted

            // Configure relationships for ShowTime and Hall
            modelBuilder
                .Entity<ShowTime>()
                .HasOne(st => st.Hall)
                .WithMany(h => h.ShowTimes)
                .HasForeignKey(st => st.HallId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion of Hall if it has associated ShowTimes
            #endregion

            #region Role
            // Seeding the roles.
            modelBuilder
                .Entity<Role>()
                .HasData(
                    new Role
                    {
                        Id = DefaultConstants.Role.AdminRoleId,
                        Name = DefaultConstants.Role.AdminRoleName,
                    },
                    new Role
                    {
                        Id = DefaultConstants.Role.MangerRoleId,
                        Name = DefaultConstants.Role.MangerRoleName,
                    },
                    new Role
                    {
                        Id = DefaultConstants.Role.UserRoleId,
                        Name = DefaultConstants.Role.UserRoleName,
                    }
                );
            #endregion

            #region UserRole
            // Configure composite primary key for UserRole
            modelBuilder
                .Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            #endregion
        }
    }
}
