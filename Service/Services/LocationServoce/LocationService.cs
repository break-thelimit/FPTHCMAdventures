﻿using AutoMapper;
using BusinessObjects.Model;
using DataAccess;
using DataAccess.Configuration;
using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Repositories.LocationRepositories;
using DataAccess.Repositories.MajorRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Service.Services.LocationServoce
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperConfig());
        });
        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> CreateNewLocation(CreateLocationDto createLocationDto)
        {
            if (await _locationRepository.ExistsAsync(s => s.LocationName == createLocationDto.LocationName))
            {
                return new ServiceResponse<Guid>
                {
                    Message = "Duplicated data: Location with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            
            var mapper = config.CreateMapper();
            var createLocation = mapper.Map<Location>(createLocationDto);
            createLocation.Id = Guid.NewGuid();
            createLocation.LocationName = createLocationDto.LocationName.Trim();
            await _locationRepository.AddAsync(createLocation);

            return new ServiceResponse<Guid>
            {
                Data = createLocation.Id,
                Message = "Successfully",
                Success = true,
                StatusCode = 201
            };
        }

        public async Task<ServiceResponse<LocationDto>> GetLocationById(Guid eventId)
        {
            try
            {

                var eventDetail = await _locationRepository.GetAsync<LocationDto>(eventId);

                if (eventDetail == null)
                {

                    return new ServiceResponse<LocationDto>
                    {
                        Message = "No rows",
                        StatusCode = 200,
                        Success = true
                    };
                }
                return new ServiceResponse<LocationDto>
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

        public async Task<ServiceResponse<IEnumerable<GetLocationDto>>> GetLocation()
        {
            var majorList = await _locationRepository.GetAllAsync<GetLocationDto>();

            if (majorList != null)
            {
                return new ServiceResponse<IEnumerable<GetLocationDto>>
                {
                    Data = majorList,
                    Success = true,
                    Message = "Successfully",
                    StatusCode = 200
                };
            }
            else
            {
                return new ServiceResponse<IEnumerable<GetLocationDto>>
                {
                    Data = majorList,
                    Success = false,
                    Message = "Faile because List event null",
                    StatusCode = 200
                };
            }
        }

        public async Task<ServiceResponse<bool>> UpdateLocation(Guid id, UpdateLocationDto updateLocationDto)
        {
            if (await _locationRepository.ExistsAsync(s => s.LocationName == updateLocationDto.LocationName && s.Id != id))
            {
                return new ServiceResponse<bool>
                {
                    Message = "Duplicated data: Location with the same name already exists.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                updateLocationDto.LocationName = updateLocationDto.LocationName.Trim();
                await _locationRepository.UpdateAsync(id, updateLocationDto);
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
            return await _locationRepository.Exists(id);
        }

        public async Task<ServiceResponse<string>> ImportDataFromExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "No file uploaded.",
                    Success = false,
                    StatusCode = 400
                };
            }

            if (file.ContentType != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "Invalid file format. Only Excel files are allowed.",
                    Success = false,
                    StatusCode = 400
                };
            }
            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet != null)
                        {
                            var rowCount = worksheet.Dimension.Rows;
                            var dataList = new List<LocationDto>();
                            
                            for (int row = 2; row <= rowCount-1; row++)
                            {
                                var data = new LocationDto
                                {
                                    Id = Guid.NewGuid(),
                                    X = Convert.ToDouble(worksheet.Cells[row, 1].Value),
                                    Y = Convert.ToDouble(worksheet.Cells[row, 2].Value),
                                    Z = Convert.ToDouble(worksheet.Cells[row, 3].Value),
                                    LocationName = worksheet.Cells[row, 4].Value.ToString().Trim(),
                                    Status = worksheet.Cells[row, 5].Value.ToString().Trim()
                                };

                                dataList.Add(data);
                            }
                            
                            // Start from row 2 to skip the header row
                            
                            var mapper = config.CreateMapper();
                            var locations = _mapper.Map<List<Location>>(dataList);
                            await _locationRepository.AddRangeAsync(locations);
                            await _locationRepository.SaveChangesAsync();

                        }
                    }
                }
                return new ServiceResponse<string>
                {
                    Data = "Upload successful.",
                    Success = true,
                    StatusCode = 200
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<string>
                {
                    Data = null,
                    Message = "Failed to process uploaded file.",
                    Success = false,
                    StatusCode = 500
                };
            }
        }
        public async Task<byte[]> ExportDataToExcel()
        {
            var dataList = await _locationRepository.GetAllAsync<GetLocationDto>(); // Replace with your repository method to get all locations

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Locations");

                // Add header row
                worksheet.Cells[1, 1].Value = "X";
                worksheet.Cells[1, 2].Value = "Y";
                worksheet.Cells[1, 3].Value = "Z";
                worksheet.Cells[1, 4].Value = "LocationName";
                worksheet.Cells[1, 5].Value = "Status";

                // Populate data rows
                for (int row = 0; row < dataList.Count; row++)
                {
                    var location = dataList[row];
                    worksheet.Cells[row + 2, 1].Value = location.X;
                    worksheet.Cells[row + 2, 2].Value = location.Y;
                    worksheet.Cells[row + 2, 3].Value = location.Z;
                    worksheet.Cells[row + 2, 4].Value = location.LocationName;
                    worksheet.Cells[row + 2, 5].Value = location.Status;
                }

                // Convert the package to a byte array
                byte[] excelData = package.GetAsByteArray();
                return excelData;
            }
        }
        public byte[] GenerateExcelTemplate()
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SampleDataLocation");

                // Thiết lập header cho các cột
                worksheet.Cells[1, 1].Value = "X";
                worksheet.Cells[1, 2].Value = "Y";
                worksheet.Cells[1, 3].Value = "Z";
                worksheet.Cells[1, 4].Value = "LocationName";
                worksheet.Cells[1, 5].Value = "Status";

                // Lưu file Excel vào MemoryStream
                var stream = new MemoryStream(package.GetAsByteArray());
                return stream.ToArray();
            }
        }
        public async Task<ServiceResponse<byte[]>> DownloadExcelTemplate()
        {
            byte[] fileContents;
            try
            {
                fileContents = GenerateExcelTemplate();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                return new ServiceResponse<byte[]>
                {
                    Data = null,
                    Message = "Failed to generate Excel template.",
                    Success = false,
                    StatusCode = 500
                };
            }

            // Trả về file Excel dưới dạng byte array
            return new ServiceResponse<byte[]>
            {
                Data = fileContents,
                Success = true,
                StatusCode = 200
            };
        }

        public async Task<ServiceResponse<PagedResult<LocationDto>>> GetLocationWithPage(QueryParameters queryParameters)
        {
            var pagedsResult = await _locationRepository.GetAllAsync<LocationDto>(queryParameters);
            return new ServiceResponse<PagedResult<LocationDto>>
            {
                Data = pagedsResult,
                Message = "Successfully",
                StatusCode = 200,
                Success = true
            };
        }

        public async Task<ServiceResponse<bool>> DisableLocation(Guid id)
        {
            var checkEvent = await _locationRepository.GetAsync<LocationDto>(id);

            if (checkEvent == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Message = "Failed",
                    StatusCode = 400,
                    Success = true
                };
            }
            else
            {
                checkEvent.Status = "INACTIVE";
                var locationData = _mapper.Map<Location>(checkEvent);

                await _locationRepository.UpdateAsync(id, locationData);
                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Success",
                    StatusCode = 200,
                    Success = true
                };
            }
        }
    }
}
