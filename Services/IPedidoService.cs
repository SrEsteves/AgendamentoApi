using AgendamentoApi.DTOs;
using AgendamentoApi.Models;

namespace AgendamentoApi.Services
{
    public interface IPedidoService
    {
        Task<Pedido> CriarNovoPedidoAsync(CriarPedidoDTO comanda);

        Task<IEnumerable<object>> GerarRelatorioVendasAsync();
    }
}