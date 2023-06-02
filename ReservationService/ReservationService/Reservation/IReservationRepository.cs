using BloodBankLibrary.Core.Accomodations;
using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Accomodations
{
    public interface IReservationRepository
    {
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        void Create(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(Reservation reservation);

    }
}
