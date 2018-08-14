using System.Text;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class ReviewService : IReviewService
    {
        private readonly BusTicketContext _dbContext;
        private readonly ICustomerService _customerService;
        private readonly IBusCompanyService _busCompanyService;

        public ReviewService(BusTicketContext dbContext, ICustomerService customerService, IBusCompanyService busCompanyService)
        {
            this._dbContext = dbContext;
            this._customerService = customerService;
            this._busCompanyService = busCompanyService;
        }

        public string PublishReview(int customerId, decimal grade, string busCompanyName, string content)
        {
            var customer = this._customerService
                .GetCustomerById(customerId);

            var busCompany = this._busCompanyService
                .GetBusCompanyByName(busCompanyName);

            var review = new Review()
            {
                Customer = customer,
                Grade = grade,
                Content = content,
                BusCompany = busCompany
            };

            //this._busCompanyService.AddReview(review, busCompany.BusCompanyId);

            this._dbContext
                .Reviews
                .Add(review);

            this._dbContext.SaveChanges();

            string result =
                $"Customer {customer.FirstName} {customer.LastName} published review for company {busCompanyName}";

            return result;
        }
    }
}
