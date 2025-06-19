using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly List<Property> _properties = new()
        {
            new Property()
            {
                Id = Guid.NewGuid(),
                Name = "fgh",
                Country = "sddfghh",
                City = "",
                Latitude = 2222,
                Longitude = 7777
            }
        };

        public void Create(Property? property)
        {
            if (property != null)
            {
                _properties.Add(property);
            }
        }

        public void Delete(Guid id)
        {
            var property = GetById(id);
            if (property != null)
            {
                _properties.Remove(property);
            }
        }

        public IEnumerable<Property> GetAll() => _properties;

        public Property? GetById(Guid id) => _properties.FirstOrDefault(p => p.Id == id);

        public void UpdateParams(Guid id, Property property)
        {
            var existingProperty = GetById(id);
            if (existingProperty != null)
            {
                existingProperty.Name = property.Name;
                existingProperty.Country = property.Country;
                existingProperty.City = property.City;
                existingProperty.Latitude = property.Latitude;
                existingProperty.Longitude = property.Longitude;
            }
        }
    }
}