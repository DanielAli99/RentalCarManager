using RentalCarManager.Handlers;
using RentalCarManager.Validators;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace RentalCarManager.Managers.VehicleManager
{
    internal class VehiclePropertyContext<T>
    {
        public Dictionary<string, T> AvailableFeatures { get; set; }
        public Type VehicleType { get; set; }
        public bool Required { get; set; } = false;
        public List<string> selectedFeautures = [];
        public Dictionary<string, object> properties = [];
    }
    internal class VehiclePropertyManager
    {
        public static void ManageVehicleProperties<T>(VehiclePropertyContext<T> context)
        {
            do
            {
                AnsiConsole.Clear();
                string selection = Regex.Replace(AnsiConsole.Prompt(CreateSelectionPrompt(context)), @"\[green\]|\[red\]|\[royalblue1\]|\[/\]|✔|\*Required\*", string.Empty).Trim();
                if (ProcessSelection(selection, context)) return;
            } while (true);
        }
        private static SelectionPrompt<string> CreateSelectionPrompt<T>(VehiclePropertyContext<T> context)
        {
            var prompt = new SelectionPrompt<string>().Title("Available Features:").EnableSearch();
            foreach (var feature in context.AvailableFeatures)
            {
                if (context.selectedFeautures.Contains(feature.Key)) prompt.AddChoice($"[green]{feature.Key}✔[/]");
                else if (context.Required && feature.Value is bool and true) prompt.AddChoice($"{feature.Key} [red]*Required*[/]");
                else prompt.AddChoice(feature.Key);
            }
            prompt.AddChoices(["[red]Reset Filters[/]", "[royalblue1]Confirm[/]"]);
            return prompt;
        }
        private static bool ProcessSelection<T>(string selection, VehiclePropertyContext<T> context)
        {
            switch (selection)
            {
                case "Confirm":
                    if (context.Required && context.AvailableFeatures.Where(x => x.Value is bool and true).Select(x => x.Key).All(key => context.properties.ContainsKey(key)))
                    { return true; }
                    else if (context.Required) OutputHandler.Display("Please populate all [red]required[/] properties");
                    else { return true; }
                    break;
                case "Reset Filters":
                    context.properties.Clear();
                    context.selectedFeautures.Clear();
                    break;
                default:
                    HandleDefaultCase<T>(selection, context);
                    break;
            }
            return false;
        }
        private static void HandleDefaultCase<T>(string selection, VehiclePropertyContext<T> context)
        {
            string propertyName = context.Required ? selection : context.AvailableFeatures[selection].ToString();
            if (!(context.selectedFeautures.Remove(selection) && context.properties.Remove(propertyName)))
            {
                context.properties.Add(propertyName, ValidateInput.UpdateProperty(context.VehicleType, propertyName));
                context.selectedFeautures.Add(selection);
            }
        }
    }
}
