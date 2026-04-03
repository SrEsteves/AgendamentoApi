using AgendamentoApi.Models;
using AgendamentoApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoApi.Services;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;

    public ClienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> ListarTodosAsync()
    {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente> AdicionarAsync(CriarClienteDTO dados)
    {
        var novoCliente = new Cliente
        {
            Nome = dados.Nome,
            Telefone = dados.Telefone,
            Idade = dados.Idade,
            Status = true
        };

        _context.Clientes.Add(novoCliente);
        await _context.SaveChangesAsync();
        return novoCliente;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AtualizarClienteAsync(int id, EditarClienteDTO dados)
    {
        var cliente = await _context.Clientes.FindAsync(id);
        if (cliente == null) return false;

        cliente.Nome = dados.NovoNome;
        cliente.Telefone = dados.NovoTelefone;
        cliente.Idade = dados.NovaIdade;

        await _context.SaveChangesAsync();
        return true;
    }
}