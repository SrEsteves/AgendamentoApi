// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;

// namespace AgendamentoApi.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]

//     public class ClientesController: ControllerBase
//     {
//         private static List<Cliente> _listaClientes = new List<Cliente>
//         {
//             new Cliente { Id = 1, Nome = "Thierry", Telefone = "11", Status = true, Idade = 27},
//             new Cliente { Id = 2, Nome = "Eliseu", Telefone = "22", Status = true, Idade = 63},
//             new Cliente { Id = 3, Nome = "Gojo", Telefone = "33", Status = true, Idade = 28}
//         };

//         //Rota 1: Pega TODOS os clientes (GET http://localhost:5040/api/clientes)

//         [HttpGet]
//         public IActionResult GetTodos()
//         {
//             return Ok(_listaClientes);
//         }

//         //Rota 2: Trazendo o LINQ para web
//         [HttpGet("ativos")]
//         public IActionResult GetAtivosEAdultos()
//         {
//             var clientesFiltrados = _listaClientes
//                 .Where(c => c.Status == true && c.Idade >= 18)
//                 .ToList();

//             return Ok(clientesFiltrados);
//         }

//         //Rota 3: Adicionando um novo cliente (POST http://localhost:5040/api/clientes)

//         [HttpPost]
//         public IActionResult AdicionarCliente([FromBody] Cliente novoCliente)
//         {
//             //Validação Simples
//             if (string.IsNullOrEmpty(novoCliente.Nome))
//             {
//                 return BadRequest("O nome do cliente é obrigatório!");
//             }

//             //Simulando o que o banco de dados faria (geraria um novo ID)

//             novoCliente.Id = _listaClientes.Count +1;

//             //Adiciona na nossa lista em memória
//             _listaClientes.Add(novoCliente);

//             //Retorna o status 201 (created) e mostra o cliente que acabou de ser criado
//             return Created("", novoCliente);
//         }

//         //Rota 4: Update

//         [HttpPut("{id}")]
//         public IActionResult AtualizarCliente(int id, [FromBody] Cliente clienteAtualizado)
//         {
//             //O FirstOrDefault é um metodo que busca o primeiro valor que achar com o parametro passado ou retorna nulo. Imagino que seja excelente para manipular Id, que por sí só, tende a ser unico.
//             var clienteExistente = _listaClientes.FirstOrDefault(c => c.Id == id);

//             //Se for nulo, vamos devolver 404
//             if (clienteExistente == null)
//             {
//                 return NotFound($"Opa, o cliente com ID {id} não existe na base.");
//             }

//             //Se achou, vai atualizar as propriedades.
//             clienteExistente.Nome = clienteAtualizado.Nome;
//             clienteExistente.Telefone = clienteAtualizado.Telefone;
//             clienteExistente.Status = clienteAtualizado.Status;
//             clienteExistente.Idade = clienteAtualizado.Idade;

//             //Retorna o status 204 É o padrão REST para "Deu certo, atualizei, mas não tenho nada novo pra te devolver na tela".

//             return NoContent();
//         }

//         //Rota 5: delete
//         [HttpDelete("{id}")]
//         public IActionResult DeletarCliente(int id)
//         {
//             var clienteExistente = _listaClientes.FirstOrDefault(c => c.Id == id);

//             if (clienteExistente == null)
//             {
//                 return NotFound($"Opa, cliente com o ID {id} não existe na base de dados!");
//             }

//             _listaClientes.Remove(clienteExistente);

//             return NoContent();
//         }
//     }

//     public class Cliente
//     {
//         public int Id {get;set;}
//         public string? Nome {get;set;}
//         public string? Telefone {get;set;}
//         public bool Status {get;set;}
//         public int Idade {get;set;}
//     }
// }