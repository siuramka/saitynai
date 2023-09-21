namespace BackendApi.Data.Entities;

public partial class Rating
{
    public int NumericalValue { get; set; }
    public DateTime CreatedAt { get; set; }
}

public partial class Rating
{
    public int Id { get; set; }
    public int SoftwareId { get; set; }
    public Software Software { get; set; }
    
    // public int ShopUserId { get; set; }
    // public ShopUser ShopUser { get; set; }
}