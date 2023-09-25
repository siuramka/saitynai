namespace BackendApi.Data.Entities;
public partial class Software
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double PriceMonthly { get; set; } = 0;
    public string Website { get; set; }
    public string Instructions { get; set; }
}
public partial class Software
{
    public int Id { get; set; }
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } 
}