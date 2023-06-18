using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Settings.WSDbContext _context;

        public ReservationRepository(Settings.WSDbContext context)
        {
            _context = context;
        }

        public void Create(ReservationBE reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
        }

        public void Delete(ReservationBE reservation)
        {
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public IEnumerable<ReservationBE> GetAll()
        {
            return _context.Reservations.ToList();
        }

        public ReservationBE GetById(int id)
        {
            return _context.Reservations.Find(id);
        }

        public void Update(ReservationBE reservation)
        {
            var entry = _context.Find(typeof(ReservationBE),reservation.Id);
            _context.Entry(entry).CurrentValues.SetValues(reservation);


            _context.SaveChanges();

        }

    }
}
