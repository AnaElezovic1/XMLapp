namespace BloodBankLibrary.Core.Accomodations
{
    public interface IAccomodationRepository
    {
        IEnumerable<AccomodationBE> GetAll();
        AccomodationBE GetById(int id);
        void Create(AccomodationBE accomodation);
        void Update(AccomodationBE accomodation);
        void Delete(AccomodationBE accomodation);

    }
}
