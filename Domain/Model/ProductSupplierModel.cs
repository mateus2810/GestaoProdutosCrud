using System;

namespace Domain.Model
{
    public class ProductSupplierModel
    {
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCode { get; set; }
        public bool Situation { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierDescription { get; set; }
        public string CNPJ { get; set; }
        public string Name { get; set; }
    }
}
