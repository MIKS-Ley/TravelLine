using Domain.Interfaces.Services;
using Domain.Interfaces.Servises;
using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Mappers;
using WebApplication1.Request;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly IRoomTypeService _roomTypeService;
        public PropertiesController(IPropertyService propertyService, IRoomTypeService roomTypeService)
        {
            _propertyService = propertyService;
            _roomTypeService = roomTypeService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Property>> GetALL()
        {
            IEnumerable<Property> properties = _propertyService.GetAll();

            return Ok( properties.Select(p => p.ToDTO() ) );
        }

        [HttpGet("{id}")]
        public ActionResult<Property> Get(Guid id)
        {
            Property? property = _propertyService.GetById(id);
            return property == null ? NotFound() : Ok(property.ToDTO());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreatePropertyRequest createPropertyRequest)
        {
            Property? property = _propertyService.Create(
                createPropertyRequest.Name,
                createPropertyRequest.Country,
                createPropertyRequest.City,
                createPropertyRequest.Latitude,
                createPropertyRequest.Longitude);
            if (property == null)
            {
                return BadRequest(property);
            }

            return Ok(property.ToDTO());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParams(Guid id, [FromBody] CreatePropertyRequest createPropertyRequest)
        {
            Property? property = _propertyService.GetById(id);
            if (property == null)
            {
                return NotFound();
            }

            _propertyService.UpdateParams(
                id,
                createPropertyRequest.Name,
                createPropertyRequest.Country,
                createPropertyRequest.City,
                createPropertyRequest.Latitude,
                createPropertyRequest.Longitude);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            Property? property = _propertyService.GetById(id);
            if (property == null) return NotFound();

            _propertyService.Delete(id);

            return Ok();
        }

        [HttpGet("roomTypes/{id}")]
        public ActionResult<IEnumerable<RoomType>> GetById(Guid id)
        {
            RoomType? roomType = _roomTypeService.GetById(id);

            return roomType == null ? NotFound() : Ok(roomType.ToDto());
        }

        [HttpGet("{propertyId}/roomTypes")]
        public ActionResult<RoomType> GetAllRoomTypesPropertyById(Guid propertyId)
        {
            IEnumerable<RoomType> roomType = _roomTypeService.GetAllRoomTypePropertyById(propertyId);

            return roomType == null ? NotFound() : Ok(roomType.Select(r => r.ToDto()).ToList());
        }

        [HttpPost("{propertyId}/roomTypes")]
        public IActionResult Create(
            Guid propertyId,
            [FromBody] CreateRoomTypeRequest createRoomTypeRequest)
        {
            RoomType? roomType = _roomTypeService.Create(
                    propertyId,
                    createRoomTypeRequest.Name,
                    createRoomTypeRequest.DailyPrice,
                    createRoomTypeRequest.Currency,
                    createRoomTypeRequest.MinPersonCount,
                    createRoomTypeRequest.MaxPersonCount,
                    createRoomTypeRequest.Services,
                    createRoomTypeRequest.Amenities);

            if (roomType == null)
            {
                return BadRequest(roomType);
            }

            return Ok(roomType.ToDto());
        }

        [HttpPut("roomTypes/{id}")]
        public IActionResult UpdateParams(Guid id, [FromBody] UpdateRoomTypeRequest updateRoomTypeRequest)
        {
            RoomType? roomType = _roomTypeService.GetById(id);
            if (_roomTypeService.GetById(id) != null)
            {
                _roomTypeService.UpdateParams(
                    id,
                    updateRoomTypeRequest.PropertyId,
                    updateRoomTypeRequest.Name,
                    updateRoomTypeRequest.DailyPrice,
                    updateRoomTypeRequest.Currency,
                    updateRoomTypeRequest.MinPersonCount,
                    updateRoomTypeRequest.MaxPersonCount,
                    updateRoomTypeRequest.Services,
                    updateRoomTypeRequest.Amenities);

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("roomTypes/{id}")]
        public IActionResult DeleteRoomtype(Guid id)
        {
            if (_roomTypeService.GetById(id) is null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
