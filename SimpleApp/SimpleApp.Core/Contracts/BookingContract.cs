using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Core.Contracts
{
    public class BookingContract
    {
        public int HotelId { get; set; }

        public DateTime Start { get; set; }

        public int Duration { get; set; }
    }
}
