using System.ComponentModel;

namespace RentalCarManager.Enums
{
    internal static class MenuOptions
    {
        public enum MainMenu
        {
            [Description("Create a new rental booking")] NewRental,
            [Description("Return existing rental vehicle")] ReturnRental,
            [Description("View Vehicles")] ViewVehicles,
            [Description("View currently active rentals")] ViewActiveRentals,
            [Description("Change existing vehicle data")] AmendVehicle,
            [Description("Add new vehicle")] NewVehicle,
        }
    }
}
