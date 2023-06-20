using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using BookingService.Booking;
using Microsoft.Extensions.Options;

namespace BloodBankLibrary.Core.Accomodations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMongoCollection<BookingBE> _bookings;

        public BookingRepository(
            IOptions<DatabaseSettings> databaseSettings
        )
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _bookings = mongoDatabase.GetCollection<BookingBE>(databaseSettings.Value.BookingsCollectionName);
        }

        public void Create(BookingBE reservation)
        {
            Random random = new();
            reservation.Id = random.Next(10000);
            _bookings.InsertOne(reservation);
        }

        public void Delete(BookingBE reservation)
        {
            _bookings.DeleteOne(r => r.Id == reservation.Id);
        }
        
        public IEnumerable<BookingBE> GetAll()
        {
            return _bookings.Find(_ => true).ToList();
        }

        public BookingBE GetById(int id)
        {
            return _bookings.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Update(BookingBE reservation)
        {
            _bookings.FindOneAndReplace(oldReservation => oldReservation.Id == reservation.Id, reservation);
        }

    }
}
