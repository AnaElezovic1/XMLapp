using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit.Encodings;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;

        }


        public void Create(Reservation reservation)
        {
            _reservationRepository.Create(reservation);
        }


        public IEnumerable<Reservation> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public void Update(Reservation reservation)
        {
            _reservationRepository.Update(reservation);
        }

        public Reservation GetById(int id)
        {
            return _reservationRepository.GetById(id);
        }

     
        public void Delete(Reservation reservation)
        {
            _reservationRepository.Delete(reservation);
        }
    }
}
