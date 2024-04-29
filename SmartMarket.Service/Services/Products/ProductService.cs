using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Extensions;
using SmartMarket.Service.Commons.Helpers;
using SmartMarket.Service.DTOs.Products;
using SmartMarket.Service.Interfaces.Categories;
using SmartMarket.Service.Interfaces.Products;
using SmartMarket.Service.Interfaces.Users;

namespace SmartMarket.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IRepository<Product> _productRepository;

    public ProductService(
        IMapper mapper,
        ICategoryService categoryService,
        IRepository<Product> productsRepository,
        IUserService userService)
    {
        _productRepository = productsRepository;
        _mapper = mapper;
        _categoryService = categoryService;
        _userService = userService;
    }

    public async Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.BarCode == productForCreationDto.BarCode)
            .FirstOrDefaultAsync();

        var categoryTask = _categoryService.RetrieveByIdAsync(productForCreationDto.CategoryId);
        var userTask = _userService.RetrieveByIdAsync(productForCreationDto.UserId);

        if (product is not null)
        {
            if (productForCreationDto.Action)
            {
                if (product.CamePrice != productForCreationDto.CamePrice)
                {
                    product.CamePrice = (product.CamePrice + productForCreationDto.CamePrice) / 2;
                    product.Quantity += productForCreationDto.Quantity;
                    UpdatePriceAndPercentage(product, productForCreationDto);
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);

                    product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "Bu turdagi mahsulot mavjudligi uchun uning soniga qo'shib qo'yildi.");
                }
                else
                {
                    if (product.CamePrice == productForCreationDto.CamePrice)
                    {
                        product.Quantity += productForCreationDto.Quantity;
                        UpdatePriceAndPercentage(product, productForCreationDto);
                        product.UpdatedAt = DateTime.UtcNow;
                        await _productRepository.UpdateAsync(product);

                        product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
                        await _productRepository.UpdateAsync(product);
                        throw new CustomException(200, "Bu turdagi mahsulot mavjudligi uchun uning soniga qo'shib qo'yildi.");
                    }
                }
            }
            else
            {
                if (product.CamePrice != productForCreationDto.CamePrice)
                {
                    product.CamePrice = productForCreationDto.CamePrice;
                    product.Quantity += productForCreationDto.Quantity;
                    UpdatePriceAndPercentage(product, productForCreationDto);
                    product.UpdatedAt = DateTime.UtcNow;
                    await _productRepository.UpdateAsync(product);

                    product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
                    await _productRepository.UpdateAsync(product);
                    throw new CustomException(200, "Bu turdagi mahsulot mavjudligi uchun uning soniga qo'shib qo'yildi.");
                }
                else
                {
                    if (product.CamePrice == productForCreationDto.CamePrice)
                    {
                        product.Quantity += productForCreationDto.Quantity;
                        UpdatePriceAndPercentage(product, productForCreationDto);
                        product.UpdatedAt = DateTime.UtcNow;
                        await _productRepository.UpdateAsync(product);

                        product.TotalPrice = (product.SalePrice ?? 0) * product.Quantity;
                        await _productRepository.UpdateAsync(product);
                        throw new CustomException(200, "Bu turdagi mahsulot mavjudligi uchun uning soniga qo'shib qo'yildi.");
                    }
                }
            }

            return _mapper.Map<ProductForResultDto>(product);
        }

        var wwwRootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "Media", "Products");
        var assetsFolderPath = Path.Combine(wwwRootPath, "Media");
        var videosFolderPath = Path.Combine(assetsFolderPath, "Products");

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        if (!Directory.Exists(videosFolderPath))
        {
            Directory.CreateDirectory(videosFolderPath);
        }
        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(productForCreationDto.ImagePath.FileName);

        var fullPath = Path.Combine(wwwRootPath, fileName);

        using (var stream = File.OpenWrite(fullPath))
        {
            await productForCreationDto.ImagePath.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        string resultPath = Path.Combine("Media", "Products", fileName);

        var mappedProduct = _mapper.Map<Product>(productForCreationDto);
        mappedProduct.PCode = GeneratePCode();
        mappedProduct.TotalPrice = (productForCreationDto.SalePrice ?? 0) * productForCreationDto.Quantity;
        mappedProduct.ImagePath = resultPath;
        UpdatePriceAndPercentage(mappedProduct, productForCreationDto);
        mappedProduct.CreatedAt = DateTime.UtcNow;

        var result = await _productRepository.InsertAsync(mappedProduct);

        return _mapper.Map<ProductForResultDto>(result);
    }

    public void UpdatePriceAndPercentage(Product product, ProductForCreationDto dto)
    {
        if (dto.SalePrice == null && dto.PercentageOfPrice != null)
        {
            decimal newPrice = (product.CamePrice / 100) * dto.PercentageOfPrice.Value;
            product.SalePrice = product.CamePrice + newPrice;
            product.PercentageOfPrice = dto.PercentageOfPrice.Value;
        }
        else if (dto.SalePrice != null && dto.PercentageOfPrice == null)
        {
            decimal percentPrice = ((dto.SalePrice.Value - product.CamePrice) / product.CamePrice) * 100;
            product.PercentageOfPrice = percentPrice;
            product.SalePrice = dto.SalePrice;
        }
    }


    public async Task<bool> DeleteAsync(long id)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        var fullPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, product.ImagePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        return await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductForResultDto>> GetAllAsync(PaginationParams @params)
    {
        var products = await _productRepository.SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<ProductForResultDto>>(products);
    }

    public async Task<ProductForResultDto> GetByIdAsync(long id)
    {
        var product = await _productRepository.SelectAll()
             .Where(p => p.Id == id)
             .AsNoTracking()
             .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        return _mapper.Map<ProductForResultDto>(product);
    }

    public async Task<ProductForResultDto> UpdateAsync(long id, ProductForUpdateDto productForUpdateDto)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);
        var user = await _userService.RetrieveByIdAsync(productForUpdateDto.UserId);

        if (product.SalePrice is null && product.PercentageOfPrice is not null)
        {
            decimal? newPrice = (product.CamePrice / 100) * product.PercentageOfPrice;
            decimal? salePrice = product.CamePrice + newPrice;
            product.SalePrice = salePrice;
        }
        else if (product.SalePrice is not null && product.PercentageOfPrice is null)
        {
            decimal? percentPrice = ((product.CamePrice - product.SalePrice) / product.CamePrice) * 100;
            decimal? percentSale = 100 - percentPrice;
            product.PercentageOfPrice = percentSale;
        }


        var fullPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, product.ImagePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(productForUpdateDto.ImagePath.FileName);
        var rootPath = Path.Combine(WebHostEnviromentHelper.WebRootPath, "Media", "Products", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await productForUpdateDto.ImagePath.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }
        string resultImage = Path.Combine("Media", "Products", fileName);

        var mapped = _mapper.Map(productForUpdateDto, product);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.ImagePath = resultImage;

        await _productRepository.UpdateAsync(mapped);

        return _mapper.Map<ProductForResultDto>(mapped);
    }

    public string GeneratePCode()
    {
        var random = new Random();

        string pCode;
        do
        {
            int randomSuffix = random.Next(1000, 9999);
            pCode = "P" + randomSuffix.ToString();
        } while (_productRepository.SelectAll().Any(t => t.PCode == pCode));

        return pCode;
    }

}