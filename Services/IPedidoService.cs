using AgendamentoApi.DTOs;
using AgendamentoApi.Models;

namespace AgendamentoApi.Services
{
    public interface IPedidoService
    {
        Task<Pedido> CriarNovoPedidoAsync(CriarPedidoDTO comanda);

        Task<IEnumerable<object>> GerarRelatorioVendasAsync();

        Task<bool> ExcluirPedidoAsync(int id);

        Task<bool> AtualizarPedidoAsync(int id, EditarPedidoDTO dados);
    }
}