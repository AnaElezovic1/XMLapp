using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly Settings.WSDbContext _context;

        public ReservationRepository(Settings.WSDbContext context)
        {
            _context = context;
        }

        public void Create(Reservation reservation)
        {
            _context.Accomodations.Add(reservation);
            _context.SaveChanges();
        }

        public void Delete(Reservation reservation)
        {
            _context.Accomodations.Remove(reservation);
            _context.SaveChanges();
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Accomodations.ToList();
        }

        public Reservation GetById(int id)
        {
            return _context.Accomodations.Find(id);
        }

        public void Update(Reservation reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

    }
}
