using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Configuration;
using DataAccess.Dtos.ImageUploadDto;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Repositories.ImageRepository;
using DataAccess.Repositories.ItemInventoryRepositories;
using DataAccess.Repositories.ItemRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public ItemService(IItemRepository itemRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }
        public async Task<ServiceResponse<Guid>> CreateNewItem(CreateItemDto createItemDto)
        {
            if (await _itemRepository.ExistsAsync(s => s.Name == createItemDto.Name ))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Item with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            if (await _itemRepository.ExistsAsync(s => s.ImageUrl == createItemDto.ImageUrl))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Item with the same ImageUrl already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            var mapper = config.CreateMapper();
            var eventTaskcreate = mapper.Map<Item>(createItemDto);
            eventTaskcreate.Id = Guid.NewGuid();
            eventTaskcreate.Name = createItemDto.Name.Trim();
            eventTaskcreate.Description = createItemDto.Description.Trim();
            eventTaskcreate.Type = createItemDto.Type.Trim();
            eventTaskcreate.Status = createItemDto.Status.Trim();
           

            // Tải lên hình ảnh và lưu URL
            string uploadedImageUrl = await _imageRepository.UploadImageAndReturnUrlAsync(createItemDto.Image);

            eventTaskcreate.ImageUrl = uploadedImageUrl; // Gán URL của hình ảnh cho thuộc tính ImageUrl

            await _itemRepository.AddAsync(eventTaskcreate);

            return new ServiceResponse<Guid>
            {
                Data = eventTaskcreate.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<string>> DisableStausItem(Guid id)
        {
            var checkEvent = await _itemRepository.GetAsync<ItemDto>(id);

            if (checkEvent == null)
            {
                return new ServiceResponse<string>
                {
                    Data = "null",
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
            else
            {
                checkEvent.Status = "INACTIVE";
                var itemData = _mapper.Map<Item>(checkEvent);

                await _itemRepository.UpdateAsync(id, itemData);
                return new ServiceResponse<string>
                {
                    Data = checkEvent.Status,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<IEnumerable<ItemDto>>> GetItem()
        {
            var majorList = await _itemRepository.GetAllAsync<ItemDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<ItemDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<ItemDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<ItemDto>> GetItemById(Guid eventId)
        {
            try
            {

                var eventDetail = await _itemRepository.GetAsync<ItemDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<ItemDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<ItemDto>
                {
                    Data = eventDetail,
                    Message = "Successfully",
                    StatusCode = 200,
                    Success = true
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> UpdateItem(Guid id, UpdateItemDto updateItemDto)
        {
            if (await _itemRepository.ExistsAsync(s => s.Name == updateItemDto.Name && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Item with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            if (await _itemRepository.ExistsAsync(s => s.ImageUrl == updateItemDto.ImageUrl && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Item with the same ImageUrl already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                updateItemDto.Name = updateItemDto.Name.Trim();
                updateItemDto.Description = updateItemDto.Description.Trim();
                updateItemDto.Type = updateItemDto.Type.Trim();
                updateItemDto.Status = updateItemDto.Status.Trim();
                await _itemRepository.UpdateAsync(id, updateItemDto);
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success edit",
                    Success = true,
                    StatusCode = 202
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EventTaskExists(id))
                {
                    return new ServiceResponse<bool>
                    {
                        Data = false,
                        Message = "Invalid Record Id",
                        Success = false,
                        StatusCode = 500
                    };
                }
                else
                {
                    throw;
                }
            }
        }
        private async Task<bool> EventTaskExists(Guid id)
        {
            return await _itemRepository.Exists(id);
        }
       
    }
}
