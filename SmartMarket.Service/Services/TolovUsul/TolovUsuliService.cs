using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartMarket.Data.IRepositories;
using SmartMarket.Domin.Configurations;
using SmartMarket.Domin.Entities.Tolovs;
using SmartMarket.Service.Commons.Exceptions;
using SmartMarket.Service.Commons.Extensions;
using SmartMarket.Service.DTOs.TolovUsullari;
using SmartMarket.Service.Interfaces.TolovUsuli;

namespace SmartMarket.Service.Services.TolovUsul;

public class TolovUsuliService : ITolovUsuliService
{
    private readonly IRepository<TolovUsuli> _tolovRepository;
    private readonly IMapper _mapper;

    public TolovUsuliService(IRepository<TolovUsuli> tolovRepository, IMapper mapper)
    {
        _mapper = mapper;
        _tolovRepository = tolovRepository;
    }

    public async Task<TolovUsuliForResultDto> CreateAsync(TolovUsuliForCreationDto dto)
    {
        var mapped = _mapper.Map<TolovUsuli>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await _tolovRepository.InsertAsync(mapped);

        return _mapper.Map<TolovUsuliForResultDto>(result);
    }


    public async Task<IEnumerable<TolovUsuliForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var categories = await _tolovRepository.SelectAll()
                .AsNoTracking()
                .ToPagedList(@params)
                .ToListAsync();

        return _mapper.Map<IEnumerable<TolovUsuliForResultDto>>(categories);
    }

    public async Task<TolovUsuliForResultDto> RetrieveByIdAsync(long id)
    {
        var category = await _tolovRepository.SelectAll()
                .Where(c => c.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        if (category is null)
            throw new CustomException(404, "Tolov usuli topilmadi.");

        return _mapper.Map<TolovUsuliForResultDto>(category);
    }
}
