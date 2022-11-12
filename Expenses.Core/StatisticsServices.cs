using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expenses.Core
{
    public class StatisticsServices : IStatisticsServices
    {
        private readonly DB.AppDbContext context;
        private readonly DB.User user;
        public StatisticsServices(DB.AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            user = context.Users.First(u => u.Username == httpContextAccessor.HttpContext.User.Identity.Name);
        }
        public IEnumerable<KeyValuePair<string, double>> GetExpenseAmountPerCategory()
        {
            return context.Expenses.Where(x => x.User.Id == user.Id).AsEnumerable().GroupBy(e => e.Description).ToDictionary(e => e.Key, e => e.Sum(x => x.Amount))
                .Select(x=>new KeyValuePair<string, double>(x.Key,x.Value));
        }
    }
}
