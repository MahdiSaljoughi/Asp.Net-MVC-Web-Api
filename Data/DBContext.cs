using Microsoft.EntityFrameworkCore;
using MvcApi.Models;

namespace MvcApi.Data;

public class DBContext : DbContext
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<Product> Products { get; set; }
}