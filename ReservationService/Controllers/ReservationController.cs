
using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _accomodationService;

        public ReservationController(IReservationService accomodationService)
        {
            _accomodationService = accomodationService;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_accomodationService.GetAll());
        }

        [HttpGet("guest/{id}")]
        public ActionResult GetAllByGuestId(int id)
        {
            return Ok(_accomodationService.GetAll().Where(e=>e.GuestId==id));
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var accomodation = _accomodationService.GetById(id);
            if (accomodation == null)
            {
                return NotFound();
            }

            return Ok(accomodation);
        }

        [HttpPost]
        public ActionResult Create(BloodBankLibrary.Core.Accomodations.ReservationBE accomodation)

        {
            _accomodationService.Create(accomodation);
            return Ok("created");
        }
        [HttpPost("delete/{id}")]
        public ActionResult Delete(int id)

        {
            _accomodationService.Delete(_accomodationService.GetById(id));
            return Ok("created");
        }
        [HttpPost("delete-guest/{id}")]
        public ActionResult DeleteByGuestId(int id)

        {
            foreach(var accomodation in _accomodationService.GetAll().Where(e=>e.GuestId==id))
            _accomodationService.Delete(_accomodationService.GetById(id));
            return Ok("done");
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, BloodBankLibrary.Core.Accomodations.ReservationBE accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _accomodationService.Update(accomodation);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(accomodation);
        }

    }
}

