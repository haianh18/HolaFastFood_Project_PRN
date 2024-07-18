using HolaBase.Models;

namespace HolaBase.Service
{
    public class FoodCategoryService
    {
        public HolaFastFoodContext con;
        public FoodCategoryService()
        {
            con = new HolaFastFoodContext();
        }

        public List<FoodCategory> GetFoodCategories()
        {
            try
            {
                var list = con.FoodCategories.ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public FoodCategory GetFoodCategory(int id)
        {
            try
            {
                var cate = con.FoodCategories.Find(id);
                return cate;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
