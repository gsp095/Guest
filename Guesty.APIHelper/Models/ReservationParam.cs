using System;
using System.Collections.Generic;
using System.Text;

namespace Guesty.APIHelper.Models
{
    public class ReservationParam
    {
        public string viewId { get; set; }
        public string filters { get; set; }
        public string fields { get; set; }
        public string sort { get; set; }
        public int limit { get; set; }
        public int skip { get; set; }
    }
}
