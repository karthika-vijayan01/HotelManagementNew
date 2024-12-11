using System;
using System.Collections.Generic;

namespace HotelManagementNew.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNo { get; set; } = null!;

    public string RoomType { get; set; } = null!;

    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
