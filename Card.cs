using System;
using System.Collections.Generic;
using System.Globalization;
#nullable disable

namespace MultipleCreditCardManagement{
    /// <summary>
    /// This is the Card class, which is the base class for all types of cards, it has six attributes.
    /// </summary>
    public abstract class Card{
        private string _cardNumber;
        private string _cardHolderName;
        private string _bankName;
        private DateTime _expiryDate;
        private CardStatus _cardStatus;
        private List<Transaction> _transactions;
        /// <summary>
        /// Parameterised constructor that sets the card number, card holder name, bank name, and expiry date, initialises the card status to Active and creates an empty list of transactions.
        /// </summary>
        public Card(string cardNumber, string cardHolderName, string bankName, DateTime expiryDate){
            _cardNumber = cardNumber;
            _cardHolderName = cardHolderName;
            _bankName = bankName;
            _expiryDate = expiryDate;
            _cardStatus = CardStatus.Active;
            _transactions = new List<Transaction>();
        }
        /// <summary>
        /// Property gets or sets the card number of the card.
        /// </summary>
        public string CardNumber{
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }
        /// <summary>
        /// Property gets or sets the card holder name of the card.
        /// </summary>
        public string CardHolderName{
            get { return _cardHolderName; }
            set { _cardHolderName = value; }
        }
        /// <summary>
        /// Property gets or sets the bank name of the card.
        /// </summary>
        public string BankName{
            get { return _bankName; }
            set { _bankName = value; }
        }
        /// <summary>
        /// This property gets or sets the expiry date of the card.
        /// </summary>
        public DateTime ExpiryDate{
            get { return _expiryDate; }
            set { _expiryDate = value; }
        }
        /// <summary>
        /// This property gets or sets the card status of the card.
        /// </summary>
        public CardStatus CardStatus{
            get { return _cardStatus; }
            set { _cardStatus = value; }
        }
        /// <summary>
        /// This property gets or sets the transactions of the card.
        /// </summary>
        public List<Transaction> Transactions{
            get { return _transactions; }
            set { _transactions = value; }
        }
        /// <summary>
        /// This public method adds a transaction to the card if the card is active and has the parameter to accept the transaction.
        /// </summary>
        public void AddTransaction(Transaction transaction){
            if(_cardStatus == CardStatus.Active){
                _transactions.Add(transaction);
                Console.WriteLine("Transaction with id (" + transaction.TransactionID + ") has been added to card with card number (" + _cardNumber + ").");
            }
            else{
                Console.WriteLine("Transaction cannot be added as the card is " + _cardStatus + ".");
            }
        }
        /// <summary>
        /// This public method removes a transaction from the card if it exists based on its ID (has the parameter to track the transaction ID).
        /// </summary>
        public void RemoveTransaction(string transactionID){
            foreach (Transaction transaction in _transactions){
                if (transaction.TransactionID == transactionID){
                    _transactions.Remove(transaction);
                    Console.WriteLine("Transaction with id (" + transactionID + ") has been removed from card with card number (" + _cardNumber + ").");
                    return;
                }
            }
            Console.WriteLine("Transaction with id (" + transactionID + ") not found in card with card number (" + _cardNumber + ").");
        }
        /// <summary>
        /// This public method allows editing or cancelling a transaction based on its ID (has the parameter to track the transaction ID).
        /// </summary>
        public void EditTransaction(string transactionID){
            Transaction transactionToEdit = null;
            

            foreach (Transaction transaction in _transactions){
                if (transaction.TransactionID == transactionID){
                    transactionToEdit = transaction;
                    break;
                }
            }
            
            if (transactionToEdit == null){
                Console.WriteLine("Transaction with id (" + transactionID + ") not found in card with card number (" + _cardNumber + ").");
                return;
            }
            
            if(transactionToEdit.TransactionStatus != TransactionStatus.Pending){
                Console.WriteLine("Transaction with id (" + transactionID + ") cannot be edited or cancelled as it is not in Pending status.");
                return;
            }
            
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Edit transaction");
            Console.WriteLine("2. Cancel transaction");
            Console.WriteLine("3. Go back");
            
            Console.Write("Your Choice: ");
            string choice = Console.ReadLine();
            
            switch(choice){
                case "1":                    
                    Console.Write("Enter the new amount (current: " + transactionToEdit.Amount + ") or press Enter to keep current: ");
                    string amountInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(amountInput))
                    {
                        double newAmount;
                        if (double.TryParse(amountInput, out newAmount) && newAmount > 0)
                        {
                            transactionToEdit.Amount = newAmount;
                            Console.WriteLine("Transaction amount updated to " + transactionToEdit.Amount + ".");
                            transactionToEdit.IsCountedForPayment = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount. Transaction amount remains " + transactionToEdit.Amount + ".");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Transaction amount remains " + transactionToEdit.Amount + ".");
                    }
                    Console.Write("Enter new description (current: " + transactionToEdit.Description + ") or press Enter to keep current: ");
                    string descriptionInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(descriptionInput))
                    {
                        transactionToEdit.Description = descriptionInput;
                        Console.WriteLine("Transaction description updated to " + transactionToEdit.Description + ".");
                    }
                    else
                    {
                        Console.WriteLine("Transaction description remains " + transactionToEdit.Description + ".");
                    }
                    Console.Write("Enter new transaction time (format: DD/MM/YYYY HH:MM, current: " + transactionToEdit.TransactionTime + ") or press Enter to keep current: ");
                    string timeInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(timeInput))
                    {
                        DateTime newTime;
                        if (DateTime.TryParseExact(timeInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out newTime))
                        {
                            transactionToEdit.TransactionTime = newTime;
                            Console.WriteLine("Transaction time updated to " + transactionToEdit.TransactionTime + ".");
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Transaction time remains " + transactionToEdit.TransactionTime + ".");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Transaction time remains " + transactionToEdit.TransactionTime + ".");
                    }
                    Console.Write("Enter new receiver (current: " + transactionToEdit.Receiver + ") or press Enter to keep current: ");
                    string receiverInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(receiverInput))
                    {
                        transactionToEdit.Receiver = receiverInput;
                        Console.WriteLine("Transaction receiver updated to " + transactionToEdit.Receiver + ".");
                    }
                    else
                    {
                        Console.WriteLine("Transaction receiver remains " + transactionToEdit.Receiver + ".");
                    }
                    Console.Write($"Enter new sender (current: {transactionToEdit.Sender}) or press Enter to keep current: ");
                    string senderInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(senderInput))
                    {
                        transactionToEdit.Sender = senderInput;
                        Console.WriteLine("Transaction sender updated to " + transactionToEdit.Sender + ".");
                    }
                    else
                    {
                        Console.WriteLine("Transaction sender remains " + transactionToEdit.Sender + ".");
                    }
                    Console.WriteLine("Transaction with id (" + transactionID + ") has been updated successfully.");
                    break;
                case "2":
                    transactionToEdit.UpdateStatus(TransactionStatus.Canceled);
                    break;
                case "3":
                default:
                    Console.WriteLine("No changes made to transaction with id (" + transactionID + ")");
                    break;
            }
        }
        /// <summary>
        /// This public method displays the details of the transactions of the card.
        /// </summary>
        public void DisplayTransactionDetails(){
            int i = 1 ;
            if(_transactions.Count == 0){
                Console.WriteLine("No transactions found for card with card number (" + _cardNumber + ").");
            }
            else{
                foreach (Transaction transaction in _transactions){
                    Console.WriteLine("Transaction " + i );
                    if(transaction.PopOutCount == 0 && DateTime.Now > transaction.TransactionTime.AddMinutes(30)){
                        transaction.UpdateStatus(TransactionStatus.Completed);
                        transaction.PopOutCount ++;
                    }
                    transaction.DisplayDetails();
                    i++;
                }
                Console.WriteLine("End of Transactions");
            }
        }
        /// <summary>
        /// This public method checks if the card is expired and updates the card status accordingly.
        /// </summary>
        public void CheckExpired(){
            if(DateTime.Now > _expiryDate){
                _cardStatus = CardStatus.Expired;
            }
        }
        /// <summary>
        /// This abstract method that must be implemented by child classes to display card details.
        /// </summary>
        public abstract void DisplayDetails();
    }
}