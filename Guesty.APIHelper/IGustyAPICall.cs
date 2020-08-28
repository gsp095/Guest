using Guesty.APIHelper.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guesty.APIHelper
{
   public interface IGustyAPICall
    {
        ReservationsDetail GetReservations(ReservationParam reservationParam, ApiDetails apiDetails);
    }
}
