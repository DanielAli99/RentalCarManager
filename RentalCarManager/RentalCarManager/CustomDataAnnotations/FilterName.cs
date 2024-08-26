namespace RentalCarManager.CustomDataAnnotations
{
    class FilterName(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
