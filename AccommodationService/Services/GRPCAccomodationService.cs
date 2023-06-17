using BloodBankLibrary.Core.Accomodations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

namespace YourNamespace.Services
{
    public class GRPCAccomodationService:AccomodationService.AccomodationServiceBase
    {
        private readonly IAccomodationRepository accomodationRepository;

        public GRPCAccomodationService(IAccomodationRepository accomodationRepository)
        {
            this.accomodationRepository = accomodationRepository;
        }

        public GRPCAccomodationService()
        {
        }

        public override Task<AccomodationList> GetAll(Empty request, ServerCallContext context)
        {
            AccomodationList list = new AccomodationList();
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
                acc1.HostId = bE.HostId;
                list.Accomodations.Add(acc1);
            }
            return Task.FromResult(list);
        }

        public override Task<Accomodation> GetById(AccomodationId request, ServerCallContext context)
        {

            AccomodationBE accomodationBE = accomodationRepository.GetById(request.Id);
            Accomodation acc1 = new Accomodation();
            acc1.Id = accomodationBE.Id;
            acc1.Description = accomodationBE.Description;
            acc1.Beds = accomodationBE.Beds;
            acc1.Name = accomodationBE.Name;
            acc1.Images = accomodationBE.Images;
            acc1.HostId = accomodationBE.HostId;
            return Task.FromResult(acc1);

        }

        public Task<Empty> Create(Accomodation request, ServerCallContext context)
        {
            AccomodationBE accomodationBE = new AccomodationBE();
            accomodationBE.Id = request.Id;
            accomodationBE.Description = request.Description;
            accomodationBE.Beds = request.Beds;
            accomodationBE.Name = request.Name;
            accomodationBE.Images = request.Images;
            accomodationBE.HostId = request.HostId;
            accomodationRepository.Create(accomodationBE);
            return Task.FromResult(new Empty());
        }

        public  Task<Empty> Delete(Accomodation request, ServerCallContext context)
        {
            AccomodationBE accomodationBE = new AccomodationBE();
            accomodationBE.Id = request.Id;
            accomodationBE.Description = request.Description;
            accomodationBE.Beds = request.Beds;
            accomodationBE.Name = request.Name;
            accomodationBE.Images = request.Images;
            accomodationBE.HostId = request.HostId;
            accomodationRepository.Delete(accomodationBE);
            return Task.FromResult(new Empty());

        }
        public Task<Empty> DeleteAllByHostId(HostId request, ServerCallContext context)
        {

            foreach (AccomodationBE bE in accomodationRepository.GetAll())
            {
                if(bE.HostId==request.Id)
                accomodationRepository.Delete(bE);

            }
            return Task.FromResult(new Empty());

        }

        public  Task<Empty> Update(Accomodation request, ServerCallContext context)
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