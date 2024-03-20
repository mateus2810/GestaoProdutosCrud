﻿using Domain.Input;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllProduct();
        public Task<bool> InsertProduct(ProductInput product);
        public Task<bool> UpdateProduct(int id, ProductInput product);
        public Task<bool> DeleteProduct(int id);
    }
}
