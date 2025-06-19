using App.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Data {
    public class ApplicationDbContext:DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }
        public DbSet<User>Users { get; set; }   
        public DbSet<Product>Products { get; set; }
        public DbSet<Employee>Employees { get; set; }
    }
}
