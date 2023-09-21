namespace BackendApi.Data.Dtos.Shop;

public class ShopDtos
{
    public record ShopDtoReturn(int Id, string Name, string Description, string ContactInformation);
    public record ShopCreateDto(int Id, string Name, string Description, string ContactInformation);

    public record ShopUpdateDto(int Id, string Name, string Description, string ContactInformation);
}