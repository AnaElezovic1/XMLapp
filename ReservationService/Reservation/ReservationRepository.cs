using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using MongoDB.Driver;
using ReservationService.Reservation;
using Microsoft.Extensions.Options;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly IMongoCollection<ReservationBE> _reservations;

        public ReservationRepository(
            IOptions<DatabaseSettings> databaseSettings
        )
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _reservations = mongoDatabase.GetCollection<ReservationBE>(databaseSettings.Value.ReservationsCollectionName);
        }

        public void Create(ReservationBE reservation)
        {
            Random random = new();
            reservation.Id = random.Next(10000);
            _reservations.InsertOne(reservation);
        }

        public void Delete(ReservationBE reservation)
        {
            _reservations.DeleteOne(res => res == reservation);
        }

        public IEnumerable<ReservationBE> GetAll()
        {
            return _reservations.Find(_ => true).ToList();
        }

        public ReservationBE GetById(int id)
        {
            return _reservations.Find(user => user.Id == id).FirstOrDefault();
        }

        public void Update(ReservationBE reservation)
        {
            _reservations.FindOneAndReplace(oldUser => oldUser.Id == reservation.Id, reservation);
        }

    }
}
