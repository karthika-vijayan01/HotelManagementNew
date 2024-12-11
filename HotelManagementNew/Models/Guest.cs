using System;
using System.Collections.Generic;

namespace HotelManagementNew.Models;

public partial class Guest
{
    public int GuestId { get; set; }

    public string GuestName { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string AadhaarNumber { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
