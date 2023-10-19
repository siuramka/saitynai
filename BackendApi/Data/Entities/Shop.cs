using System.ComponentModel.DataAnnotations;
using BackendApi.Auth.Models;

namespace BackendApi.Data.Entities;

public partial class Shop
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ContactInformation { get; set; }
}

public partial class Shop : IUserOwnedResource
{
    public int Id { get; set; }
    public ICollection<Software> Softwares { get; set; }
    
     public string ShopUserId { get; set; }
     public ShopUser ShopUser { get; set; }
}

