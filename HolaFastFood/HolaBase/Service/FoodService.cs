using HolaBase.Models;

namespace HolaBase.Service
{
    public class FoodService
    {
        public HolaFastFoodContext con;
        public FoodService()
        {
            con = new HolaFastFoodContext();
        }
        public List<Food> GetFoods()
        {
            try
            {
                var list = con.Foods.ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Food> GetFoodByCategory(int category)
        {
            try
            {
                var list = con.Foods.Where(c => c.IdCategory == category).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void SaveFood(Food food)
        {
            try
            {
                con.Foods.Add(food);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateFood(Food food)
        {
            try
            {
                con.Entry<Food>(food).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteFood(Food food)
        {
            try
            {
                var foodFound = con.Foods.FirstOrDefault(f => f.Id == food.Id);
                con.Foods.Remove(foodFound);
                con.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public Food GetFoodById(int id)
        {
            try
            {
                var food = con.Foods.Find(id);
                return food;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public List<Food> GetFoodsByName(string name)
        {
            try
            {
                var list = con.Foods.Where(f => f.Name.ToLower().Contains(name.ToLower())).ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
