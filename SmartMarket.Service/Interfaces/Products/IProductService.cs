using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.Products;

namespace SmartMarket.Service.Interfaces.Products;

public interface IProductService
{
    Task<bool> DeleteAsync(long id);
    Task<ProductForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<ProductForResultDto>> GetAllAsync(PaginationParams @params);
    Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto);
    Task<ProductForResultDto> UpdateAsync(long id, ProductForUpdateDto productForUpdateDto);
}
