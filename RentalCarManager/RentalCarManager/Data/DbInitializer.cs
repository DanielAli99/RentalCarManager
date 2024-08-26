using RentalCarManager.Models;

namespace RentalCarManager.Data
{
    internal static class DbInitializer
    {
        public static void Initialize(VehicleContext context)
        {
            context.Database.EnsureCreated();
            if (context.Vehicles.Any()) return;
            Car[] cars =
            [
            new() { Make = "Toyota", Model = "Corolla", LicensePlate = "XYZ123", Color = "Blue", Year = 2018, Acceleration = 10.0, DailyRentalRate = 29.99, AC = new AC{ MinTemperature=0, MaxTemperature = 10} },
            new() { Make = "Toyota", Model = "Camry", LicensePlate = "CAM456", Color = "Blue", Year = 2019, Acceleration = 9.8, DailyRentalRate = 35.50 },
            new() { Make = "Toyota", Model = "Prius", LicensePlate = "PRI789", Color = "White", Year = 2020, Acceleration = 8.9, DailyRentalRate = 40.00, Radio = new Radio{ MinFrequency = 10, MaxFrequency = 20} },

            new() { Make = "Honda", Model = "Civic", LicensePlate = "ABC123", Color = "Red", Year = 2019, Acceleration = 9.5, DailyRentalRate = 30.99, Radio = new Radio{ MinFrequency = 3, MaxFrequency = 5} },
            new() { Make = "Honda", Model = "Accord", LicensePlate = "ACC654", Color = "Red", Year = 2020, Acceleration = 9.0, DailyRentalRate = 34.99, AC = new AC{ MinTemperature=5, MaxTemperature = 15} },
            new() { Make = "Honda", Model = "Fit", LicensePlate = "FIT987", Color = "Blue", Year = 2017, Acceleration = 8.5, DailyRentalRate = 25.50 },

            new() { Make = "Ford", Model = "Focus", LicensePlate = "FOC321", Color = "Grey", Year = 2017, Acceleration = 9.0, DailyRentalRate = 28.50, AC = new AC{ MinTemperature=7, MaxTemperature = 12} },
            new() { Make = "Ford", Model = "Mustang", LicensePlate = "MUS654", Color = "Black", Year = 2021, Acceleration = 4.5, DailyRentalRate = 60.00, Radio = new Radio{ MinFrequency = 20, MaxFrequency = 30} },
            new() { Make = "Ford", Model = "Fusion", LicensePlate = "FUS789", Color = "White", Year = 2018, Acceleration = 8.0, DailyRentalRate = 30.00 },

            new() { Make = "Chevrolet", Model = "Malibu", LicensePlate = "CHV456", Color = "White", Year = 2018, Acceleration = 8.5, DailyRentalRate = 27.99, Radio = new Radio{ MinFrequency = 10, MaxFrequency = 17} },
            new() { Make = "Chevrolet", Model = "Impala", LicensePlate = "IMP321", Color = "Grey", Year = 2019, Acceleration = 8.3, DailyRentalRate = 29.50, AC = new AC{ MinTemperature=0, MaxTemperature = 20} },
            new() { Make = "Chevrolet", Model = "Spark", LicensePlate = "SPK654", Color = "Red", Year = 2017, Acceleration = 9.0, DailyRentalRate = 23.99 },

            new() { Make = "Hyundai", Model = "Elantra", LicensePlate = "HYD789", Color = "Silver", Year = 2020, Acceleration = 10.5, DailyRentalRate = 32.00, AC = new AC{ MinTemperature=3, MaxTemperature = 20} },
            new() { Make = "Hyundai", Model = "Sonata", LicensePlate = "SON654", Color = "Silver", Year = 2019, Acceleration = 9.7, DailyRentalRate = 31.50, Radio = new Radio{ MinFrequency = 7, MaxFrequency = 14} },
            new() { Make = "Hyundai", Model = "Tucson", LicensePlate = "TUC987", Color = "Black", Year = 2018, Acceleration = 8.9, DailyRentalRate = 29.99 },

            new() { Make = "Tesla", Model = "Model 3", LicensePlate = "TES111", Color = "Black", Year = 2021, Acceleration = 3.2, DailyRentalRate = 130.00, Radio = new Radio{ MinFrequency = 31, MaxFrequency = 157} },
            new() { Make = "Tesla", Model = "Model S", LicensePlate = "TES222", Color = "Red", Year = 2020, Acceleration = 2.9, DailyRentalRate = 140.00 },
            new() { Make = "Tesla", Model = "Model X", LicensePlate = "TES333", Color = "White", Year = 2021, Acceleration = 3.5, DailyRentalRate = 150.00, AC = new AC{ MinTemperature=10, MaxTemperature = 20} },

            new() { Make = "Nissan", Model = "Altima", LicensePlate = "ALT123", Color = "Blue", Year = 2018, Acceleration = 8.7, DailyRentalRate = 27.99, AC = new AC{ MinTemperature=7, MaxTemperature = 18} },
            new() { Make = "Nissan", Model = "Sentra", LicensePlate = "SEN456", Color = "Grey", Year = 2017, Acceleration = 8.4, DailyRentalRate = 26.50, Radio = new Radio{ MinFrequency = 5, MaxFrequency = 10} },
            new() { Make = "Nissan", Model = "Maxima", LicensePlate = "MAX789", Color = "Black", Year = 2019, Acceleration = 7.9, DailyRentalRate = 35.00 },

            new() { Make = "Kia", Model = "Optima", LicensePlate = "OPT123", Color = "Red", Year = 2020, Acceleration = 9.5, DailyRentalRate = 33.00, AC = new AC{ MinTemperature=5, MaxTemperature = 15} },
            new() { Make = "Kia", Model = "Soul", LicensePlate = "SOL456", Color = "White", Year = 2019, Acceleration = 9.2, DailyRentalRate = 31.50, Radio = new Radio{ MinFrequency = 12, MaxFrequency = 24} },
            new() { Make = "Kia", Model = "Sportage", LicensePlate = "SPR789", Color = "Blue", Year = 2018, Acceleration = 8.8, DailyRentalRate = 29.99 },
            ];

            Bus[] buses =
            [
            new() { Make = "Ford", Model = "Transit", LicensePlate = "BUS123", Color = "White", Year = 2020, Acceleration = 8.0, DailyRentalRate = 99.99, AC = new AC{ MinTemperature=10, MaxTemperature = 20}, ExtraDoors = true },
            new() { Make = "Ford", Model = "E-Series", LicensePlate = "FORD456", Color = "Blue", Year = 2019, Acceleration = 7.8, DailyRentalRate = 95.00, ExtraDoors = true },
            new() { Make = "Ford", Model = "F-550", LicensePlate = "FORD789", Color = "Grey", Year = 2018, Acceleration = 7.2, DailyRentalRate = 110.00, Radio = new Radio{ MinFrequency = 50, MaxFrequency = 80}, ExtraDoors = true },

            new() { Make = "Mercedes", Model = "Sprinter", LicensePlate = "SPR456", Color = "Black", Year = 2019, Acceleration = 7.5, DailyRentalRate = 105.00, Radio = new Radio{ MinFrequency = 73, MaxFrequency = 95}, ExtraDoors = true },
            new() { Make = "Mercedes", Model = "Tourismo", LicensePlate = "MER123", Color = "White", Year = 2020, Acceleration = 6.9, DailyRentalRate = 125.00, AC = new AC{ MinTemperature=5, MaxTemperature = 15}, ExtraDoors = true },
            new() { Make = "Mercedes", Model = "Citaro", LicensePlate = "CIT789", Color = "Silver", Year = 2018, Acceleration = 7.3, DailyRentalRate = 115.00, ExtraDoors = true },

            new() { Make = "Volvo", Model = "7800", LicensePlate = "VOL321", Color = "Blue", Year = 2018, Acceleration = 6.5, DailyRentalRate = 120.00, AC = new AC{ MinTemperature=5, MaxTemperature = 25}, ExtraDoors = true },
            new() { Make = "Volvo", Model = "9700", LicensePlate = "VOL654", Color = "Grey", Year = 2019, Acceleration = 6.7, DailyRentalRate = 130.00, Radio = new Radio{ MinFrequency = 60, MaxFrequency = 90}, ExtraDoors = true },
            new() { Make = "Volvo", Model = "B8R", LicensePlate = "VOL987", Color = "Black", Year = 2020, Acceleration = 6.9, DailyRentalRate = 135.00, AC = new AC{ MinTemperature=7, MaxTemperature = 22}, ExtraDoors = true },

            new() { Make = "Scania", Model = "Citywide", LicensePlate = "SCAN123", Color = "Red", Year = 2019, Acceleration = 6.8, DailyRentalRate = 125.00, ExtraDoors = true },
            new() { Make = "Scania", Model = "Interlink", LicensePlate = "SCAN456", Color = "White", Year = 2020, Acceleration = 7.0, DailyRentalRate = 120.00, AC = new AC{ MinTemperature=6, MaxTemperature = 18}, ExtraDoors = true },
            new() { Make = "Scania", Model = "OmniExpress", LicensePlate = "SCAN789", Color = "Blue", Year = 2018, Acceleration = 7.4, DailyRentalRate = 115.00, Radio = new Radio{ MinFrequency = 70, MaxFrequency = 100}, ExtraDoors = true },

            new() { Make = "Iveco", Model = "Daily", LicensePlate = "IVE123", Color = "Silver", Year = 2019, Acceleration = 7.0, DailyRentalRate = 110.00, ExtraDoors = true },
            new() { Make = "Iveco", Model = "Crossway", LicensePlate = "IVE456", Color = "Grey", Year = 2020, Acceleration = 6.7, DailyRentalRate = 125.00, AC = new AC{ MinTemperature=8, MaxTemperature = 20}, ExtraDoors = true },
            new() { Make = "Iveco", Model = "Urbanway", LicensePlate = "IVE789", Color = "Red", Year = 2018, Acceleration = 6.8, DailyRentalRate = 115.00, Radio = new Radio{ MinFrequency = 65, MaxFrequency = 95}, ExtraDoors = true },
            ];

            Truck[] trucks =
            [
            new() { Make = "Volvo", Model = "FH", LicensePlate = "TRK321", Color = "Black", Year = 2019, Acceleration = 12.0, DailyRentalRate = 119.99, CargoCapacity = 20000.0, Radio = new Radio{ MinFrequency = 12, MaxFrequency = 21} },
            new() { Make = "Volvo", Model = "FMX", LicensePlate = "VOL654", Color = "Grey", Year = 2020, Acceleration = 11.5, DailyRentalRate = 125.00, CargoCapacity = 23000.0, AC = new AC{ MinTemperature=15, MaxTemperature = 25} },
            new() { Make = "Volvo", Model = "VNL", LicensePlate = "VOL987", Color = "White", Year = 2018, Acceleration = 11.0, DailyRentalRate = 120.00, CargoCapacity = 22000.0 },

            new() { Make = "Freightliner", Model = "Cascadia", LicensePlate = "FRT654", Color = "Red", Year = 2018, Acceleration = 10.0, DailyRentalRate = 110.00, CargoCapacity = 22000.0, AC = new AC{ MinTemperature=10, MaxTemperature = 18} },
            new() { Make = "Freightliner", Model = "Coronado", LicensePlate = "FRT123", Color = "Blue", Year = 2019, Acceleration = 10.5, DailyRentalRate = 115.50, CargoCapacity = 21000.0, Radio = new Radio{ MinFrequency = 20, MaxFrequency = 30} },
            new() { Make = "Freightliner", Model = "Argosy", LicensePlate = "FRT789", Color = "White", Year = 2020, Acceleration = 9.8, DailyRentalRate = 112.00, CargoCapacity = 21500.0 },

            new() { Make = "Kenworth", Model = "T680", LicensePlate = "KEN987", Color = "Silver", Year = 2017, Acceleration = 11.0, DailyRentalRate = 115.00, CargoCapacity = 21000.0, Radio = new Radio{ MinFrequency = 132, MaxFrequency = 157} },
            new() { Make = "Kenworth", Model = "W900", LicensePlate = "KEN654", Color = "Black", Year = 2019, Acceleration = 12.2, DailyRentalRate = 120.00, CargoCapacity = 24000.0, AC = new AC{ MinTemperature=8, MaxTemperature = 20} },
            new() { Make = "Kenworth", Model = "T880", LicensePlate = "KEN321", Color = "Grey", Year = 2020, Acceleration = 11.3, DailyRentalRate = 118.00, CargoCapacity = 22500.0 },

            new() { Make = "Peterbilt", Model = "579", LicensePlate = "PET123", Color = "Red", Year = 2018, Acceleration = 10.9, DailyRentalRate = 117.50, CargoCapacity = 23000.0, AC = new AC{ MinTemperature=5, MaxTemperature = 18} },
            new() { Make = "Peterbilt", Model = "389", LicensePlate = "PET456", Color = "Black", Year = 2019, Acceleration = 11.1, DailyRentalRate = 119.99, CargoCapacity = 23500.0, Radio = new Radio{ MinFrequency = 25, MaxFrequency = 35} },
            new() { Make = "Peterbilt", Model = "579 UltraLoft", LicensePlate = "PET789", Color = "White", Year = 2020, Acceleration = 10.5, DailyRentalRate = 122.00, CargoCapacity = 24000.0 },

            new() { Make = "Mack", Model = "Anthem", LicensePlate = "MCK123", Color = "Silver", Year = 2019, Acceleration = 11.7, DailyRentalRate = 120.00, CargoCapacity = 22500.0 },
            new() { Make = "Mack", Model = "Pinnacle", LicensePlate = "MCK456", Color = "Blue", Year = 2020, Acceleration = 10.8, DailyRentalRate = 119.00, CargoCapacity = 22000.0, AC = new AC{ MinTemperature=12, MaxTemperature = 22} },
            new() { Make = "Mack", Model = "Granite", LicensePlate = "MCK789", Color = "Grey", Year = 2018, Acceleration = 10.5, DailyRentalRate = 118.50, CargoCapacity = 23000.0, Radio = new Radio{ MinFrequency = 15, MaxFrequency = 25} },
            ];
            foreach (Car c in cars)
            {
                context.Vehicles.Add(c);
            }
            foreach (Bus b in buses)
            {
                context.Vehicles.Add(b);
            }
            foreach (Truck t in trucks)
            {
                context.Vehicles.Add(t);
            }
            context.SaveChanges();
        }
    }
}
