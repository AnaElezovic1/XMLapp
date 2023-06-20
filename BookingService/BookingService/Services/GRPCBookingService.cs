using BloodBankLibrary.Core.Booking;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using BloodBankLibrary.Core.Accomodations;
using BloodBankAPI;
using UpdateResponse = BloodBankLibrary.Core.Booking.UpdateResponse;
using UpdateRequest = BloodBankLibrary.Core.Booking.UpdateRequest;
using GetAllResponse = BloodBankLibrary.Core.Booking.GetAllResponse;
using GetByIdRequest = BloodBankLibrary.Core.Booking.GetByIdRequest;
using DeleteResponse = BloodBankLibrary.Core.Booking.DeleteResponse;
using DeleteRequest = BloodBankLibrary.Core.Booking.DeleteRequest;
using CreateResponse = BloodBankLibrary.Core.Booking.CreateResponse;
using CreateRequest = BloodBankLibrary.Core.Booking.CreateRequest;
using GetByIdResponse = BloodBankLibrary.Core.Booking.GetByIdResponse;
using GetAllRequest = BloodBankLibrary.Core.Booking.GetAllRequest;

namespace YourNamespace.Services
{
    public class GRPCBookingService : BloodBankLibrary.Core.Booking.BookingService.BookingServiceBase
    {
        private readonly IBookingRepository bookingRepository;

        public GRPCBookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }

        public GRPCBookingService()
        {
        }

        public override Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
        {
            GetAllResponse list = new GetAllResponse();
            List<BookingBE> bookingBEs = bookingRepository.GetAll().ToList();
            List<Booking> bookings = new List<Booking>();
            foreach (BookingBE bE in bookingBEs)
            {
                Booking acc1 = new Booking();
                acc1.Id = bE.Id;
                acc1.AccommodationId = bE.AccommodationId;
                acc1.Autoaccept = bE.Autoaccept;
                acc1.End = Timestamp.FromDateTime(bE.End);
                acc1.Start = Timestamp.FromDateTime(bE.Start);
                acc1.Perperson = bE.Perperson;
                acc1.Price = bE.Price;

                list.Bookings.Add(acc1);
            }
            return Task.FromResult(list);
        }

        public override Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
        {

            BookingBE bE = bookingRepository.GetById(request.Id);
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
            bookingRepository.Create(accomodationBE);
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
            bookingRepository.Delete(accomodationBE);
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
                    foreach (BookingBE bkng in bookingRepository.GetAll().Where(e => e.AccommodationId == ac.Id))
                    {
                        bookingRepository.Delete(bkng);
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
                    foreach (BookingBE bkng in bookingRepository.GetAll().Where(e => e.AccommodationId == ac.Id))
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
            var channel = new Channel("localhost", 4311, ChannelCredentials.Insecure);
            var client = new ReservationService.ReservationServiceClient(channel);
            var reservations = client.GetAll(new BloodBankAPI.GetAllRequest());
            foreach (var reservation in reservations.Reservations)
            {
                if (reservation.BookingId == request.Booking.Id)
                    return Task.FromResult(new UpdateResponse());
            }
            bookingRepository.Update(accomodationBE);
            return Task.FromResult(new UpdateResponse());
        }
    }
}