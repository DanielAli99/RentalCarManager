using RentalCarManager.Enums;
using RentalCarManager.Handlers;
using RentalCarManager.Models;
using RentalCarManager.Resources;
using RentalCarManager.Services;
using RentalCarManager.Managers.VehicleManager;
using static RentalCarManager.Handlers.OutputHandler;
using static RentalCarManager.DisplayHelpers.MapObjectDisplayName;
using static RentalCarManager.Services.VehicleService;


namespace RentalCarManager.Managers
{
    internal class ConsoleManager(VehicleService vehicleService, OutputHandler outputHandler)
    {
        public void Run()
        {
            while (true)
            {
                Console.Clear();
                switch (MappedSingleSelection(DisplayNameList<MenuOptions.MainMenu>(), DisplayText.MainMenu))
                {
                    case MenuOptions.MainMenu.NewRental:
                        NewRental();
                        break;
                    case MenuOptions.MainMenu.ReturnRental:
                        ReturnRental();
                        break;
                    case MenuOptions.MainMenu.ViewVehicles:
                        ViewVehicles();
                        break;
                    case MenuOptions.MainMenu.ViewActiveRentals:
                        ViewActiveRentals();
                        break;
                    case MenuOptions.MainMenu.AmendVehicle:
                        AmendVehicle();
                        break;
                    case MenuOptions.MainMenu.NewVehicle:
                        NewVehicle();
                        break;
                }
            }
        }
        private async void NewRental()
        {
            Dictionary<string, object> vehicleTypeList = vehicleService.FetchVehicleTypesAsync().GetAwaiter().GetResult().VehicleTypeList();
            Type vehicleType = MappedSingleSelection(vehicleTypeList, DisplayText.VehicleType) as Type;
            IEnumerable<Vehicle> availableVehicles = outputHandler.DisplayMultiSelectionFilters(FetchFilterableVehicleProperties(vehicleType), vehicleType);
            int id = SelectVehicleById(VehicleDisplayManager.DisplayVehicleTable(availableVehicles));
            Vehicle? selectedVehicle = availableVehicles.FirstOrDefault(x => x.VehicleId.Equals(id));
            string customerName = RequestInfo();
            CustomerInfo newCustomer = new() { DateRented = DateTime.Now, Name = customerName, Vehicle = selectedVehicle };
            await vehicleService.UpdateVehicleAsync(selectedVehicle, newCustomer, typeof(Vehicle).GetProperty("Customer"));
            Display(string.Format(DisplayText.RentalSuccess, customerName, selectedVehicle.Make));
        }
        private async void ReturnRental()
        {
            List<Vehicle> rentedVehicles = vehicleService.FetchCustomersAsync().GetAwaiter().GetResult();
            Dictionary<string, object> vehicleByCustomerName = rentedVehicles
            .ToDictionary(v => string.Format(DisplayText.CustomerDetails, v.Customer.Name, v.Make, v.Customer.DateRented), v => (object)v);
            if (rentedVehicles.Count == 0) { Display(DisplayText.NoCustomers); return; }
            Vehicle selectedVehicle = MappedSingleSelection(vehicleByCustomerName, "Please select a customer:") as Vehicle;
            string amountDue = ((DateTime.Now - selectedVehicle.Customer.DateRented).TotalDays * selectedVehicle.DailyRentalRate).ToString("C");
            SingleSelection(["Confirm payment"], string.Format(DisplayText.AmountDue, amountDue));
            await vehicleService.UpdateVehicleAsync(selectedVehicle, null, typeof(Vehicle).GetProperty("Customer"));
            Display(DisplayText.ReturnSuccess);
        }
        private void ViewVehicles()
        {
            List<Vehicle> vehicles = vehicleService.FetchVehicleTypesAsync().GetAwaiter().GetResult();
            Dictionary<string, object> vehicleTypes = new(vehicles.VehicleTypeList()) { ["All"] = null };
            VehicleDisplayManager.DisplayVehicleTable(MappedSingleSelection(vehicleTypes, DisplayText.VehicleType) is not Type selectedVehicleType ?
            vehicles : vehicles.AsEnumerable().Where(vehicle => vehicle.GetType().Equals(selectedVehicleType)));
            Console.ReadLine();
        }
        private void ViewActiveRentals()
        {
            List<Vehicle> rentedVehicles = vehicleService.FetchCustomersAsync().GetAwaiter().GetResult();
            VehicleDisplayManager.DisplayVehicleTable(rentedVehicles);
            Console.ReadLine();
        }
        private async void AmendVehicle()
        {
            List<Vehicle> vehicles = vehicleService.FetchVehicleTypesAsync().GetAwaiter().GetResult();
            Dictionary<string, object> vehicleTypes = vehicles.VehicleTypeList();
            Type selectedVehicleType = MappedSingleSelection(vehicleTypes, DisplayText.VehicleType) as Type;
            IEnumerable<Vehicle> allVehicles = vehicles.AsEnumerable().Where(vehicle => vehicle.GetType().Equals(selectedVehicleType));
            List<string> allIDs = VehicleDisplayManager.DisplayVehicleTable(allVehicles);
            int id = SelectVehicleById(allIDs);
            var selectedVehicle = allVehicles.FirstOrDefault(x => x.VehicleId.Equals(id));
            Type vehicleType = selectedVehicle.GetType();
            var propertiesContext = new VehiclePropertyContext<string>
            {
                AvailableFeatures = FetchModifiableVehicleProperties(selectedVehicleType),
                VehicleType = vehicleType
            };
            VehiclePropertyManager.ManageVehicleProperties(propertiesContext);
            Dictionary<string, object> updatedProperties = propertiesContext.properties;
            foreach (var prop in updatedProperties)
            {
                await vehicleService.UpdateVehicleAsync(selectedVehicle, prop.Value, vehicleType.GetProperty(prop.Key));
            }
            Display(string.Format(DisplayText.VehicleAmended, string.Join(Environment.NewLine, updatedProperties.Keys)));
        }
        private async void NewVehicle()
        {
            List<Vehicle> vehicles = vehicleService.FetchVehicleTypesAsync().GetAwaiter().GetResult();
            Dictionary<string, object> vehicleTypes = vehicles.VehicleTypeList();
            Type selectedVehicleType = MappedSingleSelection(vehicleTypes, DisplayText.VehicleType) as Type;
            var propertiesContext = new VehiclePropertyContext<bool>
            {
                AvailableFeatures = FetchRequiredVehicleProperties(selectedVehicleType),
                VehicleType = selectedVehicleType,
                Required = true
            };
            VehiclePropertyManager.ManageVehicleProperties(propertiesContext);
            await vehicleService.AddNewVehicleAsync(selectedVehicleType, propertiesContext.properties);
            Display(string.Format(DisplayText.VehicleCreated, selectedVehicleType.Name));
        }
    }
}
