using Microsoft.EntityFrameworkCore;
using Task4TaskApi.Models;

namespace Task4TaskApi.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
