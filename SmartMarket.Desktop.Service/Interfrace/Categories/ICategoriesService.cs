using SmartMarket.Domin.Entities.Categories;
using SmartMarket.Service.DTOs.Categories;

namespace SmartMarket.Desktop.Service.Interfrace.Categories
{
    public interface ICategoriesService
    {
        public Task<Category> CreateAsync(CategoryForCreationDto category);
        public Task<Category> UpdateAsync(long id,CategoryForCreationDto category);
        public Task<bool> DeleteAsync(long id);
        public Task<IList<Category>> GetcategoriesAsync();
    }
}
