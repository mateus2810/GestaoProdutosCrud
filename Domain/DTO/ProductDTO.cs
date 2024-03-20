using System;

namespace Domain.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int? SupplierID { get; set; }
    }
}
