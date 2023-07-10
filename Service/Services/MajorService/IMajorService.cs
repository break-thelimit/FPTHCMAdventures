using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.MajorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.MajorService
{
    public interface IMajorService
    {
        Task<ServiceResponse<IEnumerable<GetMajorDto>>> GetMajor();
        Task<ServiceResponse<MajorDto>> GetEventById(Guid eventId);
        Task<ServiceResponse<Guid>> CreateNewMajor(CreateMajorDto createMajorDto);
        Task<ServiceResponse<string>> UpdateMajor(Guid id, UpdateMajorDto majorDto);
    }
}
