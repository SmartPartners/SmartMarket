using SmartMarket.Domin.Entities.ContrAgents;
using SmartMarket.Service.DTOs.ContrAgents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Desktop.Service.Interfrace.ContrAgents
{
    public interface IContrAgentService
    {
        public Task<ContrAgent> CreateAsync(ContrAgentForCreationDto entity);

        public Task<IList<ContrAgent>> GetContrAgentsAsync();
    }
}
