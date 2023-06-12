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
        var reservation = MapToReservationBE(request.Reservation);
        _accomodationService.Create(reservation);
        return new CreateResponse { Message =  "created" };
    }

    public override async Task<DeleteResponse> Delete(DeleteRequest request, ServerCallContext context)
    {
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
}