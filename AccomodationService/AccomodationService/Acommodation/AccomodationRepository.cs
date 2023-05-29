using BloodBankLibrary.Core.Accomodations;
using Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BloodBankLibrary.Core.Accomodations
{
    public class AccomodationRepository : IAccomodationRepository
    {
        private readonly Settings.WSDbContext _context;

        public AccomodationRepository(Settings.WSDbContext context)
        {
            _context = context;
        }

        public void Create(Accomodation accomodation)
        {
            _context.Accomodations.Add(accomodation);
            _context.SaveChanges();
        }

        public void Delete(Accomodation accomodation)
        {
            _context.Accomodations.Remove(accomodation);
            _context.SaveChanges();
        }

        public IEnumerable<Accomodation> GetAll()
        {
            return _context.Accomodations.ToList();
        }

        public Accomodation GetById(int id)
        {
            return _context.Accomodations.Find(id);
        }

        public void Update(Accomodation accomodation)
        {
            _context.Entry(accomodation).State = EntityState.Modified;

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
