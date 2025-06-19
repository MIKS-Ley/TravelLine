using Domain.Models;
using WebApplication1.DTO;

namespace WebApplication1.Mappers
{
    public static class PropertyMapper
    {
        public static PropertyDTO ToDTO(this Property property)
        {
            return new PropertyDTO
            {
                Id = property.Id,
                Name = property.Name,
                Country = property.Country,
                City = property.City,
                Latitude = property.Latitude,
                Longitude = property.Longitude
            };
        }
            
    }
}
