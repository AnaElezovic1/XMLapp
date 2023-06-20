using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class ReservationBE
    {
        private int id;
        private int guestId;
        private int bookingId;
        private int noOfGuests;
        private bool accepted;
        public ReservationBE(    )
        {
        }
        [BsonId]
        public int Id { get => id; set => id = value; }
        public int GuestId { get => guestId; set => guestId = value; }
        public int NoOfGuests { get => noOfGuests; set => noOfGuests = value; }
        public bool Accepted { get => accepted; set => accepted = value; }
        public int BookingId { get => bookingId; set => bookingId = value; }
    }
}