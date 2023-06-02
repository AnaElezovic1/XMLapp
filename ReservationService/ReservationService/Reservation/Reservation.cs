using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class Reservation
    {
        private int id;
        private DateTime start;
        private DateTime end;
        private int price;
        private bool perperson;
        private bool autoaccept;
        public Reservation(    )
        {
        }

        public global::System.Int32 Id { get => id; set => id = value; }
        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public global::System.Int32 Priceperperson { get => price; set => price = value; }
        public bool Perperson { get => perperson; set => perperson = value; }
        public bool Autoaccept { get => autoaccept; set => autoaccept = value; }
    }
}