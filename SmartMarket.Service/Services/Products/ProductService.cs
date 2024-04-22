using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Products;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Extensions;
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
        var category = await _categoryService.RetrieveByIdAsync(productForCreationDto.CategoryId);

        var user = await _userService.RetrieveByIdAsync(productForCreationDto.UserId);

        var product = await _productRepository.SelectAll()
            .Where(p => p.BarCode == productForCreationDto.BarCode)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is not null)
        {
            product.Quantity += productForCreationDto.Quantity;
            product.UpdatedAt = DateTime.UtcNow;
            await _productRepository.UpdateAsync(product);
            throw new CustomException(200, "Bu turdagi mahsulot mavjudligi uchun uning soniga qo'shib qo'yildi.");
        }

        var mappedProduct = _mapper.Map<Product>(productForCreationDto);
        mappedProduct.PCode = GeneratePCode();
        mappedProduct.CreatedAt = DateTime.UtcNow;

        if (mappedProduct.SalePrice is null && mappedProduct.PercentageOfPrice is not null)
        {
            decimal? newPrice = (mappedProduct.CamePrice / 100) * mappedProduct.PercentageOfPrice;
            decimal? salePrice = mappedProduct.CamePrice + newPrice;
            mappedProduct.SalePrice = salePrice;
        }
        else if (mappedProduct.SalePrice is not null && mappedProduct.PercentageOfPrice is null)
        {
            decimal? percentPrice = ((mappedProduct.SalePrice - mappedProduct.CamePrice) / mappedProduct.CamePrice) * 100;
            mappedProduct.PercentageOfPrice = (short)percentPrice;
        }

        var result = await _productRepository.InsertAsync(mappedProduct);

        return _mapper.Map<ProductForResultDto>(mappedProduct);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var product = await _productRepository.SelectAll()
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (product is null)
            throw new CustomException(404, "Mahsulot topilmadi.");

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
            product.PercentageOfPrice = (short?)percentSale;
        }
        var category = await _categoryService.RetrieveByIdAsync(productForUpdateDto.CategoryId);

        var user = await _userService.RetrieveByIdAsync(productForUpdateDto.UserId);

        var mapped = _mapper.Map(productForUpdateDto, product);
        mapped.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(mapped);

        return _mapper.Map<ProductForResultDto>(mapped);
    }



    private static int lastPCodeSuffix = 10;
    private static DateTime lastPCodeDate = DateTime.UtcNow.Date;

    public string GeneratePCode()
    {
        DateTime currentDate = DateTime.UtcNow.Date;

        if (currentDate > lastPCodeDate)
        {
            lastPCodeSuffix = 11;
            lastPCodeDate = currentDate;
        }

        string pCode;
        do
        {
            pCode = currentDate.ToString("P"+"yyyyMMdd") + lastPCodeSuffix.ToString();
            lastPCodeSuffix++;
        } while (_productRepository.SelectAll().Any(t => t.PCode == pCode));

        return pCode;
    }
}
