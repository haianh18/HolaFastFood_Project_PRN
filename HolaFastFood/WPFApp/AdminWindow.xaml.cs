using HolaBase.Models;
using HolaBase.Service;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Func<double, string> Formatter { get; set; }

        public AdminWindow()
        {
            InitializeComponent();
            LoadAccount();
            LoadFood();
            LoadBill();
            LoadYears();
            LoadRevenueData(DateTime.Now);
            Formatter = value => string.Format(CultureInfo.InvariantCulture, "{0:N3}", value);
            DataContext = this;
        }

        //Start Manage Account Tab
        private void Clear()
        {
            txtDisplayName.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtRole.Text = string.Empty;
            txtSearchAccount.Text = string.Empty;
            LoadAccount();
        }

        private void LoadAccount()
        {
            try
            {
                AccountService accountService = new AccountService();
                List<Account> accounts = accountService.GetAllAccounts();
                dgAccount.ItemsSource = null;
                dgAccount.ItemsSource = accounts;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fail to load account!");
            }
        }

        private void btnRefreshAccount_Clicked(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnAddAccount_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                AccountService accountService = new AccountService();
                Account account = new Account()
                {
                    UserName = txtUserName.Text,
                    PassWord = txtPassword.Text,
                    DisplayName = txtDisplayName.Text,
                    Type = 0
                };
                accountService.SaveAccount(account);
                Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateAccount_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                AccountService accountService = new AccountService();
                string userName = txtUserName.Text;
                var account = accountService.GetAccountByName(userName);
                if (account != null)
                {
                    account.DisplayName = txtDisplayName.Text;
                    account.UserName = userName;
                    account.PassWord = txtPassword.Text;
                    account.Type = txtRole.Text == "Staff" ? 0 : 1;
                    accountService.UpdateAccount(account);
                    MessageBox.Show("Successfully update the account");
                }
                Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteAccount_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                AccountService accountService = new AccountService();
                string userName = txtUserName.Text;
                var account = accountService.GetAccountByName(userName);
                if (account != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure want to delete this?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                    if (account.Type == 1)
                    {
                        MessageBox.Show("Cannot delete the Admin account!");
                        return;
                    }
                    accountService.DeleteAccount(account);
                    MessageBox.Show("Successfully Delete");
                }
                Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchAccount_Clicked(object sender, RoutedEventArgs e)
        {
            string input = txtSearchAccount.Text;
            if (input.Trim().Length == 0)
            {
                MessageBox.Show("Please input displayName to search!");
                return;
            }
            AccountService accountService = new AccountService();
            List<Account> accounts = accountService.SearchAccount(input);
            if ((accounts.Count) == 0)
            {
                MessageBox.Show("No account found with display name = " + input);
                return;
            }
            dgAccount.ItemsSource = null;
            dgAccount.ItemsSource = accounts;
        }

        private void dgAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid.SelectedItem == null) return;
                Account selectedAccount = dataGrid.SelectedItem as Account;
                if (selectedAccount == null) return;
                txtDisplayName.Text = selectedAccount.DisplayName;
                txtPassword.Text = selectedAccount.PassWord;
                txtUserName.Text = selectedAccount.UserName;
                txtRole.Text = selectedAccount.Type == 0 ? "Staff" : "Admin";
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        //End Manage Account Tab

        //Start Manage Food Tab
        private void ClearFood()
        {
            LoadFood();
            txtSearchFood.Text = string.Empty;
            txtFoodId.Text = string.Empty;
            txtFoodName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            cbCategory.SelectedIndex = 0;
        }

        private void LoadFood()
        {
            try
            {
                FoodService foodService = new FoodService();
                var foodList = foodService.GetFoods();
                dgFood.ItemsSource = null;
                dgFood.ItemsSource = foodList;

                FoodCategoryService foodCategoryService = new FoodCategoryService();
                var cateList = foodCategoryService.GetFoodCategories();
                cbCategory.ItemsSource = cateList;
                cbCategory.DisplayMemberPath = "Name";
                cbCategory.SelectedValuePath = "Id";
                cbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fail to load Food!");
            }
        }

        private void btnRefreshFood_Clicked(object sender, RoutedEventArgs e)
        {
            ClearFood();
        }

        private void btnAddFood_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                FoodService foodService = new FoodService();
                int.TryParse(txtPrice.Text, out var price);
                Food food = new Food()
                {
                    Name = txtFoodName.Text,
                    IdCategory = int.Parse(cbCategory.SelectedValue.ToString()),
                    Price = price,
                };
                foodService.SaveFood(food);
                ClearFood();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdateFood_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                FoodService foodService = new FoodService();
                int id = int.Parse(txtFoodId.Text);
                int.TryParse(txtPrice.Text.ToString(), out var price);
                var food = foodService.GetFoodById(id);
                if (food != null)
                {
                    food.Name = txtFoodName.Text;
                    food.IdCategory = int.Parse(cbCategory.SelectedValue.ToString());
                    food.Price = price;
                    MessageBox.Show("Successfully update the food");
                }
                ClearFood();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteFood_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                FoodService foodService = new FoodService();
                int id = int.Parse(txtFoodId.Text);
                var food = foodService.GetFoodById(id);
                if (food != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure want to delete this?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }

                    foodService.DeleteFood(food);
                    MessageBox.Show("Successfully Delete");
                }
                ClearFood();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchFood_Clicked(object sender, RoutedEventArgs e)
        {
            string input = txtSearchFood.Text;
            if (input.Trim().Length == 0)
            {
                MessageBox.Show("Please input FoodName to search!");
                return;
            }
            FoodService foodService = new FoodService();
            List<Food> foods = foodService.GetFoodsByName(input);
            if ((foods.Count) == 0)
            {
                MessageBox.Show("No food found with display name = " + input);
                return;
            }
            dgFood.ItemsSource = null;
            dgFood.ItemsSource = foods;
        }

        private void dgFood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid.SelectedItem == null) return;
                Food selectedFood = dataGrid.SelectedItem as Food;
                if (selectedFood == null) return;
                txtFoodId.Text = selectedFood.Id.ToString();
                txtFoodName.Text = selectedFood.Name;
                txtPrice.Text = selectedFood.Price.ToString();
                cbCategory.SelectedValue = selectedFood.IdCategory;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        //End Manage Food Tab

        //Start Manage Bill Tab


        private void ClearBill()
        {
            LoadBill();
            dgBillInfo.ItemsSource = null;
            start_date.Text = string.Empty;
            end_date.Text = string.Empty;
        }
        private void btnRefreshBill_Clicked(object sender, RoutedEventArgs e)
        {
            ClearBill();
        }

        private void LoadBill()
        {
            try
            {
                BillService billService = new BillService();
                var billList = billService.GetBillList();
                dgBill.ItemsSource = null;
                dgBill.ItemsSource = billList;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void LoadBillInfo(Bill selectedBill)
        {
            try
            {
                HolaFastFoodContext context = new HolaFastFoodContext();
                var billInfo = context.BillInfos.Where(c => c.IdBill == selectedBill.Id).Include(b => b.IdFoodNavigation).Select(b => new
                {

                    IdFoodNavigation = b.IdFoodNavigation,
                    Count = b.Count,
                    Amount = b.Count * b.IdFoodNavigation.Price
                }).ToList();
                dgBillInfo.ItemsSource = billInfo;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void dgBill_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dataGrid = sender as DataGrid;
                if (dataGrid.SelectedItem == null) return;
                Bill selectedBill = dataGrid.SelectedItem as Bill;
                if (selectedBill == null) return;
                LoadBillInfo(selectedBill);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearchBill_Clicked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (start_date == null || end_date == null)
                {
                    MessageBox.Show("Must pick checkin time and checkout time to search");
                    return;
                }
                DateTime.TryParse(start_date.Text.ToString(), out DateTime start);
                DateTime.TryParse(end_date.Text.ToString(), out DateTime end);
                BillService billService = new BillService();
                List<Bill> foundBills = billService.GetBillListInRange(start, end);
                if (foundBills != null)
                {
                    dgBill.ItemsSource = null;
                    dgBill.ItemsSource = foundBills;
                }
                else MessageBox.Show("No bill found from " + start_date.Text.ToString() + " till " + end_date.Text.ToString());
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void pckInput_Keyup(object sender, System.Windows.Input.KeyEventArgs e)
        {
            start_date.Text = string.Empty;
        }

        private void pckInputEnd_Keyup(object sender, System.Windows.Input.KeyEventArgs e)
        {
            end_date.Text = string.Empty;
        }


        //End Manage Bill Tab

        //Start Report Tab
        private void LoadYears()
        {
            for (int year = DateTime.Now.Year; year >= 2000; year--)
            {
                cbSelectYear.Items.Add(new ComboBoxItem { Content = year.ToString(), Tag = year });
            }
            cbSelectYear.SelectedIndex = 0;
        }

        private void LoadRevenueData(DateTime selectedDate)
        {
            BillService billService = new BillService();
            var bills = billService.GetBillsByDate(selectedDate);
            var revenueData = bills.GroupBy(b => b.DateCheckIn.Date)
                                   .Select(g => new
                                   {
                                       Date = g.Key,
                                       TotalRevenue = g.Sum(b => b.TotalPrice)
                                   })
                                   .OrderBy(r => r.Date)
                                   .ToList();

            var dates = revenueData.Select(r => r.Date.ToString("MM/dd/yyyy")).ToArray();
            var revenues = revenueData.Select(r => (double)r.TotalRevenue).ToArray();

            revenueChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = new ChartValues<double>(revenues),
                    DataLabels = true,
                    LabelPoint = point => point.Y.ToString("N3")
                }
            };
            revenueChart.AxisX[0].Labels = dates;
        }

        private void LoadRevenueData(int month, int year)
        {
            BillService billService = new BillService();
            var bills = billService.GetBillsByMonth(month, year);
            var revenueData = bills.GroupBy(b => b.DateCheckIn.Date)
                                   .Select(g => new
                                   {
                                       Date = g.Key,
                                       TotalRevenue = g.Sum(b => b.TotalPrice)
                                   })
                                   .OrderBy(r => r.Date)
                                   .ToList();

            var dates = revenueData.Select(r => r.Date.ToString("MM/dd/yyyy")).ToArray();
            var revenues = revenueData.Select(r => (double)r.TotalRevenue).ToArray();

            revenueChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Revenue",
                    Values = new ChartValues<double>(revenues),
                    DataLabels = true,
                    LabelPoint = point => point.Y.ToString("N3")
                }
            };
            revenueChart.AxisX[0].Labels = dates;
        }

        private void DpSelectDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpSelectDate.SelectedDate.HasValue)
            {
                LoadRevenueData(dpSelectDate.SelectedDate.Value);
            }
        }

        private void CbSelectMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectMonth.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Tag.ToString(), out int month))
            {
                if (cbSelectYear.SelectedItem is ComboBoxItem yearItem && int.TryParse(yearItem.Tag.ToString(), out int year))
                {
                    LoadRevenueData(month, year);
                }
            }
        }

        private void CbSelectYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSelectMonth.SelectedItem is ComboBoxItem selectedItem && int.TryParse(selectedItem.Tag.ToString(), out int month))
            {
                if (cbSelectYear.SelectedItem is ComboBoxItem yearItem && int.TryParse(yearItem.Tag.ToString(), out int year))
                {
                    LoadRevenueData(month, year);
                }
            }
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            dpSelectDate.SelectedDate = null;
            cbSelectMonth.SelectedIndex = -1;
            cbSelectYear.SelectedIndex = 0;
            revenueChart.Series.Clear();
        }

        //End Report Tab



        private void btnLogout_Clicked(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
            this.Close();
        }

        private void btnBack_Clicked(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }


    }
}
