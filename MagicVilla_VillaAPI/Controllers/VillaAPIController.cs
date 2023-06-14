
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/villa-api")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {

        //private readonly ILogger<VillaAPIController> _logger;
        private readonly ILogging _logger;
        private readonly ApplicationDatabaseContext _applicationDatabaseContext;

        public VillaAPIController(ILogging logger, ApplicationDatabaseContext applicationDatabaseContext)
        {
            _logger = logger;
            _applicationDatabaseContext = applicationDatabaseContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            _logger.Log("getting all villas");
            return Ok(await _applicationDatabaseContext.villas.ToListAsync());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0 || id > _applicationDatabaseContext.villas.ToList().Count)
            {
                _logger.Log("error getting villa for id: " + id, Logging.LogLevel.ERROR);
                return BadRequest();
            }
            var villa = await _applicationDatabaseContext.villas.FirstOrDefaultAsync(u => u.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            var villaDto = new VillaDTO()
            {
                Name = villa.Name,
                Details = villa.Details,
                Amenity = villa.Amenity,
                ImageUrl = villa.ImageUrl,
                Occupecy = villa.Occupecy,
                Rate = villa.Rate,
                SqureFeet = villa.SqureFeet,
            };
            return villaDto;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> createVilla([FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null)
            {
                return BadRequest();
            }
            if (villaDTO.Id != 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var data = await _applicationDatabaseContext.villas.FirstOrDefaultAsync();
            if (data.Name == villaDTO.Name)
            {
                ModelState.AddModelError("custom error", "villa already exist!");
                return BadRequest();
            }

            var villa = new Villa()
            {
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                ImageUrl = villaDTO.ImageUrl,
                Occupecy = villaDTO.Occupecy,
                Rate = villaDTO.Rate,
                SqureFeet = villaDTO.SqureFeet,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            _applicationDatabaseContext.villas.Add(villa);
            _applicationDatabaseContext.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "delete")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
                return BadRequest();


            var villa = await _applicationDatabaseContext.villas.FirstOrDefaultAsync(villa => villa.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            _applicationDatabaseContext.villas.ToList().Remove(villa);
            _applicationDatabaseContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "update")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (id == 0 || villaDTO == null || id != villaDTO.Id)
                return BadRequest();


            var villa = new Villa()
            {
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Amenity = villaDTO.Amenity,
                ImageUrl = villaDTO.ImageUrl,
                Occupecy = villaDTO.Occupecy,
                Rate = villaDTO.Rate,
                SqureFeet = villaDTO.SqureFeet,
                UpdatedAt = DateTime.Now

            };
            _applicationDatabaseContext.villas.Update(villa);
            _applicationDatabaseContext.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "updatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult updatePartialVilla(int id, [FromBody] JsonPatchDocument<VillaDTO> patchVilla)
        {
            if (patchVilla == null || id == 0)
            {
                return BadRequest();
            }
            var villa = _applicationDatabaseContext.villas.ToList().FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            var villaDto = new VillaDTO()
            {
                Name = villa.Name,
                Details = villa.Details,
                Amenity = villa.Amenity,
                ImageUrl = villa.ImageUrl,
                Occupecy = villa.Occupecy,
                Rate = villa.Rate,
                SqureFeet = villa.SqureFeet,
            };

            villa = new Villa()
            {
                Name = villaDto.Name,
                Details = villaDto.Details,
                Amenity = villaDto.Amenity,
                ImageUrl = villaDto.ImageUrl,
                Occupecy = villaDto.Occupecy,
                Rate = villaDto.Rate,
                SqureFeet = villaDto.SqureFeet,
                UpdatedAt = DateTime.Now

            };
            patchVilla.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

