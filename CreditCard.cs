using System;

namespace MultipleCreditCardManagement{
    /// <summary>
    /// This is credit card class that has its own eight attributes and inherited attributes and methods from Card class.
    /// </summary>
    public class CreditCard : Card
    {
        private double _creditLimit;
        private double _interestRate;
        private double _availableCredit;
        private double _chargedAmount;
        private double _minimumPayment;
        private DateTime _paymentDueDate;
        private int _points;
        private bool _isPaymentMade;
        /// <summary>
        /// Parameterised constructor to set card number, card holder name, bank name, expiry date, credit limit, interest rate, minimum payment, initialise the available credit to credit limit, set the payment due date to 30 days from now, charged amount and point to 0. And make the isPayment made status to false.
        /// </summary>
        public CreditCard(string cardNumber, string cardHolderName, string bankName, DateTime expiryDate, double creditLimit, double interestRate, double minimumPayment) : base(cardNumber, cardHolderName, bankName, expiryDate)
        {
            _creditLimit = creditLimit;
            _interestRate = interestRate;
            _availableCredit = creditLimit;
            _chargedAmount = 0;
            _minimumPayment = minimumPayment;
            _points = 0;
            _isPaymentMade = false;
            _paymentDueDate = DateTime.Now.AddDays(30); 
        }
        /// <summary>
        /// Property to get and set the credit limit of the credit card.
        /// </summary>
        public double CreditLimit
        {
            get { return _creditLimit; }
            set { _creditLimit = value; }
        }
        /// <summary>
        /// Property gets the interest rate of the credit card.
        /// </summary>
        public double InterestRate
        {
            get { return _interestRate; }
        }
        /// <summary>
        /// This property gets the available credit of the credit card.
        /// </summary> 
        public double AvailableCredit
        {
            get { return _availableCredit; }
        }
        /// <summary>
        /// This property gets and set the minimum payment of the credit card.
        /// </summary>
        public double MinimumPayment
        {
            get { return _minimumPayment; }
            set { _minimumPayment = value; }
        }
        /// <summary>
        /// This property gets the points of the credit card.
        /// </summary>
        public int Points
        {
            get { return _points; }
        }
        /// <summary>
        /// Override Method to display the details of the credit card including inherited attributes and methods from Card class.
        /// </summary>
        public override void DisplayDetails(){
            Console.WriteLine("Credit Card Details:");
            Console.WriteLine("Card Number: " + base.CardNumber);
            Console.WriteLine("Card Holder Name: " + base.CardHolderName);
            Console.WriteLine("Bank Name: " + base.BankName);
            Console.WriteLine("Expiry Date: " + base.ExpiryDate.ToShortDateString());
            Console.WriteLine("Credit Limit: " + _creditLimit);
            Console.WriteLine("Interest Rate: " + _interestRate + "%");
            Console.WriteLine("Available Credit: " + _availableCredit);
            Console.WriteLine("Charged Amount: " + _chargedAmount);
            Console.WriteLine("Minimum Payment: " + _minimumPayment);
            Console.WriteLine("Payment Due Date: " + _paymentDueDate.ToShortDateString());
            Console.WriteLine("Is Payment Made: " + _isPaymentMade);
            Console.WriteLine("Points Earned: " + _points);
        }
        /// <summary>
        /// Public Method to display the reward points and allow the user to redeem them for items.
        /// </summary>
        public void DisplayReward(){
            Console.WriteLine("Reward Points on the card: " + _points);
            Console.WriteLine("You can redeem your points for the items in the rewards catalog.");
            Console.WriteLine("Item 1: 100 points - Gift Card");
            Console.WriteLine("Item 2: 200 points - Shopping Voucher");
            Console.WriteLine("Item 3: 500 points - Travel Voucher");
            Console.WriteLine("Item 4: 1000 points - Electronics Voucher");
            Console.WriteLine("Item 5: 2000 points - Luxury Item");
            Console.Write("Please enter the item number you want to redeem:");
            int itemNumber;
            while (true)
            {
                string input = Console.ReadLine()!;
                if (string.IsNullOrEmpty(input))
                {
                    Console.Write("Please enter a valid item number: ");
                    continue;
                }
                
                if (int.TryParse(input, out itemNumber))
                {
                    break;
                }
                else
                {
                    Console.Write("Invalid input. Please enter a numeric item number: ");
                }
            }
            switch (itemNumber)
            {
                case 1:
                    if (_points >= 100)
                    {
                        _points -= 100;
                        Console.WriteLine("You have redeemed a Gift Card!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough points to redeem this item.");
                    }
                    break;
                case 2:
                    if (_points >= 200)
                    {
                        _points -= 200;
                        Console.WriteLine("You have redeemed a Shopping Voucher!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough points to redeem this item.");
                    }
                    break;
                case 3:
                    if (_points >= 500)
                    {
                        _points -= 500;
                        Console.WriteLine("You have redeemed a Travel Voucher!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough points to redeem this item.");
                    }
                    break;
                case 4:
                    if (_points >= 1000)
                    {
                        _points -= 1000;
                        Console.WriteLine("You have redeemed an Electronics Voucher!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough points to redeem this item.");
                    }
                    break;
                case 5:
                    if (_points >= 2000)
                    {
                        _points -= 2000;
                        Console.WriteLine("You have redeemed a Luxury Item!");
                    }
                    else
                    {
                        Console.WriteLine("Not enough points to redeem this item.");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid item number.");
                    break;
            }
        }
        /// <summary>
        /// Public method return boolean outcome to check remaining credit before a transaction and has the parameter to accept the amount and transaction type.
        /// </summary>
        public bool PreCheckRemainCredit(double amount, TransactionType transactionType){
            if (transactionType == TransactionType.Expense)
            {
                if (amount <= _availableCredit && amount <= _creditLimit)
                {
                    Console.WriteLine("Transaction successful. Remaining credit will be: " + (_availableCredit - amount).ToString());
                    return true;
                }
                else
                {
                    Console.WriteLine("Transaction failed. Insufficient available credit or exceeds credit limit.");
                    return false;
                }
            }
            else if (transactionType == TransactionType.Income || transactionType == TransactionType.Transfer)
            {
                Console.WriteLine("Credit cannot be added for income or transfer transactions.");
                return false;
            }
            else
            {
                Console.WriteLine("Invalid transaction type.");
                return false;
            }
        }
        /// <summary>
        /// Public method to reset the credit limit and available credit to the original limit if the payment is made and no outstanding balance exists.
        /// </summary>
        public void ResetCredit(){
            if(DateTime.Now <= _paymentDueDate && (_chargedAmount == 0 || _isPaymentMade))
            {
                _availableCredit = _creditLimit;
                Console.WriteLine("Credit has been reset. Available credit is now: " + _availableCredit);
            }
            else
            {
                Console.WriteLine("Cannot reset credit. Outstanding balance exists.It is blocked!");
                base.CardStatus = CardStatus.Blocked;
            }
        }
        /// <summary>
        /// Public method to calculate the remaining credit after each transaction and update the available credit accordingly.
        /// </summary>
        public void CalculateRemainCredit(){
            foreach (Transaction transaction in base.Transactions){
                if(transaction.TransactionType == TransactionType.Expense && transaction.TransactionStatus == TransactionStatus.Completed){
                    _availableCredit -= transaction.Amount;
                }
            }
        }
        /// <summary>
        /// Public method to calculate the charged amount and interest on the credit card if payment is not made before the due date.
        /// </summary>
        public void CalculateChargedAmount(){
            foreach (Transaction transaction in base.Transactions){
                if(transaction.TransactionStatus == TransactionStatus.Completed && transaction.IsCreditPaid == false && transaction.IsCountedForPayment == false){
                    transaction.IsCountedForPayment = true;
                    _chargedAmount += transaction.Amount;
                }
            }
            if(_chargedAmount != 0){
                if(DateTime.Now > _paymentDueDate && _chargedAmount > 0)
                {
                    if(_chargedAmount < _minimumPayment)
                    {
                        Console.WriteLine("Total amount to be paid: " + _minimumPayment);
                    }
                    else
                    {
                        Console.WriteLine("Interest charged on the amount: " + _chargedAmount * (_interestRate / 100));
                        Console.WriteLine("Total amount to be paid: " + (_chargedAmount + (_chargedAmount * (_interestRate / 100))));
                    }
                }
                else
                {
                    if(_chargedAmount < _minimumPayment)
                    {
                        Console.WriteLine("No interest charged as payment is made on time.");
                        Console.WriteLine("Total amount to be paid: " + _minimumPayment);
                    }
                    else
                    {
                        Console.WriteLine("No interest charged as payment is made on time.");
                        Console.WriteLine("Total amount to be paid: " + _chargedAmount);
                    }
                }
                Console.WriteLine("Please make the payment before the due date to avoid further interest charges.");
            }
            else{
                Console.WriteLine("No amount charged on the card. Payment is not required.");
            }
        }
        /// <summary>
        /// Public method to pay the bill and update the charged amount accordingly(has the parameter to accept the amount).
        /// </summary>
        public void PayBill(double amount){
            if (amount <= _chargedAmount)
            {
                _chargedAmount -= amount;
                _isPaymentMade = true;
                Console.WriteLine("Payment successful. Remaining charged amount: " + _chargedAmount);
            }
            else
            {
                Console.WriteLine("Payment failed. Amount should be lower than the charged amount.");
            }
        }
        /// <summary>
        /// Public method to add points based on the amount spent on the credit card(has the parameter to accept the amount).
        /// </summary>
        public void AddPoints(double amount){
            if (amount > 0)
            {
                _points += (int)(amount / 10);
                Console.WriteLine("You have earned " + (int)(amount / 10) + " points.");
            }
            else
            {
                Console.WriteLine("Invalid amount. No points earned.");
            }
        }
    }
}