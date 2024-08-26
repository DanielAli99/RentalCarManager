using RentalCarManager.Models;
using RentalCarManager.Resources;
using RentalCarManager.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace RentalCarManager.Handlers
{
    internal class OutputHandler(VehicleService vehicleService)
    {
        public static object MappedSingleSelection(Dictionary<string, object> ChoiceList, string menuText)
        {
            return ChoiceList[AnsiConsole.Prompt(new SelectionPrompt<string>().Title(menuText).AddChoices(ChoiceList.Keys).EnableSearch())];
        }
        public static object SingleSelection(IEnumerable<object> ChoiceList, string menuText)
        {
            Console.Clear();
            return AnsiConsole.Prompt(new SelectionPrompt<object>().Title(menuText).AddChoices(ChoiceList).EnableSearch());
        }
        public static void Display(string message)
        {
            Console.Clear();
            AnsiConsole.Markup(message);
            Console.ReadLine();
        }
        public static string RequestInfo()
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new TextPrompt<string>(DisplayText.CustomerName)
                    .PromptStyle("green")
                    .Validate(name =>
                    {
                        if (Regex.IsMatch(name, @"^[a-zA-Z-'\s]+$"))
                        {
                            return ValidationResult.Success();
                        }
                        else
                        {
                            return ValidationResult.Error(string.Format(DisplayText.InvalidEntry, "Name"));
                        }
                    }));
        }
        public static int SelectVehicleById(List<string> IDs)
        {
            while (true)
            {
                string vehicleId = AnsiConsole.Prompt(
                    new TextPrompt<string>($"Enter a Vehicle ID to select:")
                        .Validate(input =>
                        {
                            if (!IDs.Contains(input))
                            {
                                return ValidationResult.Error(string.Format(DisplayText.InvalidEntry, "ID"));
                            }
                            return ValidationResult.Success();
                        }));

                return int.Parse(vehicleId);
            }
        }
        public IEnumerable<Vehicle> DisplayMultiSelectionFilters(Dictionary<string, string> availableFeatures, Type vehicleType)
        {
            IEnumerable<Vehicle> availableVehicles;
            List<string> selectedFeautures = [];
            Dictionary<string, object> filters = [];
            do
            {
                availableVehicles = vehicleService.FilterVehiclesByProperties(filters, vehicleType);
                AnsiConsole.Clear();
                AnsiConsole.WriteLine($"Number of {vehicleType.Name}'s available: {availableVehicles.Count()}\n");
                var prompt = new SelectionPrompt<string>().Title("Available Features:").EnableSearch();
                foreach (string feature in availableFeatures.Keys)
                {
                    if (selectedFeautures.Contains(feature)) prompt.AddChoice($"[green]{feature}✔[/]");
                    else prompt.AddChoice(feature);
                }
                prompt.AddChoices(["[red]Reset Filters[/]", "[royalblue1]Confirm[/]"]);
                string selection = Regex.Replace(AnsiConsole.Prompt(prompt), @"\[green\]|\[red\]|\[royalblue1\]|\[/\]|✔", string.Empty);
                switch (selection)
                {
                    case "Confirm":
                        return availableVehicles;
                    case "Reset Filters":
                        selectedFeautures.Clear();
                        filters.Clear();
                        break;
                    default:
                        if (!(selectedFeautures.Remove(selection) && filters.Remove(availableFeatures[selection])))
                        {
                            string filterKey = availableFeatures[selection];
                            var featureValues = VehicleService.GetPropertyValues(availableVehicles, filterKey).Order();
                            if (!featureValues.Any())
                            {
                                Display(string.Format(DisplayText.NoFeatures, selection));
                                break;
                            }
                            object? selectedFeauture = SingleSelection(featureValues, $"Select {selection}: ");
                            filters.Add(filterKey, selectedFeauture); selectedFeautures.Add(selection);
                        }
                        break;
                }
            } while (true);
        }
    }
}
