using System;
using System.Windows;
using System.Windows.Controls;

namespace ProgProject
{
    /// <summary>
    /// Interaction logic for AddItemWindow.xaml
    /// </summary>
    public partial class AddItemWindow : Window
    {
        Inventory _inventoryTracker;
        DataGrid _dataGrid;
        public AddItemWindow(DataGrid dataGrid)
        {
            InitializeComponent();
            _dataGrid = dataGrid;
            Category.ItemsSource = Enum.GetValues(typeof(Categories));
        }
        public void SetInventoryObj(object inventory) => _inventoryTracker = inventory as Inventory;
        private void AddItemToList_BtnClick(object sender, RoutedEventArgs e)
        {
            string error = "";
            if (ItemName.Text != "" && ItemName.Text != null &&
                MinQty.Text != "" && MinQty.Text != null &&
                Qty.Text != "" && Qty.Text != null &&
                Location.Text != "" && Location.Text != null &&
                Supplier.Text != "" && Supplier.Text != null &&
                Category.SelectedIndex != -1)
            {
                int minQty = -1;
                int qty = -1;
                if (Int32.TryParse(MinQty.Text, out minQty) && Int32.TryParse(Qty.Text, out qty))
                {
                    _inventoryTracker.AddItem(ItemName.Text, Convert.ToInt32(MinQty.Text), Convert.ToInt32(Qty.Text), Location.Text, Supplier.Text, (Categories)Category.SelectedIndex);
                    _dataGrid.ItemsSource = null;
                    _dataGrid.ItemsSource = _inventoryTracker;
                    this.Close();
                }
                else
                {
                    error = "You have entered an incorrect value into the ";
                    error += minQty == -1 ? "Minimum Quantity. Please enter a positive value higher than 1." : "Quantity. Please enter a positive value higher than 0.";
                }
            }
            else {
                error = "You did not enter a value into each Text field. Please make sure that all the fields are filled up for this item.";
            }
            if(error != "")
                MessageBox.Show(error, "Value Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
