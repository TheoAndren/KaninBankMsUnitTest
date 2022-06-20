using Microsoft.VisualStudio.TestTools.UnitTesting;
using KaninBank;
using System;
using System.Collections.Generic;
using Shouldly;

namespace KaninBank.Test
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]

        public void TestIfRightAmountOfMoneyIsWithdrawn()
        {
            // Using Dans account with id 2 and id 0 in accountlist. 100kr id 0, 200kr id 1. 2 accounts
            

            //Arrange
            var accountList = Program.CreateAccountList();
            int id = 0;  // userid
            int withdrawFrom = 0; //Withdraw from account id 0 
            decimal withdrawAmount = 100m; // Withdraw 100  // Change to 150m or more to check so the
                                           // so the customer cant withdraw more than is avaible.
            Customer customer = new Customer();

            //Act

            customer.Withdraw(id, accountList, withdrawFrom, withdrawAmount);

            var actual1 = accountList[id].Balances[withdrawFrom];

            var expected1 = 0m;    //100kr - 100kr should now be 0.



            //Assert

            Assert.AreEqual(actual1, expected1);



            //Shouldy



        }

        [TestMethod]
        public void TestIfTransfersCorrect()
        {
            //Kollar om rätt summa tas och ges till rätt konto efter överföring. 
            // Kolla så att konto inte kan gå minus

            // Using Dans account with id 2 and id 0 in accountlist. 100kr id 0, 200kr id 1. 2 accounts


            //Arrange
            var accountList = Program.CreateAccountList();
            int id = 0;  // userid
            int transferFrom = 0, transferTo = 1;  // send from salary to savings
            decimal amount = 100m;   //Amount that is being transfered // Change to 150m to see if more money than is avaible
                                     // be withdrawn from the account.
            Customer customer = new Customer();


            //Act

            customer.AccountTransferOneUser(id, accountList, transferFrom, transferTo, amount); //Run the transfer method.
            var actual1 = accountList[id].Balances[transferFrom];
            var actual2 = accountList[id].Balances[transferTo];

            var expected1 = 0m;    //100kr - 100kr account should be left with 0.
            var expected2 = 300m;  //200kr + 100kr so should have increased to 300.

            //Assert
            Assert.AreEqual(actual1, expected1);
            Assert.AreEqual(actual2, expected2);

            //Shouldy
            actual1.ShouldBe(expected1);
            actual2.ShouldBe(expected2);


        }

        [TestMethod]

        public void TestIfLoanIsCorrectAmount()
        {

            //arrange
            List<Account> accountList = Program.CreateAccountList();
            int id = 3;
            decimal loan = -500m;
            Customer customer = new Customer();

            //act
            var actual = customer.AccountLoan(id, accountList, loan);
            var expected = -500m;      

            //assert
            Assert.AreEqual(actual, expected);


            //Shouldy
            actual.ShouldBeLessThanOrEqualTo(expected);



        }
        
    }
}
