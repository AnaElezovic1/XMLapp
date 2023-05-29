using System;
using System.ComponentModel.DataAnnotations;

namespace BloodBankLibrary.Core.Accomodations
{
    public class Accomodation
    {
        [Key]
        private int id;
        private string name;
        private string description;
        private string location;
        private string images;
        private int beds;

        public Accomodation(    )
        {
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Location { get => location; set => location = value; }
        public string Images { get => images; set => images = value; }
        public int Beds { get => beds; set => beds = value; }
    }
}