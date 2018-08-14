using BusTicket.Client.Core.Contracts;
using BusTicket.Services.Contracts;

namespace BusTicket.Client.Core.Commands
{
    public class PublishReviewCommand : IExecutable
    {
        private readonly IReviewService _reviewService;

        public PublishReviewCommand(IReviewService reviewService)
        {
            this._reviewService = reviewService;
        }

        public string Execute(string[] args)
        {
            var customerId = int.Parse(args[0]);
            var grade = decimal.Parse(args[1]);
            var busCompanyName = args[2];
            var content = args[3];

            var result = this._reviewService.PublishReview(customerId, grade, busCompanyName, content);

            return result;
        }
    }
}
