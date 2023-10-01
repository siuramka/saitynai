using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Dtos.Subscription;
using BackendApi.Data.Entities;

namespace BackendApi.Data.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Shop, ShopDtos.ShopDtoReturn>();
        CreateMap<ShopDtos.ShopCreateDto, Shop>();
        CreateMap<ShopDtos.ShopUpdateDto, Shop>();

        CreateMap<Software, SoftwareDtos.SoftwareDtoReturn>();
        CreateMap<Software, SoftwareDtos.SoftwareDtoReturnAll>();
        CreateMap<SoftwareDtos.SoftwareCreateDto, Software>();
        CreateMap<SoftwareDtos.SoftwareUpdateDto, Software>();
        
        CreateMap<Subscription, SubscriptionDtos.SubscriptionDtoReturn>();
        CreateMap<Subscription, SubscriptionDtos.SubscriptionDtoReturnAll>();
        CreateMap<SubscriptionDtos.SubscriptionCreateDto, Subscription>();
        CreateMap<SubscriptionDtos.SubscriptionUpdateDto, Subscription>();
    }
}