using HolaBase.Models;

namespace HolaBase.Service
{
    public class BillInfoService
    {
        public HolaFastFoodContext con;
        public BillInfoService()
        {
            con = new HolaFastFoodContext();
        }

        public List<BillInfo> GetBillInfos()
        {
            try
            {
                var billInfos = con.BillInfos.ToList();
                return billInfos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<BillInfo> GetBillInfosByBillID(int billID)
        {
            try
            {
                var billInfos = con.BillInfos.Where(c => c.IdBill == billID).ToList();
                return (billInfos);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public BillInfo GetBillInfoByID(int billInfoId)
        {
            try
            {
                var billInfo = con.BillInfos.Find(billInfoId);
                return billInfo;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void SaveBillInfo(BillInfo billInfo)
        {
            try
            {
                con.BillInfos.Add(billInfo);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateBillInfo(BillInfo billInfo)
        {
            try
            {
                con.Entry<BillInfo>(billInfo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteBillInfo(BillInfo billInfo)
        {
            try
            {
                var billInfoFound = con.BillInfos.FirstOrDefault(c => c.Id == billInfo.Id);
                con.BillInfos.Remove(billInfoFound);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
