using System;
using System.Collections.Generic;
using System.Linq;
namespace SUT_Bank21Ver2
{
    public class Program
    {
        static void Main(string[] args)
        {

            User startup = new User();


            List<User> userList = CreateUserList();
            List<Account> accountList = CreateAccountList();

            startup.Login(userList, accountList);

        }
        public static List<User> CreateUserList()
        {
            List<User> userList = new List<User>() {

            new Admin(1,"admin", "adminpw", "Admin", "Adminsson", true),
            new Customer(2, "dan", "danpw", "Dan", "Dansson", false),
            new Customer(3, "theo", "theopw", "Theo", "Theosson", false),
            new Customer(4, "simon", "simonpw", "Simon", "Simonsson", false),
            new Customer(5, "anas", "anaspw", "Anas", "Anasson", false)
        };
            return userList;
        }
        public static List<Account> CreateAccountList()
        {
            List<Account> accList = new List<Account>() {

            new Account(2, new List<string> {"Lönekonto", "Sparkonto" },new List<decimal>{100m,200m}),
            new Account(3, new List<string> {"Barnkonto", "Sparkonto" },new List<decimal>{1000m,3200m}),
            new Account(4, new List<string> {"Lönekonto", "Sparkonto" },new List<decimal>{12545m,1231240m}),
            new Account(5, new List<string> {"Sparkonto", "Barnkonto" },new List<decimal>{1123m,124500m})

        };
            return accList;
        }




    }
}