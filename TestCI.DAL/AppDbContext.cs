using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TestCI.DAL.Models;

namespace TestCI.DAL
{
    [ExcludeFromCodeCoverage]
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
