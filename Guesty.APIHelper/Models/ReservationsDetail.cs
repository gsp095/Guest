using System;
using System.Collections.Generic;
using System.Text;

namespace Guesty.APIHelper.Models
{
    public class Limitations
    {
        public List<object> availableStatuses { get; set; }
    }

    public class Integration
    {
        public string platform { get; set; }
        public string _id { get; set; }
        public Limitations limitations { get; set; }
    }

    public class Guest
    {
        public string _id { get; set; }
        public string fullName { get; set; }
    }

    public class Listing
    {
        public string _id { get; set; }
        public string title { get; set; }
    }

    public class Result
    {
        public string _id { get; set; }
        public Integration integration { get; set; }
        public string listingId { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public Guest guest { get; set; }
        public string accountId { get; set; }
        public string guestId { get; set; }
        public Listing listing { get; set; }
        public string confirmationCode { get; set; }
        public bool? isFrozen { get; set; }
    }

    public class ReservationsDetail
    {
        public List<Result> results { get; set; }
        public string title { get; set; }
        public int count { get; set; }
        public string fields { get; set; }
        public int limit { get; set; }
        public int skip { get; set; }
    }

}
