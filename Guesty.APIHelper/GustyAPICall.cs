using Guesty.APIHelper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guesty.APIHelper
{
    public class GustyAPICall : IGustyAPICall
    {
        public ReservationsDetail GetReservations(ReservationParam reservationParam, ApiDetails apiDetails)
        {

            //dodo: add more remaining fields 
            string uri = "reservations?limit=" + reservationParam.limit + "&skip=" + reservationParam.skip;
            ApiHttpClient.ApiDetails = apiDetails;
            var jsonString = ApiHttpClient.Get(uri).Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<ReservationsDetail>(jsonString);
        }

    }
}
