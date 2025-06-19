using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Servises
{
    public interface IPropertyService
    {
        Property? GetById(Guid id);

        IEnumerable<Property> GetAll();

        void UpdateParams(
           Guid propertyId,
           string name,
           string country,
           string city,
           double latitude,
           double longitude);

        void Delete(Guid id);

        Property? Create(
            string name,
            string country,
            string city,
            double latitude,
            double longitude);
    }
}
