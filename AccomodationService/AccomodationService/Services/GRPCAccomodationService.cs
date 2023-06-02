using BloodBankLibrary.Core.Accomodations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace YourNamespace.Services
{
    public class GRPCAccomodationService : Accomodation.AccomodationBase
    {
        private readonly IAccomodationRepository accomodationRepository;

        public GRPCAccomodationService(IAccomodationRepository accomodationRepository)
        {
            this.accomodationRepository = accomodationRepository;
        }

        public override Task<List<Accomodation>> GetAll(Empty request, ServerCallContext context)
        {
            return Task.FromResult(accomodationRepository.GetAll());
        }

        public override Task<Accomodation> GetById(int request, ServerCallContext context)
        {

            return Task.FromResult(accomodationRepository.GetById);
        }

        public override Task<Empty> Create(Accomodation request, ServerCallContext context)
        {
            accomodationRepository.Create(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(int request, ServerCallContext context)
        {

            accomodationRepository.Remove(request);
            return Task.FromResult(new Empty());

        }

        public override Task<Empty> Update(Accomodation request, ServerCallContext context)
        {
            accomodationRepository.Update(request);
            return Task.FromResult(new Empty());
        }
    }
}