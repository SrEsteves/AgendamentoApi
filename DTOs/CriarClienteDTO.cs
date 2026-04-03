namespace AgendamentoApi.DTOs;

public record CriarClienteDTO(
    string Nome,
    string Telefone,
    int Idade
);