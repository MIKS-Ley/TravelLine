using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IPropertyRepository 
    {
        void Create(Property? property);

        IEnumerable<Property> GetAll();

        Property? GetById(Guid id);

        void UpdateParams(Guid id, Property property);

        void Delete(Guid id); 
    }
}
