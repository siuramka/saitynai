namespace BackendApi.Data.Dtos.Subscription;

public class SubscriptionDtos
{
    public record SubscriptionDtoReturn(int Id, int TermInMonths,
        DateTime Start,
        DateTime End,
        double TotalPrice,
        bool IsCanceled);
    public record SubscriptionCreateDto(int TermInMonths);
    public record SubscriptionUpdateDto(int TermInMonths, bool IsCanceled);
}