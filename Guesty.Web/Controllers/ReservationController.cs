using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Guesty.APIHelper;
using Guesty.APIHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Guesty.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IGustyAPICall gustyAPICall;
        private readonly IOptions<ApiDetails> apiDetails;
        public ReservationController(IGustyAPICall gustyAPICall, IOptions<ApiDetails> apiDetails)
        {
            this.gustyAPICall = gustyAPICall;
            this.apiDetails = apiDetails;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ReservationsDetail GetReservation(ReservationParam reservationParam)
        {
            var reservation = gustyAPICall.GetReservations(reservationParam, apiDetails.Value);

            return reservation;
        }
    }
}