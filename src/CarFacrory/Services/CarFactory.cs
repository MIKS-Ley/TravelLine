using CarFactory.Models;
using CarFactory.Models.Enums;

namespace CarFactory
{
    public static class CarFactory
    {
        public static Car CreateCar(string model, string brand, CarConfiguration config)
        {
            return new Car(model, brand, config);
        }

        public static Car CreateDefaultCar(string model, string brand)
        {
            return new Car(model, brand, new CarConfiguration
            {
                Engine = EngineType.V4,
                Transmission = TransmissionType.Automatic,
                Color = Color.Black,
                SteeringWheel = SteeringWheelPosition.Left,
                Body = BodyType.Sedan
            });
        }
    }
}