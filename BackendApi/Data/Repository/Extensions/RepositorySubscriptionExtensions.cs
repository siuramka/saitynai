﻿using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using BackendApi.Data.Entities;

namespace BackendApi.Data.Repository.Extensions;

public static class RepositorySubscriptionExtensions
{
    public static IQueryable<Subscription> Sort(this IQueryable<Subscription> Subscriptions, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return Subscriptions.OrderBy(e => e.Start);
        
        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(Subscription).GetProperties(BindingFlags.Public |
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
            return Subscriptions.OrderBy(e => e.Start);
        
        return Subscriptions.OrderBy(orderQuery);
    }
}