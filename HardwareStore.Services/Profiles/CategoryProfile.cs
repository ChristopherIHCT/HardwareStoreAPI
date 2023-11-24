using AutoMapper;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;

namespace HardwareStore.Services.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDtoResponse>();
        CreateMap<CategoryDtoRequest, Category>();
    }
}