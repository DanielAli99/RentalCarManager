using RentalCarManager.Models;
using System.ComponentModel;

namespace RentalCarManager.DisplayHelpers
{
    internal static class MapObjectDisplayName
    {
        private static Dictionary<string, object> VehicleTypeListCache;
        private static readonly Dictionary<Enum, string> DisplayNameCache = [];
        public static Dictionary<string, object> DisplayNameList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.GetDescription()).ToDictionary();
        }
        public static Dictionary<string, object> VehicleTypeList(this List<Vehicle> vehicle)
        {
            return VehicleTypeListCache ??= vehicle.AsEnumerable().Select(vehicle => new { TypeName = vehicle.GetType().Name, Type = (object)vehicle.GetType() })
            .Distinct().ToDictionary(item => item.TypeName, item => item.Type);
        }
        private static (string, object) GetDescription(this Enum value)
        {
            if (DisplayNameCache.TryGetValue(value, out string? cachedDescription)) return (cachedDescription, value);
            string description = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description;
            DisplayNameCache.Add(value, description);
            return (description, value);
        }
    }
}
