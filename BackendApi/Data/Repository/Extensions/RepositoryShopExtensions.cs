using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using BackendApi.Data.Entities;

namespace BackendApi.Data.Repository.Extensions;

public static class RepositoryShopExtensions
{
    public static IQueryable<Shop> Sort(this IQueryable<Shop> Shops, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return Shops.OrderBy(e => e.Name);
        
        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Shop).GetProperties(BindingFlags.Public |
                                                       BindingFlags.Instance);
        var orderQueryBuilder = new StringBuilder();
        
        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;
            
            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi =>
                pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
            
            if (objectProperty == null)
                continue;
            
            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction},");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
        
        if (string.IsNullOrWhiteSpace(orderQuery))
            return Shops.OrderBy(e => e.Name);
        
        return Shops.OrderBy(orderQuery);
    }
}