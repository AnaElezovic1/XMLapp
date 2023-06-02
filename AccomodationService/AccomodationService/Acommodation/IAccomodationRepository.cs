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
