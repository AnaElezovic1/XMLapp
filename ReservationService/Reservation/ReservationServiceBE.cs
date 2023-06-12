using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit.Encodings;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationServiceBE : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationServiceBE(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;

        }


        public void Create(ReservationBE reservation)
        {
            _reservationRepository.Create(reservation);
        }


        public IEnumerable<ReservationBE> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public void Update(ReservationBE reservation)
        {
            _reservationRepository.Update(reservation);
        }

        public ReservationBE GetById(int id)
        {
            return _reservationRepository.GetById(id);
        }

     
        public void Delete(ReservationBE reservation)
        {
            _reservationRepository.Delete(reservation);
        }
    }
}
