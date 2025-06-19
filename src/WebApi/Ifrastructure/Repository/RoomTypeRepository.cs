using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Infrastructure.Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly List<RoomType> _roomTypes = new();

        public void Create(RoomType? roomType)
        {
            if (roomType != null)
            {
                _roomTypes.Add(roomType);
            }
        }

        public void Delete(Guid roomTypeId)
        {
            var roomType = GetById(roomTypeId);
            if (roomType != null)
            {
                _roomTypes.Remove(roomType);
            }
        }

        public IEnumerable<RoomType> GetAllRoomTypePropertyById(Guid propertyId) =>
            _roomTypes.Where(p => p.PropertyId == propertyId);

        public RoomType? GetById(Guid id) => _roomTypes.FirstOrDefault(u => u.Id == id);

        public void Update(Guid id, RoomType? roomType)
        {
            if (roomType != null)
            {
                _roomTypes.RemoveAll(u => u.Id == id);
                _roomTypes.Add(roomType);
            }
        }
    }
}