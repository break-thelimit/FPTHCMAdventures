﻿using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.NPCDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.NpcService
{
    public interface INpcService
    {
        Task<ServiceResponse<IEnumerable<GetNpcDto>>> GetNpc();
        Task<ServiceResponse<NpcDto>> GetNpcById(Guid eventId);
        Task<ServiceResponse<NpcDto>> GetNpcByName(string npcName);
        Task<ServiceResponse<Guid>> CreateNewNpc(CreateNpcDto createMajorDto);
        Task<ServiceResponse<string>> UpdateNpc(Guid id, UpdateNpcDto majorDto);
    }
}
