using Domain.Interfaces.Repositories;
using Domain.Interfaces.Servises;
using Domain.Models;

namespace Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }
        public Property? GetById(Guid id)
        {
            return _propertyRepository.GetById(id);
        }

        public IEnumerable<Property> GetAll()
        {
            return _propertyRepository.GetAll();
        }

        public void UpdateParams(
            Guid propertyId,
            string name,
            string country,
            string city,
            double latitude,
            double longitude)
        {
            Property? property = _propertyRepository.GetById(propertyId);
            if (property != null)
            {
                property.Id = propertyId;
                property.Name = name;
                property.Country = country;
                property.City = city;
                property.Latitude = latitude;
                property.Longitude = longitude;
                _propertyRepository.UpdateParams(propertyId, property);
            }
        }

        public void Delete(Guid id)
        {
            _propertyRepository.Delete(id);
        }

        public Property? Create(
            string name,
            string country,
            string city,
            double latitude,
            double longitude)
        {
            Property? property = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Country = country,
                City = city,
                Latitude = latitude,
                Longitude = longitude
            };
            _propertyRepository.Create(property);

            return property;
        }
    }
}
