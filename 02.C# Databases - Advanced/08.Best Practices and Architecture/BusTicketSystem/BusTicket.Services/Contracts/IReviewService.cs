namespace BusTicket.Services.Contracts
{
    public interface IReviewService
    {
        string PublishReview(int customerId, decimal grade, string busCompanyName, string content);
    }
}
