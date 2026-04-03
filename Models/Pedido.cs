using System;

namespace AgendamentoApi.Models
{
    public class Pedido
    {
        public int Id {get;set;}
        public DateTime DataPedido {get;set;} = DateTime.Now;
        public decimal ValorTotal {get;set;}
        public string Status {get;set;} = "Aguardando Pagamento";
        public int ClienteId{get;set;}
        public Cliente? Cliente {get;set;}
    }
}