using HolaBase.Models;
using System.Windows;
using System.Windows.Controls;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for InvoiceWindow.xaml
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        public InvoiceWindow(Bill bill, List<BillInfo> billInfos)
        {
            InitializeComponent();
            LoadInvoiceData(bill, billInfos);
        }
        private void LoadInvoiceData(Bill bill, List<BillInfo> billInfos)
        {
            txtStoreInfo.Text = "Store: HolaFastFood - Hola Park, Hanoi"; // Add your store information here
            txtDateCheckIn.Text = $"Check-in Date: {bill.DateCheckIn}";
            txtDateCheckOut.Text = $"Check-out Date: {bill.DateCheckOut}";
            txtUserName.Text = $"Employee: {bill.UserName}";

            var billInfoData = billInfos.Select(b => new
            {
                IdFoodNavigation = b.IdFoodNavigation,
                Count = b.Count,
                Amount = b.Count * b.IdFoodNavigation.Price
            }).ToList();

            dgBillInfo.ItemsSource = billInfoData;

            double totalAmount = billInfoData.Sum(b => b.Amount);
            txtTotalAmount.Text = $"Total Amount: {totalAmount} VND";
            txtDiscount.Text = $"Discount: {bill.Discount}%";
            txtFinalAmount.Text = $"Final Amount: {bill.TotalPrice} VND";
        }

        // Event handler for Close button
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Event handler for Print button
        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintInvoice();
        }

        // Method to print the invoice
        private void PrintInvoice()
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Invoice");
            }
        }

    }
}
