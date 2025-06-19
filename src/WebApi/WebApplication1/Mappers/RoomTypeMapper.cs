using Domain.Models;
using WebApplication1.DTO;
namespace WebApplication1.Mappers
{
    public static class RoomTypeMapper
    {
        public static RoomTypeDTO ToDto(this RoomType roomType)
        {
            return new RoomTypeDTO()
            {
                Id = roomType.Id,
                PropertyId = roomType.PropertyId,
                Name = roomType.Name,
                DailyPrice = (double)roomType.DailyPrice,
                Currency = roomType.Currency,
                MinPersonCount = roomType.MinPersonCount,
                MaxPersonCount = roomType.MaxPersonCount,
                Services = roomType.Services,
                Amenities = roomType.Amenities
            };
        }
    }
}
