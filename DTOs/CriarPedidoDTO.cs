using System.ComponentModel.DataAnnotations;


namespace AgendamentoApi.DTOs
{
    public record CriarPedidoDTO(
        [Required(ErrorMessage = "Informar o valor do pedido!")]
        [Range(0.10, 50000, ErrorMessage = "O pedido deve ter no minimo R$ 0,10 e no máximo R$ 50.000,00")]
        decimal ValorTotal,

        [Required(ErrorMessage = "Informar o id do cliente!")]
        int ClienteId 
    );
}
