using HolaBase.Models;

namespace HolaBase.Service
{
    public class BillService
    {
        public HolaFastFoodContext con;
        public BillService()
        {
            con = new HolaFastFoodContext();
        }

        public List<Bill> GetBillList()
        {
            try
            {
                var bills = con.Bills.ToList();
                return bills;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Bill> GetBillListByTableId(int tableId)
        {
            try
            {
                var list = con.Bills.Where(c => c.IdTable == tableId).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Bill> GetBillListInRange(DateTime start, DateTime end)
        {
            try
            {

                var list = con.Bills.Where(c => c.DateCheckIn >= start && (c.DateCheckOut == null) ? true : (c.DateCheckOut <= end)).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Bill> GetBillListByStatus(int status)
        {
            try
            {
                var list = con.Bills.Where(c => c.Status == status).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Bill GetBillById(int id)
        {
            try
            {
                var bill = con.Bills.Find(id);
                return bill;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Bill GetBill(int tableId)
        {
            try
            {
                var bill = con.Bills.FirstOrDefault(c => c.IdTable == tableId && c.Status == 0);
                if (bill == null)
                {
                    Console.WriteLine($"No active bill found for TableID: {tableId}");
                }
                return bill;
            }
            catch (Exception ex)
            {
                // Log the detailed exception message and inner exception, if any
                Console.WriteLine($"Error retrieving bill for TableID: {tableId} - {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw new Exception("An error occurred while retrieving the bill.", ex);
            }
        }

        public List<Bill> GetBillsByDate(DateTime date)
        {
            using (var context = new HolaFastFoodContext())
            {
                return context.Bills
                              .Where(b => b.DateCheckIn.Date == date.Date)
                              .ToList();
            }
        }

        public List<Bill> GetBillsByMonth(int month, int year)
        {
            using (var context = new HolaFastFoodContext())
            {
                return context.Bills
                              .Where(b => b.DateCheckIn.Month == month && b.DateCheckIn.Year == year)
                              .ToList();
            }
        }

        public void SaveBill(Bill bill)
        {
            try
            {
                con.Bills.Add(bill);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        public void UpdateBill(Bill bill)
        {
            try
            {
                con.Entry<Bill>(bill).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteBill(Bill bill)
        {
            try
            {
                var result = con.Bills.FirstOrDefault(c => c.Id == bill.Id);
                con.Bills.Remove(result);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

    }
}
