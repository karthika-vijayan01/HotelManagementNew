using System;
using System.Collections.Generic;

namespace HotelManagementNew.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? GuestId { get; set; }

    public int? RoomId { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Guest? Guest { get; set; }

    public virtual Room? Room { get; set; }

    public virtual ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
}
