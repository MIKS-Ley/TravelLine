using Domain.Models;

namespace Domain.Interfaces.Services
{
    public interface IRoomTypeService
    {
        RoomType? GetById(Guid id);

        IEnumerable<RoomType> GetAllRoomTypePropertyById(Guid propertyId);

        void UpdateParams(
            Guid roomTypeId,
            Guid propertyID,
            string name,
            double dailyPrice,
            string currency,
            int minPersonCount,
            int maxPersonCount,
            List<string> services,
            List<string> amenities);

        void Delete(Guid id);

        RoomType? Create(
            Guid propertyID,
            string name,
            double dailyPrice,
            string currency,
            int minPersonCount,
            int maxPersonCount,
            List<string> services,
            List<string> amenities);
    }
}
