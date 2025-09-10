using Microsoft.EntityFrameworkCore;
using APICsharp.Models;

namespace APICsharp.Data
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
