using System;
using System.Collections.Generic;

namespace IssaBankAccount.Models
{
    public class Account
    {
        public int id { get; set; }
        public int UsersId { get; set; }
        public User User { get; set; }
        public int Balance { get; set; }

    }
}