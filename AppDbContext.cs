using Microsoft.EntityFrameworkCore;
using AgendamentoApi.Controllers;
using AgendamentoApi.Models;

namespace AgendamentoApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //Tabela 1
        public DbSet<Cliente> Clientes {get;set;}

        //Tabela 2
        public DbSet<Pedido> Pedidos { get; set; }
    }
}