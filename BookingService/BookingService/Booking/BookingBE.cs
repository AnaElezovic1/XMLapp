﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class BookingBE
    {
        private int id;
        private DateTime start;
        private DateTime end;
        private int price;
        private bool perperson;
        private bool autoaccept;
        private int accommodationId;
        public BookingBE(    )
        {
        }

        public global::System.Int32 Id { get => id; set => id = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public global::System.Int32 Price { get => price; set => price = value; }
        public bool Perperson { get => perperson; set => perperson = value; }
        public bool Autoaccept { get => autoaccept; set => autoaccept = value; }
        public int AccommodationId { get => accommodationId; set => accommodationId = value; }
    }
}