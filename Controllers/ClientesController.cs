using Microsoft.AspNetCore.Mvc;
using AgendamentoApi.Services;
using AgendamentoApi.DTOs;


namespace AgendamentoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        return Ok(await _clienteService.ListarTodosAsync());
    }

    
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] CriarClienteDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Nome)) return BadRequest("Nome é obrigatório!");

        var cliente = await _clienteService.AdicionarAsync(dto);
        return Created("", cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] EditarClienteDTO dados)
    {
        //Faz um select procurando o ID
        var sucesso = await _clienteService.AtualizarClienteAsync(id, dados);

        if (!sucesso) return NotFound("Cliente não encontrado para atualizar.");
       
        return Ok("Cliente atualizado com sucesso!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var sucesso = await _clienteService.DeletarAsync(id);
        if (!sucesso) return NotFound();
        return NoContent();
    }

}



