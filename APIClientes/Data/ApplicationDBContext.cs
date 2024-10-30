using APIClientes.Modelos;
using Microsoft.EntityFrameworkCore;

namespace APIClientes.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions <ApplicationDBContext>options): base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<User > Users { get; set;}
    }
}
