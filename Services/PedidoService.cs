using Microsoft.EntityFrameworkCore;
using AgendamentoApi.DTOs;
using AgendamentoApi.Models;

namespace AgendamentoApi.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;

        //Injetando o DB aqui
        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pedido> CriarNovoPedidoAsync(CriarPedidoDTO comanda)
        {
            bool clienteExiste = await _context.Clientes.AnyAsync(c => c.Id == comanda.ClienteId);
            if (!clienteExiste)
                throw new Exception("Cliente não encontrado.");

            var novoPedido = new Pedido
            {
                ValorTotal = comanda.ValorTotal,
                ClienteId = comanda.ClienteId,
                DataPedido = DateTime.Now
            };

            novoPedido.Status = novoPedido.ValorTotal switch
            {
                <= 0 => throw new ArgumentException("Valor não pode ser zero!"),
                < 100 => "Pedido Comum - Envio Padrão",
                >= 100 and < 1000 => "Pedido Médio - Envio Expresso",
                >= 1000 => "Pedido VIP - Frete Grátis e Brinde",
            };

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();

            return novoPedido;
        }

        public async Task<IEnumerable<object>> GerarRelatorioVendasAsync()
        {
            var pedidosDoBanco = await _context.Pedidos
                .Include(p => p.Cliente)
                .Where(p => p.ValorTotal > 100)
                .ToListAsync();

            var relatorioFinal = pedidosDoBanco
                .OrderByDescending(p => p.ValorTotal) 
                .Select(p => new
                {
                    NumeroDaVenda = p.Id,
                    Cliente = p.Cliente?.Nome?.ToUpper() ?? "SEM NOME",
                    ValorFormatado = p.ValorTotal.ParaMoedaBrasileira(),
                    Categoria = p.Status
                });    

            return relatorioFinal;
        }
    }
}