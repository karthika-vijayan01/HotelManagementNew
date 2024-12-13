namespace HotelManagementNew.ViewModel
{
    public class GuestReportViewModel
    {
        public int GuestId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string AadhaarNumber { get; set; } = string.Empty;
        public DateTime? BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? RequestDate { get; set; }
        public string ServiceName { get; set; } = string.Empty;
    }
}
