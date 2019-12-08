using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Core.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int HotelId { get; set; }
    }
}
