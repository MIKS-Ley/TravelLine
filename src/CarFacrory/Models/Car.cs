namespace CarFactory.Models
{
    public class Car
    {
        public DateTime ProductionDate { get; set; }
        public string VIN { get; set; }
        public string Model { get; }
        public string Brand { get; }
        public CarConfiguration Configuration { get; }

        public Car( string model, string brand, CarConfiguration configuration )
        {
            Model = model;
            Brand = brand;
            Configuration = configuration;
        }

        public override string ToString()
        {
            return $"{Brand} {Model}\n" +
                   $"  Конфигурация:\n" +
                   $"  Двигатель:  {Configuration.Engine}\n" +
                   $"  Трансмиссия: {Configuration.Transmission} ({Configuration.Gears} gears)\n" +
                   $"  Body: {Configuration.Body}\n" +
                   $"  Цвет: {Configuration.Color}\n" +
                   $"  Рулевое управление: {Configuration.SteeringWheel}\n" +
                   $"  Максимальная скорость:  {Configuration.MaxSpeed} km/h";
        }
    }
}