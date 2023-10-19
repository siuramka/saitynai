using System.ComponentModel.DataAnnotations;
using BackendApi.Auth.Models;

namespace BackendApi.Data.Entities;
public partial class Subscription
{
    public int TermInMonths { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public double TotalPrice { get; set; }
    public bool IsCanceled { get; set; } = false;
}
public partial class Subscription : IUserOwnedResource
{
    public int Id { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
    public string ShopUserId { get; set; }
    public ShopUser ShopUser { get; set; }
}
