using Expenses.Core.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace Expenses.Core
{
    public class ExpensesServices : IExpensesServices
    {
        private DB.AppDbContext appDbContext;
        private readonly DB.User user;
        public ExpensesServices(DB.AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            this.appDbContext = context;
            //Kimin request te bulunduğunu buluyoruz.
            user = appDbContext.Users.First(x=>x.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }

        public Expense CreateExpense(DB.Expense expense)
        {
            expense.User = user; 
            appDbContext.Add(expense);
            appDbContext.SaveChanges();
            return (Expense)expense;
        }

        public void DeleteExpense(Expense expense)
        { 
            var dbExpense = appDbContext.Expenses.First(x=>x.User.Id == user.Id && x.Id == expense.Id);
            appDbContext.Expenses.Remove(dbExpense);  
            appDbContext.SaveChanges();
        }

        public Expense EditExpense(Expense expense)
        {
            var existingExpense = appDbContext.Expenses.First(x => x.User.Id == user.Id && x.Id == expense.Id);
            existingExpense.Description = expense.Description;
            existingExpense.Amount = expense.Amount;
            appDbContext.SaveChanges();
            return expense;
        }
        public Expense GetExpense(int id) => appDbContext.Expenses.Where(x => x.User.Id == user.Id && x.Id == id).Select(x => (Expense)x).First();
        public List<Expense> GetExpenses() => appDbContext.Expenses.Where(_ => _.User.Id == user.Id).Select(x => (Expense)x).ToList();
    }
}
