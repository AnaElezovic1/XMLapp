using System;
using System.Collections.Generic;
using System.Linq;
using MimeKit.Encodings;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class BookingServiceBE : IBookingService
    { 
        
        private readonly IBookingRepository _reservationRepository;
        public BookingServiceBE(IBookingRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;

        }


        public void Create(BookingBE reservation)
        {
            _reservationRepository.Create(reservation);
        }


        public IEnumerable<BookingBE> GetAll()
        {
            return _reservationRepository.GetAll();
        }

        public void Update(BookingBE reservation)
        {
            _reservationRepository.Update(reservation);
        }

        public BookingBE GetById(int id)
        {
            return _reservationRepository.GetById(id);
        }

     
        public void Delete(BookingBE reservation)
        {
            _reservationRepository.Delete(reservation);
        }
    }
}
