using Expenses.Core;
using Expenses.Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesServices expensesServices;
        public ExpensesController(IExpensesServices expensesServices)
        {
            this.expensesServices = expensesServices;
        }

        [HttpGet]
        public IActionResult GetExpenses()
        {
            var expenses = expensesServices.GetExpenses();
            return Ok(expenses);
        }
        [HttpGet("{id}", Name = "GetExpense")]
        public IActionResult GetExpense(int id)
        {
            var expense = expensesServices.GetExpense(id);
            return Ok(expense);
        }
        [HttpPost]
        public IActionResult CreateExpense(DB.Expense expense)
        {
            var newExpense = expensesServices.CreateExpense(expense);
            return CreatedAtRoute("GetExpense", new { newExpense.Id }, newExpense);
        }
        [HttpDelete]
        public IActionResult DeleteExpense(Expense expense)
        {
            expensesServices.DeleteExpense(expense);
            return Ok();
        }
        [HttpPut]
        public IActionResult EditExpense(Expense expense)
        {
            var editingExpense = expensesServices.EditExpense(expense);
            return Ok(editingExpense);
        }

    }
}
