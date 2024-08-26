using RentalCarManager.Models;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RentalCarManager.Managers.VehicleManager
{
    internal class VehicleDisplayManager
    {
        public static List<string> DisplayVehicleTable(IEnumerable<Vehicle> vehicles)
        {
            Console.Clear();
            Table table = new();
            List<string> vehicleIds = [];
            PropertyInfo[] properties = typeof(Vehicle).GetProperties();

            AddTableColumns(table, properties);
            AddVehicleRows(table, vehicles, properties, vehicleIds);

            table.Title("Vehicle List");
            table.Caption("Displaying vehicles.");
            table.Expand();
            AnsiConsole.Write(table);

            return vehicleIds;
        }

        private static void AddTableColumns(Table table, PropertyInfo[] properties)
        {
            foreach (PropertyInfo prop in properties)
            {
                table.AddColumn(prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name);
            }
        }

        private static void AddVehicleRows(Table table, IEnumerable<Vehicle> vehicles, PropertyInfo[] properties, List<string> vehicleIds)
        {
            foreach (Vehicle vehicle in vehicles)
            {
                table.AddRow(properties.Select(prop =>
                {
                    return GetPropertyDisplayValue(vehicle, prop, vehicleIds);
                }).ToArray());
            }
        }

        private static string GetPropertyDisplayValue(Vehicle vehicle, PropertyInfo prop, List<string> vehicleIds)
        {
            if (typeof(IOptionalFeatures).IsAssignableFrom(prop.PropertyType))
            {
                return (prop.GetValue(vehicle) as IOptionalFeatures)?.GetDescription ?? "N/A";
            }
            if (typeof(CustomerInfo).IsAssignableFrom(prop.PropertyType))
            {
                return (prop.GetValue(vehicle) as CustomerInfo)?.Name ?? "N/A";
            }
            if (prop.GetCustomAttribute<KeyAttribute>() is not null)
            {
                vehicleIds.Add(prop.GetValue(vehicle).ToString());
            }
            return prop.GetValue(vehicle)?.ToString() ?? "N/A";
        }

    }
}
