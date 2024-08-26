using RentalCarManager.Handlers;
using RentalCarManager.Models;
using RentalCarManager.Resources;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace RentalCarManager.Validators
{
    internal static class ValidateInput
    {
        public static object UpdateProperty(Type vehicleType, string propertyName)
        {
            PropertyInfo? propertyInfo = vehicleType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy);
            while (true)
            {
                object value;
                if (typeof(IOptionalFeatures).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    value = OptionalFeaturesInstance(propertyInfo.PropertyType);

                }
                else if (propertyInfo.Name.Equals("Color"))
                {
                    value = OutputHandler.SingleSelection(Enum.GetNames(typeof(ConsoleColor)).ToList(), "");
                }
                else
                {
                    try
                    {
                        value = Convert.ChangeType(AnsiConsole.Ask<string>($"Please enter a value for {propertyName}:"), propertyInfo.PropertyType);
                    }
                    catch (Exception ex) when (ex is FormatException || ex is InvalidCastException)
                    {
                        AnsiConsole.MarkupLine(string.Format(DisplayText.InvalidEntry, propertyInfo.PropertyType.Name));
                        continue;
                    }
                }
                var propertyInstance = Activator.CreateInstance(vehicleType);
                var validationResults = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var validationContext = new ValidationContext(propertyInstance) { MemberName = propertyName };

                if (!Validator.TryValidateProperty(value, validationContext, validationResults))
                {
                    foreach (var validationResult in validationResults)
                        AnsiConsole.MarkupLine($"[red]{validationResult.ErrorMessage}[/]");
                    continue;
                }
                return value;
            }
        }
        private static object OptionalFeaturesInstance(Type featureType)
        {
            object featureInstance = Activator.CreateInstance(featureType);
            var properties = featureInstance.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanWrite);
            foreach (var property in properties)
            {
                while (true)
                {
                    try
                    {
                        object convertedValue = Convert.ChangeType(AnsiConsole.Ask<string>($"Please enter a value for {property.Name}:"), property.PropertyType);
                        property.SetValue(featureInstance, convertedValue);
                        break;
                    }
                    catch (Exception ex) when (ex is FormatException || ex is InvalidCastException)
                    {
                        AnsiConsole.MarkupLine(string.Format(DisplayText.InvalidEntry, property.PropertyType.Name));
                        continue;
                    }
                }
            }
            return featureInstance;
        }
    }
}
