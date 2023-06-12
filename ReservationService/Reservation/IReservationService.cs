using BloodBankLibrary.Core.Accomodations;
using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Accomodations
{
    public interface IReservationService
    {
        IEnumerable<ReservationBE> GetAll();
        ReservationBE GetById(int id);
        void Create(ReservationBE reservation);
        void Update(ReservationBE reservation);

        void Delete(ReservationBE reservation);
    }
}
