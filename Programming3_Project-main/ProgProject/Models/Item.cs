using System;

namespace ProgProject
{
    //Enum of all the available categories
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
        //Backing fields for each Item
        private int _availableQuantity;
        private int _minQuantity;
        private string _location;
        private string _supplier;

        public Item()
        {
            //default constructor in case its required
            AvailableQuantity = 0;
            MinQuantity = 1;
            Location = "TBA";
            Supplier = "TBA";
            Category = Categories.Other;
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
        //Auto Getter/Setter for Name
        public string Name { get; set; }
        //Getter/Setter for Available Quantity with check for negative values
        public int AvailableQuantity{
            get => _availableQuantity;
            set {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Available Quantity","Cannot have a negative Quantity");
                _availableQuantity = value;
            }
        }
        //Getter/Setter for Minimum Quantity with check for values below 1
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
        //Getter/Setter for Location with check for values that are null
        public string Location {
            get => _location;
            set {
                if (value == null)
                    throw new Exception("Location cannot have a value of null");
                _location = value;
            }
        }
        //Getter/Setter for Supplier with check for values that are null
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
        //Auto Getter/Setter for Category
        public Categories Category { get; set; }
    }
}
