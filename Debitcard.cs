using System;

namespace MultipleCreditCardManagement{
    /// <summary>
    /// This is debit card class that has its own two attributes and inherited attributes and methods from Card class.
    /// </summary>
    public class DebitCard : Card
    {
        private double _balance;
        private double _transactionLimit;
        /// <summary>
        ///  Parameterised constructor to set card number, card holder name, bank name, expiry date, balance, transaction limit.
        /// </summary>
        public DebitCard(string cardNumber, string cardHolderName, string bankName, DateTime expiryDate, double balance, double transactionLimit) : base(cardNumber, cardHolderName, bankName, expiryDate)
        {
            _balance = balance;
            _transactionLimit = transactionLimit;
        }
        /// <summary>
        /// Property to get the balance of the debit card.
        /// </summary>
        public double Balance
        {
            get { return _balance; }
        }
        /// <summary>
        /// Property to get and set the transaction limit of the debit card.
        /// </summary>
        public double TransactionLimit
        {
            get { return _transactionLimit; }
            set { _transactionLimit = value; }
        }
        /// <summary>
        /// Public override method to display the details of the debit card.
        /// </summary>
        public override void DisplayDetails(){
            Console.WriteLine("Debit Card Details:");
            Console.WriteLine("Card Number: " + base.CardNumber);
            Console.WriteLine("Card Holder Name: " + base.CardHolderName);
            Console.WriteLine("Bank Name: " + base.BankName);
            Console.WriteLine("Expiry Date: " + base.ExpiryDate.ToShortDateString());
            Console.WriteLine("Balance: " + _balance.ToString());
            Console.WriteLine("Transaction Limit: " + _transactionLimit.ToString());
        }
        /// <summary>
        ///  Public method return boolean outcome to check remaining balance before a transaction and has the parameter to accept the amount and transaction type.
        /// </summary>
        public bool PreCheckRemainBalance(double amount,TransactionType transactionType){
            if(transactionType == TransactionType.Expense || transactionType == TransactionType.Transfer){
                if (amount <= _balance && amount <= _transactionLimit){
                    Console.WriteLine("Transaction successful. Remaining balance will be: " + (_balance - amount).ToString());
                    return true;
                }
                else{
                    Console.WriteLine("Transaction failed. Insufficient balance or exceeds transaction limit.");
                    return false;
                }
            }
            else if(transactionType == TransactionType.Income){
                Console.WriteLine("Transaction successful. New balance will be: " + (_balance + amount).ToString());
                return true;
            }
            else{
                Console.WriteLine("Invalid transaction type.");
                return false;
            }
        }
        /// <summary>
        /// Public method to calculate the remaining balance after all transactions are completed.
        /// </summary>
        public void CalculateRemainBalance(){
            foreach(Transaction transaction in Transactions){
                if((transaction.TransactionType == TransactionType.Expense || transaction.TransactionType == TransactionType.Transfer) && transaction.TransactionStatus == TransactionStatus.Completed){
                    _balance -= transaction.Amount;
                }
                else if(transaction.TransactionType == TransactionType.Income && transaction.TransactionStatus == TransactionStatus.Completed){
                    _balance += transaction.Amount;
                }
            }
        }
    }
}