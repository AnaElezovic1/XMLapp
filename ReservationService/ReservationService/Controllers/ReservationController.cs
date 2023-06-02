
using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
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

        [HttpPost]
        public ActionResult Create(Reservation reservation)

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
        public ActionResult Update(int id, Reservation reservation)
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

