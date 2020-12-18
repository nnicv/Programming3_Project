using System;

namespace Project
{
    public enum Categories
    {
        Meat_Produce,
        Desserts,
        Snacks,
        Dairy,
        Drinks,
        Frozen_Food,
        Fruit,
        Vegetables,
        Bakery,
        Cleaning_Supplies,
        Pet_Accessories,
        Other
    }
    class Item
    {
        private int _availableQuantity;
        private int _minQuantity;
        private string _location;
        private string _supplier;

        public Item()
        {
            AvailableQuantity = 0;
            MinQuantity = 1;
            Location = "TBA";
            Supplier = "TBA";
            Category = Categories.Other;
        }
        public Item(int minQuantity, int availableQuantity)
        {
            AvailableQuantity = availableQuantity;
            MinQuantity = minQuantity;
            Location = "TBA";
            Supplier = "TBA";
            Category = Categories.Other;
        }
        public Item(int minQuantity, int availableQuantity,string locationName, string supplierName, Categories categoryOfItem)
        {
            AvailableQuantity = availableQuantity;
            MinQuantity = minQuantity;
            Location = locationName;
            Supplier = supplierName;
            Category = categoryOfItem;
        }
        public Item(string name, int minQuantity, int availableQuantity, string locationName, string supplierName, Categories categoryOfItem)
        {
            AvailableQuantity = availableQuantity;
            MinQuantity = minQuantity;
            Location = locationName;
            Supplier = supplierName;
            Category = categoryOfItem;
            Name = name;
        }
        public string Name { get; set; }
        public int AvailableQuantity{
            get => _availableQuantity;
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Available Quantity","Cannot have a negative Quantity");
                _availableQuantity = value;
            }
        }
        public int MinQuantity
        {
            get => _minQuantity;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Min Quantity", "Cannot have a Max Quantity of less than 1");
                _minQuantity = value;
            }
        }
        public string Location {
            get => _location;
            set {
                if (value == null)
                    throw new Exception("Location cannot have a value of null");
                _location = value;
            }
        }
        public string Supplier
        {
            get => _supplier;
            set
            {
                if (value == null)
                    throw new Exception("Supplier cannot have a value of null");
                _supplier = value;
            }
        }
        public Categories Category { get; set; }
    }
}
