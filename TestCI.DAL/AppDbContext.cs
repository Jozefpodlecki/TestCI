using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
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
