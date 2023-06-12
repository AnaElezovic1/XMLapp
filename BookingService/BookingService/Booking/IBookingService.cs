using BloodBankLibrary.Core.Accomodations;
using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Accomodations
{
    public interface IBookingService
    {
        IEnumerable<BookingBE> GetAll();
        BookingBE GetById(int id);
        void Create(BookingBE reservation);
        void Update(BookingBE reservation);

        void Delete(BookingBE reservation);
    }
}
