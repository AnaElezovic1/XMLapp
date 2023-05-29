using BloodBankLibrary.Core.Accomodations;
using System;
using System.Collections.Generic;

namespace BloodBankLibrary.Core.Accomodations
{
    public interface IAccomodationRepository
    {
        IEnumerable<Accomodation> GetAll();
        Accomodation GetById(int id);
        void Create(Accomodation accomodation);
        void Update(Accomodation accomodation);
        void Delete(Accomodation accomodation);

    }
}
