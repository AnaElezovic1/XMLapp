using AccommodationService.Acommodation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BloodBankLibrary.Core.Accomodations
{
    public class AccomodationRepository : IAccomodationRepository
    {
        private readonly IMongoCollection<AccomodationBE> _acommodations;

        public AccomodationRepository(
            IOptions<DatabaseSettings> databaseSettings
        )
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _acommodations = mongoDatabase.GetCollection<AccomodationBE>(databaseSettings.Value.AcommodationsCollectionName);
        }

        public void Create(AccomodationBE accomodation)
        {
            Random random = new();
            accomodation.Id = random.Next(10000);
            _acommodations.InsertOne(accomodation);
        }

        public void Delete(AccomodationBE accomodation)
        {
            _acommodations.DeleteOne(user => user.Id == accomodation.Id);
        }

        public IEnumerable<AccomodationBE> GetAll()
        {
            return _acommodations.Find(_ => true).ToList();
        }

        public AccomodationBE GetById(int id)
        {
            return _acommodations.Find(accomodation => accomodation.Id == id).FirstOrDefault();
        }

        public void Update(AccomodationBE accomodation)
        {
            _acommodations.FindOneAndReplace(oldUser => oldUser.Id == accomodation.Id, accomodation);
        }

    }
}
