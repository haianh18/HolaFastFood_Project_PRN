using HolaBase.Models;

namespace HolaBase.Service
{
    public class TableFoodService
    {
        public HolaFastFoodContext context;
        public TableFoodService()
        {
            context = new HolaFastFoodContext();
        }

        public List<TableFood> GetTableFoods()
        {
            try
            {
                var list = context.TableFoods.ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public TableFood GetTableFood(int id)
        {
            try
            {
                var table = context.TableFoods.Find(id);
                return table;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SaveTable(TableFood table)
        {
            try
            {
                context.TableFoods.Add(table);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteTable(TableFood table)
        {
            try
            {
                var tableFound = context.TableFoods.SingleOrDefault(c => c.Id == table.Id);
                if (!tableFound.Status.Equals("Empty"))
                {
                    throw new Exception("Table is being occupied at the moment! Cannot delete!");
                }
                context.TableFoods.Remove(tableFound);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdateTable(TableFood table)
        {
            try
            {
                context.Entry<TableFood>(table).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
