using System;
using NUnit.Framework;

namespace MultipleCreditCardManagement{
    [TestFixture]
    public class CardTest{
        [Test]
        public void TestAddTransaction(){
            //Setup
            DebitCard debitCard = new DebitCard("1234 5678 9012 3456","Andrew","Maybank",new DateTime(15/06/2025),10000,5000);
            Transaction transaction1 = new Transaction(2000, new DateTime(2023, 10, 1), "Groceries", "1111 2222 3333 4444", "1234 5678 9012 3456", TransactionType.Expense);
            Transaction transaction2 = new Transaction(10000, new DateTime(2023, 10, 2), "Salary", "1234 5678 9012 3456", "5678 1234 8765 4321", TransactionType.Income);
            //Check
            Assert.AreEqual(0,debitCard.Transactions.Count);
            //Execute
            debitCard.AddTransaction(transaction1);
            //Check Again
            Assert.AreEqual(1,debitCard.Transactions.Count);
            Assert.AreEqual(transaction1,debitCard.Transactions[0]);
            //Execute Again
            debitCard.AddTransaction(transaction2);
            //Check Again
            Assert.AreEqual(2,debitCard.Transactions.Count);
            Assert.AreEqual(transaction1,debitCard.Transactions[0]);
            Assert.AreEqual(transaction2,debitCard.Transactions[1]);
        }
        [Test]
        public void TestRemoveTransaction(){
            //Setup
            DebitCard debitCard = new DebitCard("1234 5678 9012 3456","Andrew","Maybank",new DateTime(15/06/2025),10000,5000);
            Transaction transaction1 = new Transaction(2000, new DateTime(2023, 10, 1), "Groceries", "1111 2222 3333 4444", "1234 5678 9012 3456", TransactionType.Expense);
            Transaction transaction2 = new Transaction(10000, new DateTime(2023, 10, 2), "Salary", "1234 5678 9012 3456", "5678 1234 8765 4321", TransactionType.Income);
            debitCard.AddTransaction(transaction1);
            debitCard.AddTransaction(transaction2);
            //Check
            Assert.AreEqual(2,debitCard.Transactions.Count);
            Assert.AreEqual(transaction1,debitCard.Transactions[0]);
            Assert.AreEqual(transaction2,debitCard.Transactions[1]);
            //Execute 
            debitCard.RemoveTransaction(transaction1.TransactionID);
            //Check Again
            Assert.AreEqual(1,debitCard.Transactions.Count);
            Assert.AreEqual(transaction2,debitCard.Transactions[0]);
            //Execute Again
            debitCard.RemoveTransaction(transaction2.TransactionID);
            //Check Again
            Assert.AreEqual(0,debitCard.Transactions.Count);
        }
    }
}