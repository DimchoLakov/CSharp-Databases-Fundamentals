using System;
using BusTicket.Models.Enums;

namespace BusTicket.Models
{
    public class Trip
    {
        public int TripId { get; set; }

        public TimeSpan DepartureTime { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public TripStatus Status { get; set; }

        public int OriginBusStationId { get; set; }
        public virtual BusStation OriginBusStation { get; set; }

        public int DestinationBusStationId { get; set; }
        public virtual BusStation DestinationBusStation { get; set; }

        public int BusCompanyId { get; set; }
        public virtual BusCompany BusCompany { get; set; }
    }
}
