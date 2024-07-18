using HolaBase.Models;

namespace HolaBase.Service
{
    public class AccountService
    {
        public HolaFastFoodContext context;
        public AccountService()
        {
            context = new HolaFastFoodContext();
        }

        public List<Account> GetAllAccounts()
        {
            try
            {
                var list = context.Accounts.ToList();
                return list;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Account GetAccountByName(string userName)
        {
            try
            {
                var acc = context.Accounts.Find(userName);
                return acc;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void SaveAccount(Account account)
        {
            try
            {
                context.Accounts.Add(account);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void UpdateAccount(Account account)
        {
            try
            {
                context.Entry<Account>(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void DeleteAccount(Account account)
        {
            try
            {
                var acc = context.Accounts.FirstOrDefault(a => a.UserName.Equals(account.UserName));
                context.Accounts.Remove(acc);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Account> SearchAccount(string input)
        {
            try
            {
                var acc = context.Accounts.Where(a => a.DisplayName.ToLower().Contains(input.ToLower())).ToList();
                return acc;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Account Login(string username, string password)
        {
            List<Account> accounts = GetAllAccounts();
            Account? account = accounts.FirstOrDefault(c => (c.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && c.PassWord.Equals(password)));
            return account;
        }
    }
}
