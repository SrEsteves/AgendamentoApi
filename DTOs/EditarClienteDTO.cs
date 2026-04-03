namespace AgendamentoApi.DTOs;

public record EditarClienteDTO
(
    string NovoNome,
    string NovoTelefone,
    int NovaIdade
);
