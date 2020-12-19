using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ProgProject
{
    class Inventory : IEnumerable<Item>
    {
        //Backing fields to contain the list of Items
        private List<Item> _itemsInventory;

        //Constructors for the Inventory Class, One of which takes in an Item List in order to populate the Inventory instantly
        public Inventory() => _itemsInventory = new List<Item>();
        private Inventory(List<Item> items) => _itemsInventory = items;
        public void AddItem(string name, int minQuantity, int availableQuantity, string locationName, string supplierName, Categories category) => _itemsInventory.Add(new Item(name, minQuantity, availableQuantity, locationName, supplierName, category));
        public void RemoveItem(int index)
        {
            if (index != -1)
                _itemsInventory.RemoveAt(index);
            else
                throw new Exception(String.Format("Error could not find the Item with index {0}", index));
        }
        public int Count { 
            get => _itemsInventory.Count;
        }
        public void UpdateItem(int index, Item itemData) => _itemsInventory[index] = itemData;
        public string GeneralReport() => GenerateReport("General Report of items in store \n");
        public string ShoppingListReport() => GenerateReport("Items that need to be purchased for more stock \n", false);
        private string GenerateReport(string headerStr, bool fullReport=true) {
            //Generate a report based on which Report is called to be created
            string formatting = "{0,-30} {1,-5} {2,-30} {1,-5} {3,-30}\n";
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.AppendLine(headerStr);
            strBuilder.AppendFormat(formatting, "Name", "|", "Minimum Quantity", "Available Quantity");
            foreach (Item item in _itemsInventory)
            {
                if (fullReport)
                    strBuilder.AppendFormat(formatting, item.Name,"|", item.MinQuantity, item.AvailableQuantity);
                else
                    if (item.AvailableQuantity < item.MinQuantity)
                        strBuilder.AppendFormat(formatting, item.Name,"|", item.MinQuantity, item.AvailableQuantity);
            }

            return strBuilder.ToString();

        }
        //Process the JSON string data back into a List of Item objects and returns a new Inventory object with said Items
        public static Inventory LoadItems(string data) => new Inventory(JsonConvert.DeserializeObject<List<Item>>(data));
        //Process all the items in the inventory object into a JSON string
        public string SaveItems() => JsonConvert.SerializeObject(_itemsInventory);
        //Enumeration implementation
        public IEnumerator<Item> GetEnumerator() => _itemsInventory.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
