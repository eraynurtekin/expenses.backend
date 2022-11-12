using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Core.DTO
{
    public class AuthenticatedUser
    {
        public string Token { get; set; }
        public string Username { get; set; }
    }
}
