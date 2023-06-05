
using Microsoft.EntityFrameworkCore;

namespace BloodBankLibrary.Core.Accomodations
{
    public class AccomodationRepository : IAccomodationRepository
    {
        private readonly Settings.WSDbContext _context;

        public AccomodationRepository(Settings.WSDbContext context)
        {
            _context = context;
        }

        public void Create(AccomodationBE accomodation)
        {
            _context.Accomodations.Add(accomodation);
            _context.SaveChanges();
        }

        public void Delete(AccomodationBE accomodation)
        {
            _context.Accomodations.Remove(accomodation);
            _context.SaveChanges();
        }

        public IEnumerable<AccomodationBE> GetAll()
        {
            return _context.Accomodations.ToList();
        }

        public AccomodationBE GetById(int id)
        {
            return _context.Accomodations.Find(id);
        }

        public void Update(AccomodationBE accomodation)
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
