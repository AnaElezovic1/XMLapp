namespace AccommodationService.Acommodation
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string AcommodationsCollectionName { get; set; } = null!;
    }
}
