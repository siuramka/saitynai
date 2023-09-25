using BackendApi.Helpers;

namespace BackendApi.Data.Dtos.Shop;

public class ShopParameters : QueryStringParameters
{
    public ShopParameters()
    {
        OrderBy = "name"; //default sort by
    }
}