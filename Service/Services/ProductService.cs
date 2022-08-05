using AutoMapper;
using core6.DTOs;
using core6.Models;
using core6.Repositories;
using core6.Services;
using core6.UnitOfWorks;

namespace Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var prodcutsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return prodcutsDto;
        }
    }
}
