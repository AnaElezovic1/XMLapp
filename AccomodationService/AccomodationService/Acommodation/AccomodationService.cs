namespace BloodBankLibrary.Core.Accomodations
{
    public class AccomodationService : IAccomodationService
    {
        private readonly IAccomodationRepository _accomodationRepository;
        public AccomodationService(IAccomodationRepository accomodationRepository)
        {
            _accomodationRepository = accomodationRepository;

        }


        public void Create(Accomodation accomodation)
        {
            _accomodationRepository.Create(accomodation);
        }


        public IEnumerable<Accomodation> GetAll()
        {
            return _accomodationRepository.GetAll();
        }

        public void Update(Accomodation accomodation)
        {
            _accomodationRepository.Update(accomodation);
        }

        public Accomodation GetById(int id)
        {
            return _accomodationRepository.GetById(id);
        }


        public void Delete(Accomodation accomodation)
        {
            _accomodationRepository.Delete(accomodation);
        }
    }
}
