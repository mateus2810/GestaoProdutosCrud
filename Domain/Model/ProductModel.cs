﻿using System;

namespace Domain.Model
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public int? FornecedorID { get; set; }
    }
}
