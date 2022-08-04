using core6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core6.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
