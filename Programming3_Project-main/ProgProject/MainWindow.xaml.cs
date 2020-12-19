using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ProgProject
{
    /// <summary>
    /// Nicholas Chudinov 1423131
    /// Kevin-Christopher Laskai 1948515
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Fields for storing the inventory data as well as save information
        Inventory _inventoryTracker = new Inventory();
        int _savedRecordIndex=0;

        public MainWindow()
        {
            //Initialize data for the main window and set the correct ItemSource for the Item Data
            DataContext = _inventoryTracker;
            InitializeComponent();
            ItemDataGrid.ItemsSource = _inventoryTracker;
            UpdateBtn.Visibility = Visibility.Collapsed;
            RemoveBtn.Visibility = Visibility.Collapsed;
        }
        private void GenerateShoppingList(object sender, RoutedEventArgs e) => MessageBox.Show(_inventoryTracker.ShoppingListReport(), "Shopping List Report",MessageBoxButton.OK, MessageBoxImage.Information);

        private void GenerateGeneralReport(object sender, RoutedEventArgs e) => MessageBox.Show(_inventoryTracker.GeneralReport(), "Report", MessageBoxButton.OK, MessageBoxImage.Information);

        private void AddItem_BtnClick(object sender, RoutedEventArgs e)
        {
            //Shows the Add item window as well as passes the required objects
            AddItemWindow addItemWindow = new AddItemWindow(ItemDataGrid);
            addItemWindow.SetInventoryObj(_inventoryTracker);
            addItemWindow.Show();
        }
        private void Row_Click(object sender, MouseButtonEventArgs e) {
            //Event handler that deals with single left mouse clicks on the DataGrid in the window to show/hide the conditional buttons
            if (ItemDataGrid.SelectedIndex != -1)
            {
                UpdateBtn.Visibility = Visibility.Visible;
                RemoveBtn.Visibility = Visibility.Visible;
            }
            else {
                UpdateBtn.Visibility = Visibility.Collapsed;
                RemoveBtn.Visibility = Visibility.Collapsed;
            }
        }
        private void SaveItems_BtnClick(object sender, RoutedEventArgs e) => Save();
        private void Save()
        {
            //Sets up the Save dialog window for the App as well as attempts to save the data to a json file
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON File (*.json)|*.json";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "data";
            saveFileDialog.DefaultExt = ".json";
            try
            {
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, _inventoryTracker.SaveItems());
                else
                    return;
            }
            catch (Exception e) {
                MessageBox.Show("Error writing saving to file: " + e.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadItems_BtnClick(object sender, RoutedEventArgs e)
        {
            //Attemps to load the data from the selected (json) file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try
            {
                if (openFileDialog.ShowDialog() == true)
                    _inventoryTracker = Inventory.LoadItems(File.ReadAllText(openFileDialog.FileName));
                else
                    return;
                ItemDataGrid.ItemsSource = null;
                ItemDataGrid.ItemsSource = _inventoryTracker;
            }
            catch (Exception exception) {
                MessageBox.Show("Error trying to load data from json file: " + exception.Message, "Load Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveItem_BtnClick(object sender, RoutedEventArgs e)
        {
            if (ItemDataGrid.SelectedIndex > -1)
            {
                _inventoryTracker.RemoveItem(ItemDataGrid.SelectedIndex);
                ItemDataGrid.ItemsSource = null;
                ItemDataGrid.ItemsSource = _inventoryTracker;
            }
        }

        private void UpdateItem_BtnClick(object sender, RoutedEventArgs e)
        {
            if (ItemDataGrid.SelectedIndex > -1)
            {
                UpdateItem updateItemWindow = new UpdateItem(ItemDataGrid);
                updateItemWindow.SetInventoryObj(_inventoryTracker);
                updateItemWindow.Show();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            //Catch the event of trying to close the app
            if (!CheckLoadExit())
                e.Cancel = true;
        }
        private bool CheckLoadExit() {
            //Ask the user what they want to do with the unsaved data and proceed based on their response
            if (_savedRecordIndex < _inventoryTracker.Count) {
                MessageBoxResult result = MessageBox.Show("Data has not been saved.\nDo you want to save your data before you continue?", "Data Not Saved", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                
                switch (result) {
                    case MessageBoxResult.No:
                        return true;
                    case MessageBoxResult.Cancel:
                        return false;
                    case MessageBoxResult.Yes:
                        Save();
                        break;
                }

                if (_savedRecordIndex < _inventoryTracker.Count)
                    return false;
            }
            return true;
        }
    }
}
