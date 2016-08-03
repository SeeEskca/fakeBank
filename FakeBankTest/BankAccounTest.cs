using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeBank;
using System.Data;
using BankDB;

namespace FakeBankTest
{
    [TestClass]
    public class BankAccounTest
    {
        // [TestMethod]
        //public void TestMethod1()
        //{
        //}

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        [DataSource(@"Provider=Microsoft.SqlServerCe.Client.4.0; Data source=(localdb)\SQLEXPRESS; integrated security=true; database=MyBank", "TestAddition")]
        public void TestDataSourceValue()
        {
            BankDB.Math target = new BankDB.Math();
            int x = Convert.ToInt32(TestContext.DataRow["XValue"]);
            int y = Convert.ToInt32(TestContext.DataRow["YValue"]);
            int expected = Convert.ToInt32(TestContext.DataRow["Sum"]);

            int actual = target.addIntegers(x, y);
            Assert.AreEqual(expected, actual, "x:<{0}> y:<{1}>", new object[] { x, y });
        }
        [TestMethod] 
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            account.Debit(debitAmount);

            // assert is handled by ExpectedException
        }

        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsGreaterThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange
            double beginningBalance = 11.99;
            double debitAmount = 34.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("No exception was thrown");
            // assert is handled by ExpectedException
        }

    }
}
