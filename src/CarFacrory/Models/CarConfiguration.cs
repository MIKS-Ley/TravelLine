using System;
using CarFactory.Models.Enums;  

namespace CarFactory.Models
{
    public class CarConfiguration
    {
        public EngineType Engine { get; set; }
        public TransmissionType Transmission { get; set; }
        public Color Color { get; set; }
        public SteeringWheelPosition SteeringWheel { get; set; }
        public BodyType Body { get; set; }

        public int MaxSpeed => CalculateMaxSpeed();
        public int Gears => CalculateGears();

        private int CalculateMaxSpeed()
        {
            int baseSpeed = 180;

            // Влияние двигателя
            baseSpeed += Engine switch
            {
                EngineType.V4 => 20,
                EngineType.V6 => 30,
                EngineType.V8 => 60,
                EngineType.Lada124 => 10,
                EngineType.Lada126 => 15,
                EngineType.ToyotaJZ => 60,
                EngineType.TeslaPlaidTriMotor => 70,
                _ => 0
            };

            // Влияние коробки передач
            baseSpeed += Transmission switch
            {
                TransmissionType.Mechanical => 10,
                TransmissionType.Automatic => 0,
                TransmissionType.Robot => -5,
                TransmissionType.CVT => -10,
                _ => 0
            };

            // Влияние типа кузова
            baseSpeed += Body switch
            {
                BodyType.Sedan => 0,
                BodyType.Hatchback => 10,
                BodyType.SUV => -20,
                BodyType.Coupe => 20,
                BodyType.Convertible => 15,
                _ => 0
            };

            return baseSpeed;
        }

        private int CalculateGears()
        {
            return Transmission switch
            {
                TransmissionType.Mechanical => 6,
                TransmissionType.Automatic => 8,
                TransmissionType.Robot => 3,
                TransmissionType.CVT => 1,
                _ => 0
            };
        }
    }
}