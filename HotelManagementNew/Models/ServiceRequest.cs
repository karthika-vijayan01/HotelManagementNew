using System;
using System.Collections.Generic;

namespace HotelManagementNew.Models;

public partial class ServiceRequest
{
    public int RequestId { get; set; }

    public int? BookingId { get; set; }

    public int? ServiceId { get; set; }

    public DateTime RequestDate { get; set; }

    public virtual Booking? Booking { get; set; }

    public virtual Service? Service { get; set; }
}
