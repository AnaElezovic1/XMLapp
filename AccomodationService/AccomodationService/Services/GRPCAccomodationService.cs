using BloodBankLibrary.Core.Accomodations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace YourNamespace.Services
{
    public class GRPCAccomodationService:Accomodation.AccomodationBase
    {
        private readonly IAccomodationRepository accomodationRepository;

        public GRPCAccomodationService(IAccomodationRepository accomodationRepository)
        {
            this.accomodationRepository = accomodationRepository;
        }

        public override Task<List<Accomodation>> GetAll(Empty request, ServerCallContext context)
        {
            List<AccomodationBE> accomodationBE = accomodationRepository.GetAll().ToList();
            List<Accomodation> accomodations = new List<Accomodation>();
            foreach(AccomodationBE bE in accomodationBE)
            {
                Accomodation acc1 = new Accomodation();
                acc1.Id = bE.Id;
                acc1.Description = bE.Description;
                acc1.Beds = bE.Beds;
                acc1.Name = bE.Name;
                acc1.Images = bE.Images;
                accomodations.Add(acc1);
            }
            return Task.FromResult(accomodations);
        }

        public override Task<Accomodation> GetById(int request, ServerCallContext context)
        {

            AccomodationBE accomodationBE = accomodationRepository.GetById(request);
            Accomodation acc1 = new Accomodation();
            acc1.Id = accomodationBE.Id;
            acc1.Description = accomodationBE.Description;
            acc1.Beds = accomodationBE.Beds;
            acc1.Name = accomodationBE.Name;
            acc1.Images = accomodationBE.Images;
            return Task.FromResult(acc1);

        }

        public override Task<Empty> Create(Accomodation request, ServerCallContext context)
        {
            AccomodationBE accomodationBE = new AccomodationBE();
            accomodationBE.Id = request.Id;
            accomodationBE.Description = request.Description;
            accomodationBE.Beds = request.Beds;
            accomodationBE.Name = request.Name;
            accomodationBE.Images = request.Images;
            accomodationRepository.Create(accomodationBE);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(Accomodation request, ServerCallContext context)
        {
            AccomodationBE accomodationBE = new AccomodationBE();
            accomodationBE.Id = request.Id;
            accomodationBE.Description = request.Description;
            accomodationBE.Beds = request.Beds;
            accomodationBE.Name = request.Name;
            accomodationBE.Images = request.Images;
            accomodationRepository.Delete(accomodationBE);
            return Task.FromResult(new Empty());

        }

        public override Task<Empty> Update(Accomodation request, ServerCallContext context)
        {
            AccomodationBE accomodationBE = new AccomodationBE();
            accomodationBE.Id = request.Id;
            accomodationBE.Description = request.Description;
            accomodationBE.Beds = request.Beds;
            accomodationBE.Name = request.Name;
            accomodationBE.Images = request.Images;
            accomodationRepository.Update(accomodationBE);
            return Task.FromResult(new Empty());
        }
    }
}