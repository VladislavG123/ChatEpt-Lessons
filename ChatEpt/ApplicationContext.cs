using ChatEpt.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatEpt;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<MessageEntity> Messages { get; set; }
}