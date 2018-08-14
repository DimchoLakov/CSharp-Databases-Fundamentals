using System;
using System.Linq;
using System.Text;
using BusTicket.Data;
using BusTicket.Models;
using BusTicket.Services.Contracts;

namespace BusTicket.Services
{
    public class BusCompanyService : IBusCompanyService
    {
        private readonly BusTicketContext _dbContext;

        public BusCompanyService(BusTicketContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool Exists(string name)
        {
            return this.GetBusCompanyByName(name) != null;
        }

        public void CreateBusCompany(string name, string nationality)
        {
            var busCompany = new BusCompany()
            {
                Name = name,
                Nationality = nationality
            };

            this._dbContext
                .BusCompanies
                .Add(busCompany);

            this._dbContext
                .SaveChanges();
        }

        public BusCompany GetBusCompanyById(int busCompanyId)
        {
            var busCompany = this._dbContext
                .BusCompanies
                .FirstOrDefault(x => x.BusCompanyId == busCompanyId);

            return busCompany;
        }

        public BusCompany GetBusCompanyByName(string name)
        {
            var busCompany = this._dbContext
                .BusCompanies
                .FirstOrDefault(x => x.Name == name);

            return busCompany;
        }

        public BusCompany RemoveBusCompany(int busCompanyId)
        {
            var busCompany = this.GetBusCompanyById(busCompanyId);

            this._dbContext
                .BusCompanies
                .Remove(busCompany);

            this._dbContext
                .SaveChanges();

            return busCompany;
        }

        public void AddReview(Review review, int busCompanyId)
        {
            var busCompany = this.GetBusCompanyById(busCompanyId);

            busCompany.Reviews.Add(review);

            this._dbContext
                .SaveChanges();
        }

        public void AddTrip(Trip trip, int busCompanyId)
        {
            var busCompany = this.GetBusCompanyById(busCompanyId);

            this._dbContext
                .Trips
                .Add(trip);

            this._dbContext
                .SaveChanges();
        }

        public string PrintReviews(int busCompanyId)
        {
            var sb = new StringBuilder();

            var busCompany = this.GetBusCompanyById(busCompanyId);

            foreach (var rev in busCompany.Reviews)
            {
                sb.AppendLine($"{rev.ReviewId} {rev.Grade} {rev.DateOfPublishing}")
                    .AppendLine($"{rev.Customer.FirstName} {rev.Customer.LastName}")
                    .AppendLine(rev.Content);
            }

            return sb.ToString().Trim();
        }
    }
}
