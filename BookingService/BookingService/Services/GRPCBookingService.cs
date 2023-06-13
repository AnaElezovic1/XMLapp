using BloodBankLibrary.Core.Booking;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using BloodBankLibrary.Core.Accomodations;

namespace YourNamespace.Services
{
    public class GRPCBookingService:BookingService.BookingServiceBase
    {
        private readonly IBookingRepository accomodationRepository;

        public GRPCBookingService(IBookingRepository accomodationRepository)
        {
            this.accomodationRepository = accomodationRepository;
        }

        public GRPCBookingService()
        {
        }

        public override Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            GetAllResponse list = new GetAllResponse();
            List<BookingBE> accomodationBE = accomodationRepository.GetAll().ToList();
            List<Booking> accomodations = new List<Booking>();
            foreach(BookingBE bE in accomodationBE)
            {
                Booking acc1 = new Booking();
                acc1.Id = bE.Id;
                acc1.AccommodationId = bE.AccommodationId;
                acc1.Autoaccept = bE.Autoaccept;
                acc1.End = Timestamp.FromDateTime(bE.End);
                acc1.Start = Timestamp.FromDateTime(bE.Start);
                acc1.Perperson= bE.Perperson;
                acc1.Price= bE.Price;
               
                list.Bookings.Add(acc1);
            }
            return Task.FromResult(list);
        }

        public override Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
        {

            BookingBE bE = accomodationRepository.GetById(request.Id);
            Booking acc1 = new Booking();
            acc1.Id = bE.Id;
            acc1.AccommodationId = bE.AccommodationId;
            acc1.Autoaccept = bE.Autoaccept;
            acc1.End = Timestamp.FromDateTime(bE.End);
            acc1.Start = Timestamp.FromDateTime(bE.Start);
            acc1.Perperson = bE.Perperson;
            acc1.Price = bE.Price;
            GetByIdResponse getByIdResponse = new GetByIdResponse();
            getByIdResponse.Booking = acc1;
            return Task.FromResult(getByIdResponse);

        }

        public override Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            BookingBE accomodationBE = new BookingBE();
            accomodationBE.Id = request.Booking.Id;
            accomodationBE.AccommodationId = request.Booking.AccommodationId;
            accomodationBE.Autoaccept = request.Booking.Autoaccept;
            accomodationBE.End = request.Booking.End.ToDateTime();
            accomodationBE.Start = request.Booking.Start.ToDateTime();
            accomodationBE.Perperson = request.Booking.Perperson;
            accomodationBE.Price = request.Booking.Price;
            accomodationRepository.Create(accomodationBE);
            return Task.FromResult(new CreateResponse());
        }

        public override Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
        {
            
            BookingBE accomodationBE = new BookingBE();
            accomodationBE.Id = request.Booking.Id;
            accomodationBE.AccommodationId = request.Booking.AccommodationId;
            accomodationBE.Autoaccept = request.Booking.Autoaccept;
            accomodationBE.End = request.Booking.End.ToDateTime();
            accomodationBE.Start = request.Booking.Start.ToDateTime();
            accomodationBE.Perperson = request.Booking.Perperson;
            accomodationBE.Price = request.Booking.Price;
            accomodationRepository.Delete(accomodationBE);
            return Task.FromResult(new DeleteResponse());

        }
        public override Task<GetAllResponse> DeleteAllByHostId(GetByIdRequest request, ServerCallContext context)
        {
            var channel = new Channel("localhost", 4111, ChannelCredentials.Insecure);
            var client = new AccomodationService.AccomodationServiceClient(channel);
            var accommodation = client.GetAll(null);
            foreach (Accomodation ac in accommodation.Accomodations)
            {
                if (ac.HostId == request.Id)
                    foreach (BookingBE bkng in accomodationRepository.GetAll().Where(e => e.AccommodationId == ac.Id)){
                        accomodationRepository.Delete(bkng);
                    }

            }
            return Task.FromResult(new GetAllResponse());

        }
        public override Task<GetAllResponse> GetByHostId(GetByIdRequest request, ServerCallContext context)
        {
            GetAllResponse list = new GetAllResponse();
            var channel = new Channel("localhost", 4211, ChannelCredentials.Insecure);
            var client = new AccomodationService.AccomodationServiceClient(channel);

        

            var accommodation = client.GetAll(new Empty());
            foreach (Accomodation ac in accommodation.Accomodations)
            {
                if (ac.HostId == request.Id)
                    foreach (BookingBE bkng in accomodationRepository.GetAll().Where(e => e.AccommodationId == ac.Id))
                    {
                        Booking acc1 = new Booking();
                        acc1.Id = bkng.Id;
                        acc1.AccommodationId = bkng.AccommodationId;
                        acc1.Autoaccept = bkng.Autoaccept;
                        acc1.End = Timestamp.FromDateTime(bkng.End);
                        acc1.Start = Timestamp.FromDateTime(bkng.Start);
                        acc1.Perperson = bkng.Perperson;
                        acc1.Price = bkng.Price;

                        list.Bookings.Add(acc1);
                    }

            }
            return Task.FromResult(list);

        }
        public override Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
        {
            BookingBE accomodationBE = new BookingBE();
            accomodationBE.Id = request.Booking.Id;
            accomodationBE.AccommodationId = request.Booking.AccommodationId;
            accomodationBE.Autoaccept = request.Booking.Autoaccept;
            accomodationBE.End = request.Booking.End.ToDateTime();
            accomodationBE.Start = request.Booking.Start.ToDateTime();
            accomodationBE.Perperson = request.Booking.Perperson;
            accomodationBE.Price = request.Booking.Price;
            accomodationRepository.Update(accomodationBE);
            return Task.FromResult(new UpdateResponse());
        }
    }
}