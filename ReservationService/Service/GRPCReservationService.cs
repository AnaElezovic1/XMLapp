using BloodBankLibrary.Core.Booking;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using BloodBankLibrary.Core.Accomodations;
using BloodBankAPI;
using GetAllResponse = BloodBankAPI.GetAllResponse;
using GetAllRequest = BloodBankAPI.GetAllRequest;
using GetByIdResponse = BloodBankAPI.GetByIdResponse;
using GetByIdRequest = BloodBankAPI.GetByIdRequest;
using CreateResponse = BloodBankAPI.CreateResponse;
using CreateRequest = BloodBankAPI.CreateRequest;
using DeleteResponse = BloodBankAPI.DeleteResponse;
using DeleteRequest = BloodBankAPI.DeleteRequest;
using UpdateResponse = BloodBankAPI.UpdateResponse;
using UpdateRequest = BloodBankAPI.UpdateRequest;
using System.Threading.Channels;
using Channel = Grpc.Core.Channel;
using Docker.DotNet.Models;

public class GRPCReservationService : BloodBankAPI.ReservationService.ReservationServiceBase
{
    private readonly IReservationService _accomodationService;

    public GRPCReservationService(IReservationService accomodationService)
    {
        _accomodationService = accomodationService;
    }

    public override async Task<GetAllResponse> GetAll(GetAllRequest request, ServerCallContext context)
    {
        var reservations = _accomodationService.GetAll();
        var response = new GetAllResponse();
        response.Reservations.AddRange(reservations.Select(MapToReservation));
        return response;
    }

    public override async Task<GetByIdResponse> GetById(GetByIdRequest request, ServerCallContext context)
    {
        var reservation = _accomodationService.GetById(request.Id);
        if (reservation == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Reservation with id {request.Id} not found."));
        }
        return new GetByIdResponse { Reservation = MapToReservation(reservation) };
    }

    public override async Task<GetAllByGuestIdResponse> GetAllByGuestId(GetAllByGuestIdRequest request, ServerCallContext context)
    {
        var reservations = _accomodationService.GetAll().Where(e => e.GuestId == request.GuestId);
        var response = new GetAllByGuestIdResponse();
        response.Reservations.AddRange(reservations.Select(MapToReservation));
        return response;
    }

    public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
    {
        var reservationList = _accomodationService.GetAll();
        var reservation = MapToReservationBE(request.Reservation);
        var channelb = new Channel("localhost", 4111, ChannelCredentials.Insecure);
        var clientb = new BookingService.BookingServiceClient(channelb);
        BloodBankLibrary.Core.Booking.GetByIdRequest getByIdRequest = new BloodBankLibrary.Core.Booking.GetByIdRequest();
        getByIdRequest.Id = reservation.BookingId;
        var booking = clientb.GetById(getByIdRequest);
        if (booking.Booking.Autoaccept == true)
        {
            reservation.Accepted = true;
        }
        foreach (ReservationBE bE in reservationList)
        {
            if(bE.Accepted==true && bE.BookingId==reservation.BookingId)
                return new CreateResponse { Message = "already taken" };
        }
        _accomodationService.Create(reservation);
       
        return new CreateResponse { Message =  "created" };
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
        var channelb = new Channel("localhost", 4111, ChannelCredentials.Insecure);
        var clientb = new BookingService.BookingServiceClient(channelb);
        BloodBankLibrary.Core.Booking.GetByIdRequest getByIdRequest = new BloodBankLibrary.Core.Booking.GetByIdRequest();
        getByIdRequest.Id = _accomodationService.GetById(request.Id).BookingId;
        var booking = clientb.GetById(getByIdRequest);
        if (booking.Booking.Start.ToDateTime().AddDays(1) > DateTime.Now)
            return new DeleteResponse { Message = "unable to delete" };
        var reservation = _accomodationService.GetById(request.Id);
        if (reservation == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Reservation with id {request.Id} not found."));
        }
        _accomodationService.Delete(reservation);
        return new DeleteResponse { Message =  "deleted" };
    }

    public override async Task<DeleteByGuestIdResponse> DeleteByGuestId(DeleteByGuestIdRequest request, ServerCallContext context)
    {
        var reservations = _accomodationService.GetAll().Where(e => e.GuestId == request.GuestId).ToList();
        foreach (var reservation in reservations)
        {
            _accomodationService.Delete(reservation);
        }
        return new DeleteByGuestIdResponse { Message =  "done"  };
    }

    public override async Task<UpdateResponse> Update(UpdateRequest request, ServerCallContext context)
    {
        var updatedReservation = MapToReservationBE(request.Reservation);
        updatedReservation.Id = request.Id;
        try
        {
            _accomodationService.Update(updatedReservation);
             foreach (ReservationBE bE in _accomodationService.GetAll())
        {
            if (bE.BookingId == updatedReservation.BookingId)
                _accomodationService.Delete(bE);
        }
            return new UpdateResponse { Reservation = MapToReservation(updatedReservation) };
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, "Error updating reservation"));
        }
    }

    private Reservation MapToReservation(ReservationBE reservationBE)
    {
        var reservation = new Reservation
        {
             Id = reservationBE.Id,
            GuestId = reservationBE.GuestId,
            BookingId = reservationBE.BookingId,
            NoOfGuests = reservationBE.NoOfGuests,
            Accepted = reservationBE.Accepted
        };
        return reservation;
    }

    private ReservationBE MapToReservationBE(Reservation reservation)
    {
        var reservationBE = new ReservationBE
        {
            Id = reservation.Id,
            GuestId = reservation.GuestId,
            BookingId = reservation.BookingId,
            NoOfGuests = reservation.NoOfGuests,
            Accepted = reservation.Accepted
        };
        return reservationBE;
    }
    public override async Task<GetAllAccommodationResponse> GetAllGuestAccommodations(GetAllByGuestIdRequest request, ServerCallContext context)
    {
        var list = new GetAllAccommodationResponse();
        var channel = new Channel("localhost", 4211, ChannelCredentials.Insecure);
        var client = new AccomodationService.AccomodationServiceClient(channel);
        var accommodation = client.GetAll(new Empty());
        var channelb = new Channel("localhost", 4111, ChannelCredentials.Insecure);
        var clientb = new BookingService.BookingServiceClient(channelb);
        var bookings = clientb.GetAll(new BloodBankLibrary.Core.Booking.GetAllRequest());
        var reservations = _accomodationService.GetAll();
        foreach (var reservation in reservations)
            foreach (var booking in bookings.Bookings)
                foreach (var acc in accommodation.Accomodations)
                    if (acc.Id == booking.AccommodationId && booking.Id == reservation.BookingId)
                    {
                        var acchelper = new BloodBankAPI.Accomodation();
                        acchelper.Beds = acc.Beds;
                        acchelper.Description = acc.Description;
                        acchelper.HostId = acc.HostId;
                        acchelper.Id = acc.Id;
                        acchelper.Images = acc.Images;
                        acchelper.Location = acc.Location;
                        acchelper.Name = acc.Name;
                       
                        list.Accommodations.Add(acchelper);
                    }
        return list;
    }
}