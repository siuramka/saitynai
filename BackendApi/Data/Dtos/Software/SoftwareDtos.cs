using BackendApi.Data.Dtos.Shop;

namespace BackendApi.Data.Dtos.Software;

public class SoftwareDtos
{
    public record SoftwareDtoReturn(int Id, string Name,
        string Description,
        double? PriceMonthly,
        string Website,
        string Instructions);
    
    public record SoftwareDtoReturnAll(int Id, string Name,
        string Description,
        double? PriceMonthly,
        string Website,
        string Instructions, ShopDtos.ShopDtoReturn Shop);

    public record SoftwareCreateDto(string Name,
        string Description,
        double? PriceMonthly,
        string Website,
        string Instructions);

    public record SoftwareUpdateDto(string Name,
        string Description,
        double? PriceMonthly,
        string Website,
        string Instructions);
}