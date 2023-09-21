using AutoMapper;
using BackendApi.Data.Dtos.Shop;
using BackendApi.Data.Dtos.Software;
using BackendApi.Data.Entities;

namespace BackendApi.Data.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShopDtos.ShopCreateDto, Shop>();
        CreateMap<ShopDtos.ShopUpdateDto, Shop>();
        CreateMap<Shop, ShopDtos.ShopDtoReturn>();

        CreateMap<Software, SoftwareDtos.SoftwareDtoReturn>();
        CreateMap<SoftwareDtos.SoftwareCreateDto, Software>();
        CreateMap<SoftwareDtos.SoftwareUpdateDto, Software>();

    }
}