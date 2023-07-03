using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.NPCDto;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DataAccess.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Event
            CreateMap<Event, GetEventDto>().ReverseMap();
            CreateMap<Event, UpdateEventDto>().ReverseMap();
            CreateMap<Event, CreateEventDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            #endregion  
            
            #region Task
            CreateMap<Task, GetTaskDto>().ReverseMap();
            CreateMap<Task, UpdateTaskDto>().ReverseMap();
            CreateMap<Task, CreateTaskDto>().ReverseMap();
            CreateMap<Task, TaskDto>().ReverseMap();
            #endregion 


            #region Event Task
            CreateMap<EventTask, GetEventTaskDto>().ReverseMap();
            CreateMap<EventTask, UpdateEventTaskDto>().ReverseMap();
            CreateMap<EventTask, CreateEventTaskDto>().ReverseMap();
            CreateMap<EventTask, EventTaskDto>().ReverseMap();
            #endregion

            #region Major
            CreateMap<Major, GetMajorDto>().ReverseMap();
            CreateMap<Major, UpdateMajorDto>().ReverseMap();
            CreateMap<Major, CreateMajorDto>().ReverseMap();
            CreateMap<Major, MajorDto>().ReverseMap();
            #endregion  
            
            
            #region Question
            CreateMap<Question, GetQuestionDto>().ReverseMap();
            CreateMap<Question, UpdateQuestionDto>().ReverseMap();
            CreateMap<Question, CreateQuestionDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
            #endregion 
            
            #region NPC
            CreateMap<Npc, GetNpcDto>().ReverseMap();
            CreateMap<Npc, UpdateNpcDto>().ReverseMap();
            CreateMap<Npc, CreateNpcDto>().ReverseMap();
            CreateMap<Npc, NpcDto>().ReverseMap();
            #endregion 
            
            #region Location
            CreateMap<Location, GetLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();
            CreateMap<Location, CreateLocationDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            #endregion
            
            
            #region User
            CreateMap<User, ApiUserDto>().ReverseMap();
            CreateMap<User, AuthResponseDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            #endregion


        }
    }
}
