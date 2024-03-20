using System;

namespace Domain.Input
{
    public class ProductInput
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int FornecedorId { get; set; }
    }
}
