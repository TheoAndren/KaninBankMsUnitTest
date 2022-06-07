using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace KaninBank
{
    class Admin : User
    {

        public Admin(int id, string email, string password, string firstname, string lastname, bool isadmin) //Constructor
        {
            Id = id;
            Email = email;
            Password = password;
            Firstname = firstname;
            Lastname = lastname;
            IsAdmin = isadmin;

        }
        public Admin() //Constructor without values
        {
        }
        public void AdminMenu(List<User> userList, List<Account> accountList)
        {
            //MENU SCREEN
            UpdateThread();
            bool logout = false;
            while (logout == false)
            {
                Header();
                Console.WriteLine("1.Skriv ut användare\n2.Lägg till användare\n3.Visa alla konton\n4.Lägg till nytt konto\n5.Transaktioner\n6.Växla användare\n7.Avsluta");
                string menuoption = Console.ReadLine();
                switch (menuoption)
                {
                    case "1":
                        {
                            Console.Clear();
                            //Display all users                           
                            AdminDisplayUsers(userList);  //Display users don't exist
                            break;
                        }
                    case "2":
                        {
                            Console.Clear();
                            //Create new user                           
                            AdminAddUser(userList, accountList);
                            break;
                        }
                    case "3":
                        {
                            Console.Clear();
                            //Display accounts
                            ShowAllAccounts(userList, accountList);
                            break;
                        }
                    case "4":
                        {
                            Console.Clear();
                            //Open new accounts
                            accountList = AdminAddAccount(userList, accountList);
                            break;
                        }
                    case "5": //Check transfers
                        {
                            Console.Clear();
                            CheckTransfer();
                            break;
                        }
                    case "6": //LogOut
                        {
                            Console.Clear();
                            Login(userList, accountList);
                            break;
                        }
                    case "7":
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
        public void AdminAddUser(List<User> userList, List<Account> accountList) //Admin method to add new users
        {
            Console.Clear();
        emailinuse:
            Console.WriteLine("Email?");
            string email = Console.ReadLine();
            foreach (User templist in userList)              //Stops 2 users from using the same email
            {
                if (templist.Email == email)
                {
                    Console.WriteLine("Email används redan av en annan användare. Välj ny.");
                    goto emailinuse;
                }
            }
            Console.WriteLine("Lösenord");
            string password = Console.ReadLine();
            Console.WriteLine("Förnamn?");
            string firstname = Console.ReadLine();
            Console.WriteLine("Efternamn?");
            string lastname = Console.ReadLine();
            Console.WriteLine("Är detta ett admin-konto? (Ja/Nej)");
        notcorrect:
            string isadmininput = Console.ReadLine();
            bool isnewuseradmin = false; //PH
            if (isadmininput.ToUpper() == "JA")
            {
                isnewuseradmin = true;
            }
            else if (isadmininput.ToUpper() == "NEJ")
            {
                isnewuseradmin = false;
            }
            else
            {
                Console.WriteLine("Är detta ett admin konto? Skriv Ja eller Nej.");
                goto notcorrect;
            }
            bool newnumber = false;
            int id = 1;

            while (newnumber == false)
            {

                foreach (User item in userList)
                {
                    id++;
                    if (item.Id != id)
                    {
                        newnumber = true;
                    }
                }

            }
            if (isnewuseradmin == true)
            {
                userList.Add(new Admin(id, email, password, firstname, lastname, isnewuseradmin)); //Add the user to the master list
            }
            else if (isnewuseradmin == false)
            {
                userList.Add(new Customer(id, email, password, firstname, lastname, isnewuseradmin)); //Add the user to the master list
            }

            //List<string> accounts = accountList[id-1].Accounts;
            //List<decimal> balances = accountList[id-1].Balances;
            Console.WriteLine("Skriv in namnet på användarens bank-konto:");
            string accname = Console.ReadLine();
            string first = accname.Substring(0, 1).ToUpper();
            string rest = accname.Substring(1);
            accname = first + rest;

            accountList.Add(new Account(id, new List<string> { accname }, new List<decimal> { 0 }));
            Console.WriteLine($"{accname} startat med 0kr.");
            Console.WriteLine("\nTryck Enter för att återgå till huvudmenyn.");


        }

        public void AdminDisplayUsers(List<User> userList)                 //Display all users
        {
            foreach (User item in userList)
            {
                if (item.IsAdmin == true) { Console.WriteLine("ADMIN ACCOUNT"); }
                else { Console.WriteLine("USER ACCOUNT"); }
                Console.WriteLine("ID: {0}", item.Id);
                Console.WriteLine("Email: {0}", item.Email);
                Console.WriteLine("Password: {0}", item.Password);
                Console.WriteLine("Firstname: {0}", item.Firstname);
                Console.WriteLine("Lastname: {0}", item.Lastname);
                Console.WriteLine("\n");
            }
            Console.ReadKey();
        }


        public void ShowAllAccounts(List<User> userList, List<Account> accountList)
        {
            Console.Clear();
            List<string> names = new List<string>();
            foreach (var item in userList.Where(e => e.Id > 1))
            {
                if (item.IsAdmin == false)
                {
                    names.Add($"ID: {item.Id} Namn: {item.Firstname} {item.Lastname}");
                }
            }
            int id = 0;

            foreach (var item in accountList)
            {
                List<string> accounts = item.Accounts;
                List<decimal> balances = item.Balances;

                Console.WriteLine($"{names[id]}. ");
                for (int j = 0; j < accounts.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {accounts[j]}\t{balances[j]}");
                }
                Console.WriteLine("-----------------");
                id++;
            }

            Console.ReadKey();
        }

        public List<Account> AdminAddAccount(List<User> userList, List<Account> accountlist)
        {
            //TBD
            Console.WriteLine("Function not yet integrated. Click enter to return to the menu");
            Console.ReadKey();
            return accountlist;
        }

    }
}