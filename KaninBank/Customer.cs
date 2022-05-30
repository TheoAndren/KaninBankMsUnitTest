using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SUT_Bank21Ver2
{
    class Customer : User
    {

        public Customer(int id, string email, string password, string firstname, string lastname, bool isadmin) //Constructor
        {
            Id = id;
            Email = email;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            IsAdmin = isadmin;
        }
        public Customer() //Constructor withoutvalues
        {
        }
        public void CustomerMenu(int id, List<User> userList, List<Account> accountList)
        {
            //MENU SCREEN
            UpdateThread();
            bool logout = false;
            while (logout == false)
            {

                Header();
                Console.WriteLine("1.Kontoöversikt\n2.Öppna nytt konto\n3.Överföring mellan egna konton\n4.Överföring till annan användare\n5.Låna Pengar\n6.Öppna Sparkonto\n7.Växla användare\n8.Avsluta");
                string menuoption = Console.ReadLine();
                switch (menuoption)
                {
                    case "1":                                   //Display logged in users' accounts
                        {
                            AccountDisplayBalances(id, accountList);
                            break;
                        }
                    case "2":                                   //Open new account for logged in user
                        {
                            Console.Clear();
                            CustomerAddAccount(id, accountList);
                            break;
                        }
                    case "3":                                   //Transfer money within same user
                        {
                            AccountTransferOneUser(id, accountList);
                            break;
                        }
                    case "4":                                   //Transfer money to diffrent users
                        {
                            Console.Clear();
                            accountList = AccountTransferBetweenUser(id, userList, accountList);
                            break;
                        }
                    case "5":                                   //Borrow money
                        {
                            Console.Clear();
                            AccountLoan(id, accountList);
                            break;
                        }
                    case "6":                                   //Logout
                        {
                            Console.Clear();
                            CustomerAddSavingAcc(id, accountList);
                            break;

                        }
                    case "7":
                        {
                            Console.Clear();
                            Login(userList, accountList);
                            break;
                        }
                    case "8":
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            Console.Clear();
                            Console.WriteLine("Ogiltigt val.\nSkriv en siffra i menyn.\n\n");
                            continue;
                        }
                }
            }
        }
        public List<Account> AccountLoan(int id, List<Account> accountList)
        {
            List<decimal> balances = accountList[id - 2].Balances;
            List<String> accounts = accountList[id - 2].Accounts;
            balances.Aggregate((x, y) => x + y);
            decimal assettotals = balances.Aggregate(0m, (current, next) => current + next);

            Console.Clear();
            Console.WriteLine("Välkommen till låneavdelningen, skriv in summan som du önskar låna.");
            decimal loanamount = decimal.Parse(Console.ReadLine());
            decimal interest = loanamount * 1.15m - loanamount;
            Console.Clear();
            Console.WriteLine("Summan som du önskar låna är: {0}kr. Den totala räntan för ditt lån {1}kr. ", loanamount, interest);
            bool check = true;
            int transfer = 0;
            decimal maxloan = assettotals * 5;



            if (maxloan >= loanamount)
            {
                Console.Write("Vill du acceptera lånet? [JA/NEJ]");
                Console.WriteLine("");
                string input = Console.ReadLine();

                if (input.ToUpper() == "JA")
                {
                    while (check)
                    {
                        Console.Clear();
                        Console.WriteLine("Vilket konto vill du föra över pengarna till? ");
                        for (int i = 0; i < accounts.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {accounts[i]}\t{balances[i]}");
                        }
                        transfer = int.Parse(Console.ReadLine());
                        if (transfer > accounts.Count || transfer == 0)
                            Console.WriteLine("Felaktigt val.");
                        else
                            check = false;

                        Console.Clear();
                        balances[transfer - 1] += loanamount;
                        Console.WriteLine($"{loanamount}kr överfört. Din nya balans = {balances[transfer - 1]}");
                        Console.WriteLine("\nTryck Enter för att komma tillbaka till menyn.");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
                else if (input.ToUpper() == "NEJ")
                {
                    Console.Clear();
                    Console.WriteLine("Om du har några andra önskemål så kontakta oss direkt på 072542324 så hjälper vi dig. Ha en bra dag! ");
                    Console.WriteLine("Tryck Enter för att komma tillbaka till Menyn ");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Lån godkännes EJ. Försök igen med en mindre summa. ");
                Console.ReadKey();
            }
            return accountList;
        }


        public void AccountDisplayBalances(int id, List<Account> accountList)
        {
            Console.Clear();
            List<string> accounts = accountList[id - 2].Accounts;
            List<decimal> balances = accountList[id - 2].Balances;

            for (int i = 0; i < accounts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {accounts[i]}\t{balances[i]}kr");
            }
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey();
        }
        public void AccountTransferOneUser(int id, List<Account> accountList)
        {
            List<string> accounts = accountList[id - 2].Accounts;
            List<decimal> balances = accountList[id - 2].Balances;
            bool check = true;
            int transferFrom = 0,
                transferTo = 0,
                amount = 0;
            while (check)
            {
                Console.Clear();
                Console.WriteLine("Vilket konto vill du föra över FRÅN? Välj ett konto från listan.");
                for (int i = 0; i < accounts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {accounts[i]}\t{balances[i]}");
                }
                transferFrom = int.Parse(Console.ReadLine());
                if (transferFrom > accounts.Count || transferFrom == 0)
                    Console.WriteLine("Felaktigt val.");
                else
                    check = false;
            }
            check = true;
            while (check)
            {
                Console.WriteLine("Vilket konto vill du föra över TILL?");
                for (int i = 0; i < accounts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {accounts[i]}\t{balances[i]}");
                }
                transferTo = int.Parse(Console.ReadLine());
                if (transferTo > accounts.Count || transferTo == 0)
                    Console.WriteLine("Felaktigt val.");
                else
                    check = false;
            }
            check = true;
            while (check)
            {
                Console.WriteLine("Hur mycket vill du föra över?");
                amount = int.Parse(Console.ReadLine());
                if (amount > balances[transferFrom - 1])
                    Console.WriteLine("Täckning saknas, försök med ett mindre belopp.");
                else if (amount == 0)
                    Console.WriteLine("0 är inte ett giltigt tal.");
                else
                    check = false;
            }

            balances[transferFrom - 1] -= amount;
            balances[transferTo - 1] += amount;
            Console.WriteLine($"{amount}kr överfört från {accounts[transferFrom - 1]} till {accounts[transferTo - 1]}.");
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey();
            Console.Clear();
        }
        public List<Account> AccountTransferBetweenUser(int id, List<User> userlist, List<Account> accountList)
        {
            //TBD
            Console.WriteLine("Function not yet integrated. Click enter to return to the menu");
            Console.ReadKey();
            return accountList;
        }
        public void AccountExhangerate()
        {
            //TBD
            Console.WriteLine("Function not yet integrated. Click enter to return to the menu");
            Console.ReadKey();

            //double exhangerateC = 0.098;
            //double exhangerateE = 10.23;

            //int choice;
            //double crowns;
            //double Euro;
            //Console.WriteLine("Enter your Choice :\n 1- Crowns to Euro \n 2- Euro to Crowns ");
            //choice = int.Parse(Console.ReadLine());

            //switch (choice)
            //{
            //    case 1:

            //        Console.Write("Enter the Crowns Amount :");
            //        crowns = Double.Parse(Console.ReadLine());
            //        Euro = crowns * exhangerateC;
            //        Console.WriteLine("{0} Crowns Equals {1} Euro ", crowns, Euro);

            //        break;


            //    case 2:
            //        Console.Write("Enter the Euro Amount :");
            //        Euro = Double.Parse(Console.ReadLine());
            //        crowns = Euro * exhangerateE;
            //        Console.WriteLine("{0} Euro Equals {1} Crowns", Euro, crowns);
            //        break;

            //}
        }

        public void CustomerAddAccount(int id, List<Account> accountList)
        {
            Console.Clear();
            List<string> accounts = accountList[id - 2].Accounts;
            List<decimal> balances = accountList[id - 2].Balances;
            Console.WriteLine("Skriv in namnet på ditt konto:");
            string accname = Console.ReadLine();
            string first = accname.Substring(0, 1).ToUpper();
            string rest = accname.Substring(1);
            accname = first + rest;
            accounts.Add(accname);
            balances.Add(0);
            Console.Clear();
            Console.WriteLine($"{accname} startat med 0kr.");
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey();
        }

        public void CustomerAddSavingAcc(int id, List<Account> accountList)
        {
            Console.Clear();
            List<string> accounts = accountList[id - 2].Accounts;
            List<decimal> balances = accountList[id - 2].Balances;
            Console.WriteLine("Skriv in namnet på ditt Sparkonto:");
            string accname = Console.ReadLine();
            string first = accname.Substring(0, 1).ToUpper();
            string rest = accname.Substring(1);
            accname = first + rest;
            accounts.Add(accname);
            Console.WriteLine("Hur mycket vill du sätta in på kontot? ");
            decimal savingsamount = decimal.Parse(Console.ReadLine());
            balances.Add(savingsamount);
            decimal interest = savingsamount * 1.05m - savingsamount;
            Console.Clear();
            Console.WriteLine("{0} startat med {1}kr", accname, savingsamount);
            Console.WriteLine("Din ränta efter ett år blir {0}", interest);
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");
            Console.ReadKey();
        }
    }
}