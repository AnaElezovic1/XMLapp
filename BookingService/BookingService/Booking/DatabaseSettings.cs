namespace BookingService.Booking
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string BookingsCollectionName { get; set; } = null!;
    }
}
