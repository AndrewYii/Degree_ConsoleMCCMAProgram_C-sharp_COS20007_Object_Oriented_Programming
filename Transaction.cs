using System;

namespace MultipleCreditCardManagement{
    /// <summary>
    /// This is the Transaction class which has the eleven attributes.
    /// </summary>
    public class Transaction{
        private string _transactionID;
        private double _amount;
        private DateTime _transactionTime;
        private string _description;
        private string _receiver;
        private string _sender;
        private TransactionType _transactionType;
        private TransactionStatus _transactionStatus;
        private int _popOutCount;
        private bool _isCreditPaid;
        private bool _isCountedForPayment;
        /// <summary>
        /// Parameterised constructor to set amount, transaction time, description, receiver, sender, transaction type, initialise the transaction status to pending and set the pop out count to 0, credit paid to false, is counted for payment to false and the transaction id to a new GUID.
        /// </summary>
        public Transaction(double amount, DateTime transactionTime, string description, string receiver, string sender, TransactionType transactionType){
            _transactionID = Guid.NewGuid().ToString();
            _amount = amount;
            _transactionTime = transactionTime;
            _description = description;
            _receiver = receiver;
            _sender = sender;
            _transactionType = transactionType;
            _transactionStatus = TransactionStatus.Pending;
            _popOutCount = 0;
            _isCreditPaid = false;
            _isCountedForPayment = false;
        }
        /// <summary>
        /// Property to get the transaction ID of the transaction.
        /// </summary>
        public string TransactionID{
            get { return _transactionID; }
        }
        /// <summary>
        /// Property to get and set the amount of the transaction.
        /// </summary>
        public double Amount{
            get { return _amount; }
            set { _amount = value; }
        }
        /// <summary>
        /// Property to get and set the transaction time of the transaction.
        /// </summary>
        public DateTime TransactionTime{
            get { return _transactionTime; }
            set { _transactionTime = value; }
        }
        /// <summary>
        /// Property to get and set the description of the transaction.
        /// </summary>
        public string Description{
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// Property to get and set the receiver of the transaction.
        /// </summary>
        public string Receiver{
            get { return _receiver; }
            set { _receiver = value; }
        }
        /// <summary>
        /// Property to get and set the sender of the transaction.
        /// </summary>
        public string Sender{
            get { return _sender; }
            set { _sender = value; }
        }
        /// <summary>
        /// Property to get and set the transaction type of the transaction.
        /// </summary>
        public TransactionType TransactionType{
            get { return _transactionType; }
            set { _transactionType = value; }
        }
        /// <summary>
        /// Property to get the transaction status of the transaction.
        /// </summary>
        public TransactionStatus TransactionStatus{
            get { return _transactionStatus; }
        }
        /// <summary>
        /// Property to get and set the is credit paid status of the transaction.
        /// </summary>
        public bool IsCreditPaid{
            get { return _isCreditPaid; }
            set { _isCreditPaid = value; }
        }
        /// <summary>
        /// Property to get and set the pop out count of the transaction.
        /// </summary>
        public int PopOutCount{
            get { return _popOutCount; }
            set { _popOutCount = value; }
        }
        /// <summary>
        /// Property to get and set the is counted for payment status of the transaction.
        /// </summary>
        public bool IsCountedForPayment{
            get { return _isCountedForPayment; }
            set { _isCountedForPayment = value; }
        }
        /// <summary>
        /// Public method to update the status of the transaction and has the parameter to accept the new status.
        /// </summary>
        public void UpdateStatus(TransactionStatus newStatus){
            _transactionStatus = newStatus;
            switch(newStatus){
                case TransactionStatus.Completed:
                    Console.WriteLine("Transaction " + _transactionID + " has been completed.");
                    Console.WriteLine("You cannot edit this transaction anymore!");
                    break;
                case TransactionStatus.Canceled:
                    Console.WriteLine("Transaction " + _transactionID + " has been canceled.");
                    break;
            }
        }
        /// <summary>
        /// Public method to display the details of the transaction.
        /// </summary>
        public void DisplayDetails(){
            Console.WriteLine("Transaction ID: " + _transactionID);
            Console.WriteLine("Amount: " + _amount);
            Console.WriteLine("Transaction Time: " + _transactionTime);
            Console.WriteLine("Description: " + _description);
            Console.WriteLine("Receiver: " + _receiver);
            Console.WriteLine("Sender: " + _sender);
            Console.WriteLine("Transaction Type: " + _transactionType);
            Console.WriteLine("Transaction Status: " + _transactionStatus);
        }
    }
}