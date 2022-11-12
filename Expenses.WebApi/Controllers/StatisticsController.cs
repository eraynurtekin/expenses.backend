using Expenses.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsServices statisticsService;

        public StatisticsController(IStatisticsServices statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        public IActionResult GetExpenseAmountPerCategory()
        {
            var response = statisticsService.GetExpenseAmountPerCategory();
            return Ok(response);
        }
    }
}
