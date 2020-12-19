using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Inventory
    {
        private List<Item> _itemsInventory;
        public Inventory() {
            _itemsInventory = new List<Item>();
        }
        public List<Item> GetItemsInventory() => _itemsInventory;
        public Item GetItemInInventory(int index) => _itemsInventory[index];
        public void AddItem(string name, int minQuantity, int availableQuantity, string locationName, string supplierName, int category)
        {
            //return bool for success?
            _itemsInventory.Add(new Item(name, minQuantity, availableQuantity, locationName, supplierName,(Categories)category));
        }
        public void AddItem(string name, int minQuantity, int availableQuantity, string locationName, string supplierName, Categories category)
        {
            //return bool for success?
            _itemsInventory.Add(new Item(name, minQuantity, availableQuantity, locationName, supplierName, category));
        }
        public void RemoveItem(int index)
        {
            //return bool for success?
            _itemsInventory.RemoveAt(index);
        }
        public void RemoveItem(string name)
        {
            //return bool for success?
            int index = SearchByName(name);
            if (index != -1)
                _itemsInventory.RemoveAt(index);
            else
                Console.WriteLine("Error could not find the Item called {0}", name);
        }
        public void UpdateItem(int index, Item itemData)
        {
            //Rethink the logic, might not be needed cause pass by ref
            //returns bool on success?
            //or return changed available Quantity
            _itemsInventory[index].Name = itemData.Name;
            _itemsInventory[index].MinQuantity = itemData.MinQuantity;
            _itemsInventory[index].AvailableQuantity = itemData.AvailableQuantity;
            _itemsInventory[index].Location = itemData.Location;
            _itemsInventory[index].Supplier = itemData.Supplier;
            _itemsInventory[index].Category = itemData.Category;
        }
        public string GeneralReport() => GenerateReport("General Report of items in store \n");
        public string ShoppingListReport() => GenerateReport("Items that need to be purchased for more stock \n", false);
        private string GenerateReport(string headerStr, bool fullReport=true) {
            string formatting = "{0,-20} {1} {2,-20} {1} {3,-20}\n";
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine(headerStr);
            strBuilder.AppendFormat(formatting, "Name", "|", "Minimum Quantity", "Available Quantity");
            foreach (Item item in _itemsInventory)
            {
                if (fullReport)
                    strBuilder.AppendFormat(formatting, item.Name, item.MinQuantity, item.AvailableQuantity);
                else
                    if (item.AvailableQuantity < item.MinQuantity)
                        strBuilder.AppendFormat(formatting, item.Name, item.MinQuantity, item.AvailableQuantity);
            }

            return strBuilder.ToString();

        }
        public static Inventory LoadItems()
        {
            //ask for file
            //get data
            //process it
            //return object with processed data
            return new Inventory();
        }
        public void SaveItems()
        {
            //ask for location
            //write in proper format into that file
            //return bool for success?

        }
        public int SearchByName(string nameOfItem)
        {
            for (int i = 0; i < _itemsInventory.Count; i++) {
                if (_itemsInventory[i].Name == nameOfItem)
                    return i;
            }
            return -1;
        }
    }
}
