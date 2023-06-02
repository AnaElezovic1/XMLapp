using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BloodBankLibrary.Core.Reservations;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace YourNamespace.Services
{
    public class GRPCReservationService : Reservation.ReservationBase
    {
        private readonly IReservationRepository reservationRepository;

        public GRPCReservationService(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
        }

        public override Task<List<Reservation>> GetAll(Empty request, ServerCallContext context)
        {
            return Task.FromResult(reservationRepository.GetAll());
        }

        public override Task<Reservation> GetById(ReservationId request, ServerCallContext context)
        {

            return Task.FromResult(reservationRepository.GetById);
        }

        public override Task<Empty> Create(Reservation request, ServerCallContext context)
        {
            reservationRepository.Create(request);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(ReservationId request, ServerCallContext context)
        {

            reservationRepository.Remove(reservation);
            return Task.FromResult(new Empty());

        }

        public override Task<Empty> Update(Reservation request, ServerCallContext context)
        {
            reservationRepository.Update(request);
            return Task.FromResult(new Empty());
        }
    }
}