using AgendamentoApi.DTOs;
using AgendamentoApi.Models;

namespace AgendamentoApi.Services;

public interface IClienteService
{
    Task <IEnumerable<Cliente>> ListarTodosAsync();
    Task<Cliente> AdicionarAsync(CriarClienteDTO dados);
    Task<bool> DeletarAsync(int id);
    Task<bool> AtualizarClienteAsync(int id, EditarClienteDTO dados);
}