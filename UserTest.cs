using System;
using NUnit.Framework;

namespace MultipleCreditCardManagement{
    
    [TestFixture]
    public class UserTest{
        [Test]
        public void TestAddCard(){
            //Setup
            User user = new User("Andrew","Andrew","abc050801");
            CreditCard creditCard = new CreditCard("4321 8765 2109 6543","Andrew","CIMB",new DateTime(15/06/2026),20000,15,500);
            DebitCard debitCard = new DebitCard("1234 5678 9012 3456","Andrew","Maybank",new DateTime(15/06/2025),10000,5000);
            //Check
            Assert.AreEqual(0,user.Cards.Count);
            //Execute
            user.AddCard(creditCard);
            //Check Again 
            Assert.AreEqual(1,user.Cards.Count);
            Assert.AreEqual(creditCard,user.Cards[0]);
            //Execute Again
            user.AddCard(debitCard);
            //Check Again
            Assert.AreEqual(2,user.Cards.Count);
            Assert.AreEqual(creditCard,user.Cards[0]);
            Assert.AreEqual(debitCard,user.Cards[1]);
        }
        [Test]
        public void TestRemoveCard(){
            //Setup
            User user = new User("Andrew","Andrew","abc050801");
            CreditCard creditCard = new CreditCard("4321 8765 2109 6543","Andrew","CIMB",new DateTime(15/06/2026),20000,15,500);
            DebitCard debitCard = new DebitCard("1234 5678 9012 3456","Andrew","Maybank",new DateTime(15/06/2025),10000,5000);
            user.AddCard(creditCard);
            user.AddCard(debitCard);
            //Check
            Assert.AreEqual(2,user.Cards.Count);
            Assert.AreEqual(creditCard,user.Cards[0]);
            Assert.AreEqual(debitCard,user.Cards[1]);
            //Execute 
            user.RemoveCard(creditCard.CardNumber);
            //Check Again
            Assert.AreEqual(1,user.Cards.Count);
            Assert.AreEqual(debitCard,user.Cards[0]);
            //Execute Again
            user.RemoveCard(debitCard.CardNumber);
            //Check Again
            Assert.AreEqual(0,user.Cards.Count);
        }
    }
}