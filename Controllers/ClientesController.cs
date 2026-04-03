using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AgendamentoApi.Models;

namespace AgendamentoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        //Variavel que segura a conexão com o banco.
        private readonly AppDbContext _context;

        //O Construtor, o .NET ve isso e devolve o DB pronto pra usar
        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            //O ".ToList()" aqui faz um SELECT * FROM Clientes no banco
            var clientes = _context.Clientes.ToList();
            return Ok(clientes);
        }

        [HttpGet("ativos")]
        public IActionResult GetAtivosEAdultos()
        {
            var clientesFiltrados = _context.Clientes
                .Where(c => c.Status == true && c.Idade >= 18)
                .ToList();

            return Ok(clientesFiltrados);
        }
        
        [HttpPost]
        public IActionResult AdicionarCliente([FromBody] Cliente novoCliente)
        {
            if (string.IsNullOrEmpty(novoCliente.Nome))
            {
                return BadRequest("O nome do cliente é obrigatório!");
            }

            _context.Clientes.Add(novoCliente); // Prepara o Insert
            _context.SaveChanges(); //Executa a alteração no arquivo físico do banco

            return Created("", novoCliente);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCliente(int id, [FromBody] Cliente clienteAtualizado)
        {
            //Faz um select procurando o ID
            var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            //Validando se é null, e retornando notfound
            if (clienteExistente == null) return NotFound();

            // Atualiza os dados caso tenha encontrado o cliente usando o parametro
            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Telefone = clienteAtualizado.Telefone;
            clienteExistente.Status = clienteAtualizado.Status;
            clienteExistente.Idade = clienteAtualizado.Idade;

            //Diferente da lista antiga, agora preciso mandar para o banco o update

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarCliente(int id)
        {
            //Mesma coisa, aqui faz um select baseado no id passado
            var clienteExistente = _context.Clientes.FirstOrDefault(c => c.Id == id);

            //valida se é null e encerra se for
            if (clienteExistente == null) return NotFound();

            //Prepara o delete
            _context.Clientes.Remove(clienteExistente);

            //Efetiva o delete no banco
            _context.SaveChanges();

            return NoContent();
        }

    }

    

}
