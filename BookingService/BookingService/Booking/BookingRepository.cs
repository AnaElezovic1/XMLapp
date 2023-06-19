using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Accomodations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Settings.WSDbContext _context;

        public BookingRepository(Settings.WSDbContext context)
        {
            _context = context;
        }

        public void Create(BookingBE reservation)
        {
            _context.Bookings.Add(reservation);
            _context.SaveChanges();
        }

        public void Delete(BookingBE reservation)
        {
            _context.Bookings.Remove(reservation);
            _context.SaveChanges();
        }
        
        public IEnumerable<BookingBE> GetAll()
        {
            return _context.Bookings.ToList();
        }

        public BookingBE GetById(int id)
        {
            return _context.Bookings.Find(id);
        }

        public void Update(BookingBE reservation)
        {
            _context.Entry(reservation).State = EntityState.Modified;

                _context.SaveChanges();


        }

    }
}
