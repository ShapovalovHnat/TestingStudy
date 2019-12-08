using SimpleApp.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Core.Interfaces
{
    public interface IBookingService
    {
        bool IsFree(BookingContract booking);

        int Book(BookingContract booking);

        bool Cancel(int id);
    }
}
