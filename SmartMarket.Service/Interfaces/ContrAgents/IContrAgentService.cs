﻿using SmartMarket.Domin.Configurations;
using SmartMarket.Service.DTOs.ContrAgents;

namespace SmartMarket.Service.Interfaces.ContrAgents;

public interface IContrAgentService
{
    Task<bool> RemoveAsync(long id);
    Task<ContrAgentForResultDto> RetrieveByIdAsync(long id);
    Task<ContrAgentForResultDto> CreateAsync(ContrAgentForCreationDto dto);
    Task<ContrAgentForResultDto> PayForProductsAsync(long agentId, decimal paid);
    Task<ContrAgentForResultDto> ModifyAsync(long id, ContrAgentForUpdateDto dto);
    Task<IEnumerable<ContrAgentForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
