using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SUT_Bank21Ver2
{
    public class Account
    {
        public int Id { get; set; }
        public List<string> Accounts { get; set; }
        public List<decimal> Balances { get; set; }

        public Account(int id, List<string> accounts, List<decimal> balances)
        {
            Id = id;
            Accounts = accounts;
            Balances = balances;
        }
    }
}