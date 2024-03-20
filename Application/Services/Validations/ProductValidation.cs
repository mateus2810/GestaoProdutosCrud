using Domain.Input;
using System;

namespace Application.Services.Validations
{
    public static class ProductValidation
    {
        public static void Validate(ProductInput productInput)
        {
            if (productInput.DataFabricacao >= productInput.DataValidade)
            {
                throw new ArgumentException("A data de fabricação deve ser anterior à data de validade.");
            }
        }
    }
}
