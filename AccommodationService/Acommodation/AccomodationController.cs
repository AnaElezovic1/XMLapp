
using Microsoft.AspNetCore.Mvc;
using BloodBankLibrary.Core.Accomodations;

namespace BloodBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private readonly IAccomodationService _accomodationService;

        public AccomodationController(IAccomodationService accomodationService)
        {
            _accomodationService = accomodationService;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_accomodationService.GetAll());
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
        public ActionResult Create(AccomodationBE accomodation)

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

        [HttpPut("{id}")]
        public ActionResult Update(int id, AccomodationBE accomodation)
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

