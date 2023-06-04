namespace BloodBankLibrary.Core.Accomodations
{
    public class AccomodationService : IAccomodationService
    {
        private readonly IAccomodationRepository _accomodationRepository;
        public AccomodationService(IAccomodationRepository accomodationRepository)
        {
            _accomodationRepository = accomodationRepository;

        }


        public void Create(AccomodationBE accomodation)
        {
            _accomodationRepository.Create(accomodation);
        }


        public IEnumerable<AccomodationBE> GetAll()
        {
            return _accomodationRepository.GetAll();
        }

        public void Update(AccomodationBE accomodation)
        {
            _accomodationRepository.Update(accomodation);
        }

        public AccomodationBE GetById(int id)
        {
            return _accomodationRepository.GetById(id);
        }


        public void Delete(AccomodationBE accomodation)
        {
            _accomodationRepository.Delete(accomodation);
        }
    }
}
