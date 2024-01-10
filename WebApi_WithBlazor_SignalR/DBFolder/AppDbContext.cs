using Microsoft.EntityFrameworkCore;
using WebApi_WithBlazor_SignalR.Models;

namespace WebApi_WithBlazor_SignalR.DBFolder
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Employee> employees { get; set; }
    }
}
