using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Expenses.DB
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }    
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
