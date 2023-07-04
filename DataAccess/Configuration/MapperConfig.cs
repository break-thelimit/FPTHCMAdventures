using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.GiftDto;
using DataAccess.Dtos.InventoryDto;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.NPCDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Dtos.RankDto;
using DataAccess.Dtos.RoleDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.TaskItemDto;
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
            
            #region Rank
            CreateMap<Rank, GetRankDto>().ReverseMap();
            CreateMap<Rank, UpdateRankDto>().ReverseMap();
            CreateMap<Rank, CreateRankDto>().ReverseMap();
            CreateMap<Rank, RankDto>().ReverseMap();
            #endregion 
            
            #region Answer
            CreateMap<Answer, AnswerDto>().ReverseMap();
            CreateMap<Answer, UpdateAnswerDto>().ReverseMap();
            CreateMap<Answer, CreateAnswerDto>().ReverseMap();
            CreateMap<Answer, GetAnswerDto>().ReverseMap();
            #endregion
            
            #region Gift
            CreateMap<Gift, GiftDto>().ReverseMap();
            CreateMap<Gift, UpdateGiftDto>().ReverseMap();
            CreateMap<Gift, CreateGiftDto>().ReverseMap();
            CreateMap<Gift, GetGiftDto>().ReverseMap();
            #endregion  
            
            #region Exchange History
            CreateMap<ExchangeHistory, ExchangeHistoryDto>().ReverseMap();
            CreateMap<ExchangeHistory, UpdateExchangeHistoryDto>().ReverseMap();
            CreateMap<ExchangeHistory, CreateExchangeHistoryDto>().ReverseMap();
            CreateMap<ExchangeHistory, GetExchangeHistoryDto>().ReverseMap();
            #endregion
            
            #region Inventory
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<Inventory, UpdateInventoryDto>().ReverseMap();
            CreateMap<Inventory, CreateInventoryDto>().ReverseMap();
            CreateMap<Inventory, GetInventoryDto>().ReverseMap();
            #endregion
            
            #region Item Inventory
            CreateMap<ItemIventory, ItemInventoryDto>().ReverseMap();
            CreateMap<ItemIventory, UpdateItemInventoryDto>().ReverseMap();
            CreateMap<ItemIventory, CreateItemInventoryDto>().ReverseMap();
            CreateMap<ItemIventory, GetItemInventoryDto>().ReverseMap();
            #endregion 
            
            #region Player
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, UpdatePlayerDto>().ReverseMap();
            CreateMap<Player, CreatePlayerDto>().ReverseMap();
            CreateMap<Player, GetPlayerDto>().ReverseMap();
            #endregion  
            
            #region Player History
            CreateMap<PlayHistory, PlayerHistoryDto>().ReverseMap();
            CreateMap<PlayHistory, UpdatePlayerHistoryDto>().ReverseMap();
            CreateMap<PlayHistory, CreatePlayerHistoryDto>().ReverseMap();
            CreateMap<PlayHistory, GetPlayerHistoryDto>().ReverseMap();
            #endregion
            
            #region Role
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, GetRoleDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            #endregion
            
            #region School
            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<School, GetSchoolDto>().ReverseMap();
            CreateMap<School, CreateSchoolDto>().ReverseMap();
            CreateMap<School, UpdateSchoolDto>().ReverseMap();
            #endregion
            
            #region School Event
            CreateMap<SchoolEvent, SchoolEventDto>().ReverseMap();
            CreateMap<SchoolEvent, GetSchoolEventDto>().ReverseMap();
            CreateMap<SchoolEvent, CreateSchoolEventDto>().ReverseMap();
            CreateMap<SchoolEvent, UpdateSchoolEventDto>().ReverseMap();
            #endregion 
            
            #region Task Item
            CreateMap<TaskItem, TaskItemDto>().ReverseMap();
            CreateMap<TaskItem, GetTaskItemDto>().ReverseMap();
            CreateMap<TaskItem, CreateTaskItemDto>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskItemDto>().ReverseMap();
            #endregion 
            
            #region Item
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Item, GetItemDto>().ReverseMap();
            CreateMap<Item, CreateItemDto>().ReverseMap();
            CreateMap<Item, UpdateItemDto>().ReverseMap();
            #endregion
            

            
            #region User
            CreateMap<User, ApiUserDto>().ReverseMap();
            CreateMap<User, AuthResponseDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            #endregion


        }
    }
}
