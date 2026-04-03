using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para o .Include()
using System.Linq;
using System;
using AgendamentoApi.DTOs;      
using AgendamentoApi.Services;  
using AgendamentoApi.Models;


namespace AgendamentoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPedidoService _pedidoService;

        //Injetando o DB aqui
        public PedidosController(AppDbContext context, IPedidoService pedidoService)
        {
            _context = context;
            _pedidoService = pedidoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodosPedidosAsync()
        {
            var pedidos = await _context.Pedidos
                                .Include(p => p.Cliente)
                                .ToListAsync();


            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedidoAsync([FromBody] CriarPedidoDTO comanda)
        {
           try
           {
            var pedidoPronto = await _pedidoService.CriarNovoPedidoAsync(comanda);

            //Retorna Status 201 Created
            return Created("", pedidoPronto);
           }
           catch (Exception ex)
           {
            //Retorna o erro
            return BadRequest(ex.Message);
           }
        }

        [HttpGet("relatorio")]
        public async Task<IActionResult> ObterRelatorio()
        {
           var relatorio = await _pedidoService.GerarRelatorioVendasAsync();

           return Ok(relatorio);
        }
    }
}

public static class MinhasExtensoes
{
    public static string ParaMoedaBrasileira(this decimal valor)
    {
        return valor.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
    }
}

