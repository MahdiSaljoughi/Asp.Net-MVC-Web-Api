using Microsoft.EntityFrameworkCore;
using MvcApi.Models;

namespace MvcApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    // public DbSet<Role> Roles { get; set; }
    
    public DbSet<Product> Products { get; set; }
}