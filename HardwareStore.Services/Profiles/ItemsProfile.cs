using AutoMapper;
using HardwareStore.Dto.Request;
using HardwareStore.Dto.Response;
using HardwareStore.Entities;

namespace HardwareStore.Services.Profiles;

public class ItemsProfile : Profile
{
    public ItemsProfile()
    {
        CreateMap<Items, ItemDtoResponse>();
        CreateMap<ItemDtoRequest, Items>();
    }
}