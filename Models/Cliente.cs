using System;

namespace AgendamentoApi.Models
{
    public class Cliente
    {
        public int Id {get;set;}
        public string? Nome {get;set;}
        public string? Telefone {get;set;}
        public bool Status {get;set;}
        public int Idade {get;set;}
    }
}