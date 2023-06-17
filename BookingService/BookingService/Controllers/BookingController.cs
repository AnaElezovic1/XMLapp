
using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Accomodations;
using Grpc.Core;
using BloodBankLibrary.Core.Booking;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _reservationService;

        public BookingController(IBookingService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_reservationService.GetAll());
        }
        

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var reservation = _reservationService.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
        [HttpGet("host/{id}")]
        public ActionResult GetByHostId(int id)
        {
            var channel = new Channel("localhost", 4111, ChannelCredentials.Insecure);
            var client = new AccomodationService.AccomodationServiceClient(channel);
            var accommodation = client.GetAll(null);
            return Ok(accommodation.Accomodations.Where(e=>e.HostId==id));
        }

        [HttpPost]
        public ActionResult Create(BookingBE reservation)
        {
            _reservationService.Create(reservation);
            return Ok("created");
        }
        [HttpPost("delete/{id}")]
        public ActionResult Delete(int id)

        {
            _reservationService.Delete(_reservationService.GetById(id));
            return Ok("created");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, BookingBE reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _reservationService.Update(reservation);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(reservation);
        }

    }
}

