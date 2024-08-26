using Microsoft.EntityFrameworkCore;
using RentalCarManager.CustomDataAnnotations;
using RentalCarManager.Data;
using RentalCarManager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace RentalCarManager.Services
{
    internal class VehicleService(VehicleContext context)
    {
        public async Task AddNewVehicleAsync(Type vehicleType, Dictionary<string, object> Properties)
        {
            var newVehicleInstance = Activator.CreateInstance(vehicleType);
            foreach (var prop in Properties)
            {
                PropertyInfo? property = vehicleType.GetProperty(prop.Key);
                property.SetValue(newVehicleInstance, prop.Value);
            }
            context.Entry(newVehicleInstance).State = EntityState.Added;
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
        public async Task UpdateVehicleAsync(Vehicle vehicle, object value, PropertyInfo property)
        {
            property.SetValue(vehicle, value);
            context.Entry(vehicle).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task<List<Vehicle>> FetchVehicleTypesAsync() { return await context.Vehicles.ToListAsync(); }
        public async Task<List<Vehicle>> FetchCustomersAsync()
        {
            return await context.Vehicles.Where(v => v.Customer != null).ToListAsync();
        }
        public static IEnumerable<object> GetPropertyValues(IEnumerable<Vehicle> vehicles, string propertyName)
        {
            return vehicles.SelectMany(vehicle =>
            {
                object? propertyValue = vehicle.GetType().GetProperty(propertyName)?.GetValue(vehicle);
                if (propertyValue is IOptionalFeatures optionalFeature)
                {
                    return [optionalFeature.GetDescription];
                }
                else if (propertyValue != null)
                {
                    return [propertyValue];
                }
                return Enumerable.Empty<object>();
            }).ToHashSet();
        }
        public static Dictionary<string, bool> FetchRequiredVehicleProperties(Type vehicleType)
        {
            Dictionary<string, bool> attributes = [];
            foreach (PropertyInfo property in vehicleType.GetProperties())
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() != null ||
                    property.GetCustomAttribute<KeyAttribute>() != null ||
                    property.GetCustomAttribute<SystemManaged>() != null) continue;
                attributes.Add(property.Name, property.GetCustomAttribute<RequiredAttribute>() != null);
            }
            return attributes;
        }
        public static Dictionary<string, string> FetchModifiableVehicleProperties(Type vehicleType)
        {
            Dictionary<string, string> attributes = [];
            foreach (PropertyInfo property in vehicleType.GetProperties())
            {
                if (property.GetCustomAttribute<NotMappedAttribute>() != null ||
                   property.GetCustomAttribute<KeyAttribute>() != null ||
                   property.GetCustomAttribute<SystemManaged>() != null) continue;
                FilterName filterAttribute = property.GetCustomAttribute<FilterName>();
                DisplayAttribute DisplayAttribute = property.GetCustomAttribute<DisplayAttribute>();
                if (filterAttribute != null) attributes.Add(filterAttribute.Name, property.Name);
                else if (DisplayAttribute != null) attributes.Add(DisplayAttribute.Name, property.Name);
                else attributes.Add(property.Name, property.Name);
            }
            return attributes;
        }
        public static Dictionary<string, string> FetchFilterableVehicleProperties(Type vehicleType)
        {
            Dictionary<string, string> attributes = [];
            foreach (PropertyInfo property in vehicleType.GetProperties())
            {
                FilterName filterAttribute = property.GetCustomAttribute<FilterName>();
                if (filterAttribute != null) attributes.Add(filterAttribute.Name, property.Name);
            }
            return attributes;
        }
        public IEnumerable<Vehicle> FilterVehiclesByProperties(Dictionary<string, object> filters, Type vehicleType)
        {
            IEnumerable<Vehicle> query = context.Vehicles.AsEnumerable().Where(vehicle => vehicle.GetType().Equals(vehicleType) && !vehicle.Rented);
            foreach (var filter in filters)
            {
                PropertyInfo propInfo = vehicleType.GetProperty(filter.Key);
                if (propInfo != null)
                {
                    if (typeof(IOptionalFeatures).IsAssignableFrom(propInfo.PropertyType))
                    {
                        query = query.Where(item =>
                        {
                            if (propInfo.GetValue(item) is IOptionalFeatures feature)
                            {
                                return feature.GetDescription.Equals(filter.Value);
                            }
                            return false;
                        });
                    }
                    else
                    {
                        query = query.Where(item =>
                        {
                            object? value = propInfo.GetValue(item);
                            return value != null && value.Equals(filter.Value);
                        });
                    }
                }
            }
            return query;
        }
    }
}
