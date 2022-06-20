using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace KaninBank
{

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool IsAdmin { get; set; }
        public decimal Balance { get; set; }

        public string UpdateNotice { get; set; }  //Field with updated time

        public void Login(List<User> userList, List<Account> accountList)
        {
            Customer CustomerObj = new Customer(); //Objects with PH values
            Admin AdminObj = new Admin();   //Objects with PH values
            int loginAtempt = 3;
            List<string> loginName = new List<string>(); //Adds the login-name to a list to check for banned logins

            while (loginAtempt > 0)
            {
                Console.WriteLine("Logga in med email: ");
                string loginTest = Console.ReadLine().ToLower();
                Console.WriteLine("Lösenord: ");
                string passwordTest = Console.ReadLine();

                //Check if banned
                loginName.Add(loginTest);
                int SameUserLoginCount = 0;
                foreach (string s in loginName)
                {
                    if (s == loginTest)
                        SameUserLoginCount++;
                }
                if (SameUserLoginCount > 3)
                {
                    Console.WriteLine("För många misslyckade försök. Användarnamnet är nu bannlyst. Kontakta Admin för mer info: admin@admin.admin");
                }
                else
                {
                    foreach (User templist in userList)
                    {
                        if (templist.Email == loginTest && templist.Password == passwordTest)
                        {
                            bool adminCheck = templist.IsAdmin;
                            if (adminCheck == true)
                            {
                                loginAtempt = 0;

                                AdminObj.AdminMenu(userList, accountList);
                            }
                            else
                            {
                                loginAtempt = 0;
                                int id = templist.Id;
                                CustomerObj.CustomerMenu(id, userList, accountList);
                            }
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Fel användarnamn eller lösenord, försök igen.");
                }
            }
        }
        //Methods to start the 15min update on transfers
        public void UpdateThread()          //Start new thread and UpdateTransferTImer()
        {
            Thread UpdateThread = new Thread(
        () => UpdateTransferTimer()
    );
            UpdateThread.Start();
        }

        public void UpdateTransferTimer()
        {
            UpdateNotice = "No transfer yet";
            while (true)
            {
                DateTime currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                DateTime update0 = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, 00, 00);
                DateTime update15 = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, 15, 00);
                DateTime update30 = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, 30, 00);
                DateTime update45 = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour, 45, 00);
                if (currentDateTime.Equals(update0) || currentDateTime.Equals(update15) || currentDateTime.Equals(update30) || currentDateTime.Equals(update45))
                {
                    UpdateNotice = DateTime.Now.ToString("yyyy-MM-dd HH:mm");     //Function to update transfers every 15min will be here              
                }
            }
        }
        public void CheckTransfer()                 //Needs to be expanded: Transfer History, Time untill next transfer and more
        {
            Console.WriteLine("Last transfer: {0}", UpdateNotice);
            Console.ReadKey();
            Console.Clear();
        }
        public void Header()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Net.WebClient wc = new System.Net.WebClient();
            string logo = wc.DownloadString("https://raw.githubusercontent.com/sigreyo/SUT21-Bank/master/logo.txt");
            string[] logoarr = logo.Split("\n");
            foreach (var item in logoarr)
            {
                Console.WriteLine(item);
            }
            Console.ResetColor();
        }
    }
}