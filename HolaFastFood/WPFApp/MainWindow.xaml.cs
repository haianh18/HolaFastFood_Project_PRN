using HolaBase.Models;
using HolaBase.Service;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HolaFastFoodContext con = new HolaFastFoodContext();
        private TableFoodService tableFoodService = new TableFoodService();
        private BillInfoService billInfoService = new BillInfoService();
        private BillService billService = new BillService();
        private FoodCategoryService foodCategoryService = new FoodCategoryService();
        private FoodService foodService = new FoodService();



        public MainWindow()
        {
            InitializeComponent();
            loadPage();
        }

        //Load items to window

        private void loadPage()
        {
            loadFoodCategory();
            loadTableFood();
            NumericTextBox.Text = "1";
        }
        private void loadTableFood()
        {
            tableGrid.ColumnDefinitions.Clear();
            tableGrid.Children.Clear();
            List<TableFood> tables = tableFoodService.GetTableFoods();

            if (tables != null && tables.Count > 0)
            {
                // Clear existing columns and children
                tableGrid.ColumnDefinitions.Clear();
                tableGrid.Children.Clear();

                int columnsCount = (tables.Count + 5) / 6;
                for (int i = 0; i < columnsCount; i++)
                {
                    tableGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int i = 0; i < tables.Count; i++)
                {
                    int column = i / 6;
                    int row = i % 6;

                    if (tableGrid.RowDefinitions.Count <= row)
                    {
                        tableGrid.RowDefinitions.Add(new RowDefinition());
                    }

                    Button tableButton = new Button
                    {
                        Content = tables[i].Name + "\n" + tables[i].Status,
                        Height = 50,
                        Margin = new Thickness(5),
                        Background = (tables[i].Status.Equals("Empty")) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.RosyBrown),
                        Foreground = (tables[i].Status.Equals("Empty")) ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.White),
                        Tag = tables[i].Id, //Store Table Id in Tag property
                    };

                    tableButton.Click += TableButton_Click; // Attach click event handler

                    Grid.SetColumn(tableButton, column);
                    Grid.SetRow(tableButton, row);
                    tableGrid.Children.Add(tableButton);
                }
            }
        }
        private void loadFoodCategory()
        {
            var foodCategory = foodCategoryService.GetFoodCategories();
            categoryComboBox.ItemsSource = foodCategory;
            categoryComboBox.DisplayMemberPath = "Name";
            categoryComboBox.SelectedValuePath = "Id";
            categoryComboBox.SelectedIndex = 0;
        }



        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                int tableID = (int)clickedButton.Tag;
                var table = tableFoodService.GetTableFood(tableID);
                if (!table.Status.Equals("Empty"))
                {
                    Bill bill = billService.GetBill(tableID);
                    var listSource = con.BillInfos.Where(c => c.IdBill == bill.Id).Include(s => s.IdFoodNavigation).Select(b => new
                    {
                        IdFoodNavigation = b.IdFoodNavigation,
                        Count = b.Count,
                        Id = b.Id,
                        Amount = b.Count * b.IdFoodNavigation.Price
                    }).ToList();
                    orderDataGrid.ItemsSource = listSource;
                    double amount = 0;
                    if (orderDataGrid.ItemsSource != null)
                    {
                        foreach (var item in listSource)
                        {
                            amount += item.Amount;
                        }
                        txtTotalAmount.Text = amount.ToString();
                        txtFinalAmount.Text = amount.ToString();
                        bill.TotalPrice = amount;
                        billService.UpdateBill(bill);
                    }

                }
                else
                {
                    orderDataGrid.ItemsSource = null;
                    txtTotalAmount.Text = "0";
                    txtFinalAmount.Text = "0";
                }
                txtChosenTable.Text = tableID.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Button increase & decrease quantity added to the table
        private void IncreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(NumericTextBox.Text, out int value))
            {
                NumericTextBox.Text = (value + 1).ToString();
            }
        }
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            int currentValue = int.Parse(NumericTextBox.Text);
            if (currentValue > 1)
            {
                NumericTextBox.Text = (currentValue - 1).ToString();
            }
        }
        private void NumericTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void categoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedCategoryId = (int)categoryComboBox.SelectedValue;
            var listFood = foodService.GetFoodByCategory(selectedCategoryId);
            if (listFood != null)
            {
                itemComboBox.ItemsSource = listFood;
                itemComboBox.DisplayMemberPath = "Name";
                itemComboBox.SelectedValuePath = "Id";
                itemComboBox.SelectedIndex = 0;
            }
        }

        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void btnAddFood_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            if (txtChosenTable.Text.Length == 0)
            {
                MessageBox.Show("Please pick a table first!");
                return;
            }

            int.TryParse(txtChosenTable.Text, out var tableId);
            var table = tableFoodService.GetTableFood(tableId);
            var bill = billService.GetBill(tableId);

            if (table.Status.Equals("Empty"))
            {
                orderDataGrid.ItemsSource = null;
                if (bill == null)
                {
                    bill = new Bill()
                    {
                        DateCheckIn = DateTime.Now,
                        IdTable = tableId,
                        Status = 0,
                        UserName = "admin"
                    };
                    billService.SaveBill(bill);
                }

                table.Status = "Occupied";
                tableFoodService.UpdateTable(table);
                txtTotalAmount.Text = "0";
                txtFinalAmount.Text = "0";
                loadTableFood();
            }

            int selectedFoodId = (int)itemComboBox.SelectedValue;
            int.TryParse(NumericTextBox.Text.ToString(), out var quantity);

            var existedBillInfo = con.BillInfos.FirstOrDefault(b => b.IdBill == bill.Id && b.IdFood == selectedFoodId);

            if (existedBillInfo != null)
            {
                existedBillInfo.Count += quantity;
                con.BillInfos.Update(existedBillInfo);
                con.SaveChanges();
                //con.Entry(existedBillInfo).State = EntityState.Modified;
            }
            else
            {
                HolaFastFoodContext con1 = new HolaFastFoodContext();
                BillInfo billInfo = new BillInfo()
                {
                    IdBill = bill.Id,
                    IdFood = selectedFoodId,
                    Count = quantity
                };
                con1.BillInfos.Add(billInfo);
                con1.SaveChanges();
            }

            DisplayBillInfo();


            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An error occurred: {ex.Message}");
            //}
        }

        private void DisplayBillInfo()
        {
            HolaFastFoodContext con2 = new HolaFastFoodContext();
            int.TryParse(txtChosenTable.Text, out var tableId);
            var table = tableFoodService.GetTableFood(tableId);
            var bill = billService.GetBill(tableId);
            var listSource = con.BillInfos
                    .Where(c => c.IdBill == bill.Id)
                    .Include(s => s.IdFoodNavigation)
                    .Select(b => new
                    {
                        IdFoodNavigation = b.IdFoodNavigation,
                        Count = b.Count,
                        Id = b.Id,
                        Amount = b.Count * b.IdFoodNavigation.Price
                    }).ToList();

            orderDataGrid.ItemsSource = listSource;

            double amount = 0;
            if (orderDataGrid.ItemsSource != null)
            {
                foreach (var item in listSource)
                {
                    amount += item.Amount;
                }
                txtTotalAmount.Text = amount.ToString();
                txtFinalAmount.Text = amount.ToString();
                bill.TotalPrice = amount;
                con2.Bills.Update(bill);
                //con.Entry(bill).State = EntityState.Modified;
            }

            con2.SaveChanges();
        }


        private void btnRemoveFood_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            var selectedItem = orderDataGrid.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("No item selected to remove.");
                return;
            }

            var itemIdProperty = selectedItem.GetType().GetProperty("Id");
            if (itemIdProperty == null)
            {
                MessageBox.Show("Selected item does not have an ID property.");
                return;
            }

            int itemId = (int)itemIdProperty.GetValue(selectedItem);
            int.TryParse(txtChosenTable.Text, out var tableId);
            var table = tableFoodService.GetTableFood(tableId);
            var bill = billService.GetBill(tableId);
            if (bill != null)
            {
                var billInfos = billInfoService.GetBillInfosByBillID(bill.Id);
                if (billInfos.Count == 1)
                {
                    Clear();
                    txtChosenTable.Text = tableId.ToString();
                }
                else
                {
                    BillInfo billInfo = billInfoService.GetBillInfoByID(itemId);
                    billInfoService.DeleteBillInfo(billInfo);
                    DisplayBillInfo();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"An error occurred: {ex.Message}");
            //}
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            MessageBoxResult result = MessageBox.Show("Do you want to clear all the food?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            int.TryParse(txtChosenTable.Text, out var tableId);
            var table = tableFoodService.GetTableFood(tableId);
            var bill = billService.GetBill(tableId);
            if (bill != null)
            {
                billService.DeleteBill(bill);
            }
            if (table != null)
            {
                table.Status = "Empty";
                tableFoodService.UpdateTable(table);
            }
            cbDiscount.SelectedIndex = 0;
            NumericTextBox.Text = "1";
            txtTotalAmount.Text = "0";
            txtFinalAmount.Text = "0";
            txtChosenTable.Text = null;
            orderDataGrid.ItemsSource = null;
            loadTableFood();
        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
            this.Close();
        }

        private void ManageStore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            this.Close();
            adminWindow.Show();
        }

        private void btnDiscount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (orderDataGrid.Items.Count == 0 || orderDataGrid.ItemsSource == null || txtChosenTable.Text.Length == 0)
                {
                    MessageBox.Show("Please add food to the table first!");
                    return;
                }

                // Get the selected discount value from the ComboBox
                if (cbDiscount.SelectedItem is ComboBoxItem selectedItem)
                {
                    string discountText = selectedItem.Content.ToString();
                    if (float.TryParse(discountText, out var discount))
                    {
                        if (float.TryParse(txtTotalAmount.Text, out var totalAmount))
                        {
                            float final = (100 - discount) * totalAmount / 100;
                            txtFinalAmount.Text = final.ToString("F0");
                        }
                        else
                        {
                            MessageBox.Show("Total amount is not a valid number.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected discount value is not valid.");
                    }
                }
                else
                {
                    MessageBox.Show("No discount selected.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            if (orderDataGrid.Items.Count == 0 || orderDataGrid.ItemsSource == null || txtChosenTable.Text.Length == 0)
            {
                MessageBox.Show("There are no food to checkout!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Do you want to Checkout?", "Confirmation", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                return;
            }
            int.TryParse(txtChosenTable.Text, out var tableId);
            var table = tableFoodService.GetTableFood(tableId);
            var bill = billService.GetBill(tableId);
            if (bill != null)
            {
                // Load bill info and show invoice window
                var billInfos = con.BillInfos
                    .Where(c => c.IdBill == bill.Id)
                    .Include(s => s.IdFoodNavigation)
                    .ToList();
                bill.TotalPrice = double.Parse(txtFinalAmount.Text);
                bill.Status = 1;
                bill.DateCheckOut = DateTime.Now;
                bill.UserName = txtAccountLogin.Content.ToString();
                if (cbDiscount.SelectedItem is ComboBoxItem selectedItem)
                {
                    string discountText = selectedItem.Content.ToString();
                    bill.Discount = int.Parse(discountText);
                }
                billService.UpdateBill(bill);
                table.Status = "Empty";
                tableFoodService.UpdateTable(table);

                cbDiscount.SelectedIndex = 0;
                NumericTextBox.Text = "1";
                txtTotalAmount.Text = "0";
                txtFinalAmount.Text = "0";
                txtChosenTable.Text = null;
                orderDataGrid.ItemsSource = null;
                loadTableFood();
                MessageBox.Show("Checkout successfully!");


                InvoiceWindow invoiceWindow = new InvoiceWindow(bill, billInfos);
                invoiceWindow.ShowDialog();
            }
        }
    }


}