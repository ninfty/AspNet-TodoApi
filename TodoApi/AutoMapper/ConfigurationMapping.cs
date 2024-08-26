using AutoMapper;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.AutoMapper
{
    public class ConfigurationMapping : Profile
    {
        public ConfigurationMapping()
        {
            CreateMap<Todo, TodoDTO>().ReverseMap();
        }
    }
}
