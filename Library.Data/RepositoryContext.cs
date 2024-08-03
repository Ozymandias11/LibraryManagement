

using Library.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Library.Data
{
    public class RepositoryContext : IdentityDbContext<Employee>
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {

        
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }



        // DbSets represents tables in our database
        public DbSet<Author>? Author { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<BookAuthor>? BookAuthor{ get; set; }
        public DbSet<BookCategory>? BookCategories { get; set; }
        public DbSet<BookCopy>? BookCopies { get; set; }
        public DbSet<BookPublisher>? BookPublisher { get; set; }    
        public DbSet<Category>? Category { get; set; }
        public DbSet<Customer>? Customer { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }
        public DbSet<Room>? Rooms { get; set; }
        public DbSet<Shelf>? Shelf { get; set; }
        public DbSet<BookCopyShelf>? Stored { get; set; }
        public DbSet<NavigationMenu>? NavigationMenus { get; set; }
        public DbSet<RoleMenuPermission>? RoleMenus { get; set; }





    }
}
