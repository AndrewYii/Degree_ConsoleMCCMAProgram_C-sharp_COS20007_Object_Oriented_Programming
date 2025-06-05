using System;
using System.Collections.Generic;
#nullable disable

namespace MultipleCreditCardManagement{
    /// <summary>
    /// This is the User class which has the five attributes.
    /// </summary> <summary>
    public class User{
        private string _realName;
        private string _username;
        private string _password;
        private bool _isLogined;
        private List<Card> _cards;
        /// <summary>
        /// Parameterised constructor that sets the realName, username, password ,initialises isLogined to false and cards to an empty list.
        /// </summary>
        public User(string realName, string username, string password){
            _realName = realName;
            _username = username;
            _password = password;
            _isLogined = false;
            _cards = new List<Card>();
        }
        /// <summary>
        /// This public method is used to log out the user. It sets the isLogined attribute to false and prints a message indicating successful logout.
        /// </summary>
        public void Logout(){
            _isLogined = false;
            Console.WriteLine("User logged out successfully.");
        }
        /// <summary>
        /// This public method is used to log in the user. It checks if the provided username and password match the stored values.
        /// </summary>
        public void Login(string username, string password){
            if(_username == username && _password == password){
                _isLogined = true;
                Console.WriteLine("User logged in successfully.");
            }else{
                Console.WriteLine("Invalid password.");
            }
        }
        /// <summary>
        /// This public method is used to view the user's profile. It prints the real name, username, password and a message indicating that this is the user's profile information.
        /// </summary>
        public void ViewProfile(){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Profile Details:");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Real Name: " + _realName);
            Console.WriteLine("Username: " + _username);
            Console.WriteLine("Password: " + _password);
            Console.WriteLine("This is your profile information.");
        }
        /// <summary>
        /// This public method is used to edit the user's profile. It prompts the user to enter new values for real name, username and password.If the user enters a new value, it updates the corresponding attribute. If the user presses Enter without entering a new value, it keeps the current value.
        /// </summary>
        public void EditProfile(){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Editing The Profile");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Enter the new name (current name is " + _realName + ") or press Enter to keep current:");
            string realNameInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(realNameInput)){
                _realName = realNameInput;
                Console.WriteLine("Real name updated successfully.");
            }
            else{
                Console.WriteLine("Real name remains the same.");
            }
            
            Console.Write("Enter your new username (current username is " + _username + ") or press Enter to keep current:");
            string usernameInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(usernameInput)){
                _username = usernameInput;
                Console.WriteLine("Username updated successfully.");
            }
            else{
                Console.WriteLine("Username remains the same.");
            }
            
            Console.Write("Enter your new password (current password is " + _password + ") or press Enter to keep current:");
            string passwordInput = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(passwordInput)){
                _password = passwordInput;
                 Console.WriteLine("Password updated successfully.");
            }
            else{
                Console.WriteLine("Password remains the same.");
            }
            Console.WriteLine("Profile updated successfully.");
        }
        /// <summary>
        /// This public method adds a card to the user's list of cards. It takes a Card object as a parameter and adds it to the _cards list. It also prints a message indicating successful addition of the card.
        /// </summary>
        public void AddCard(Card card){
            _cards.Add(card);
            Console.WriteLine("Card added successfully.");
        }
        /// <summary>
        /// This public method adds a card to the user's list of cards. It takes a Card object as a parameter and adds it to the _cards list. It also prints a message indicating successful addition of the card.
        /// </summary>
        public void EditCard(string cardNumber){
            bool found = false;
            foreach(Card card in _cards){
                if(card.CardNumber == cardNumber){
                    found = true;
                    if(card is CreditCard){
                        Console.Write("Enter the new credit limit (current limit is " + ((CreditCard)card).CreditLimit + ") or press Enter to keep current:");
                        string creditLimitinput = Console.ReadLine();
                        if(creditLimitinput != null && creditLimitinput != ""){
                            if(double.TryParse(creditLimitinput, out double newCreditLimit)){
                                ((CreditCard)card).CreditLimit = newCreditLimit;
                                Console.WriteLine("Credit limit updated successfully.");
                            }
                            else{
                                Console.WriteLine("Invalid input. Credit limit remains the same.");
                            }
                        }
                        else{
                            Console.WriteLine("Credit limit remain the same.");
                        }
                        Console.Write("Enter the new minimum payment (current minimum payment is " + ((CreditCard)card).MinimumPayment + ") or press Enter to keep current:");
                        string minimumPaymentInput = Console.ReadLine();
                        if(minimumPaymentInput != null && minimumPaymentInput != ""){
                            if(double.TryParse(minimumPaymentInput, out double newMinimumPayment)){
                                ((CreditCard)card).MinimumPayment = newMinimumPayment;
                                Console.WriteLine("Minimum payment updated successfully.");
                            }
                            else{
                                Console.WriteLine("Invalid input. Minimum payment remains the same.");
                            }
                        }
                        else{
                            Console.WriteLine("Minimum payment remain the same.");
                        }
                    }
                    else if(card is DebitCard){
                        Console.Write("Enter the new daily transaction limit (current limit is " + ((DebitCard)card).TransactionLimit + ") or press Enter to keep current:");
                        string transactionLimitInput = Console.ReadLine();
                        if(transactionLimitInput != null && transactionLimitInput != ""){
                            if(double.TryParse(transactionLimitInput, out double newTransactionLimit)){
                                ((DebitCard)card).TransactionLimit = newTransactionLimit;
                                Console.WriteLine("Daily transaction limit updated successfully.");
                            }
                            else{
                                Console.WriteLine("Invalid input. Daily transaction limit remains the same.");
                            }
                        }
                        else{
                            Console.WriteLine("Daily transaction limit remain the same.");
                        }
                    }
                }
            }
            if(!found){
                Console.WriteLine("Card not found.");
            }
        }
        /// <summary>
        /// This public method removes a card from the user's list of cards. It takes a card number as a parameter and searches for the corresponding card in the _cards list. If found, it removes the card and prints a message indicating successful removal. If not found, it prints a message indicating that the card was not found.
        /// </summary>
        public void RemoveCard(string cardNumber){
            foreach(Card card in _cards){
                if(card.CardNumber == cardNumber){
                    _cards.Remove(card);
                    Console.WriteLine("Card removed successfully.");
                    return;
                }
            }
            Console.WriteLine("Card not found.");
        }
        /// <summary>
        /// This public method displays the user's card details. It takes a boolean parameter to choose whether to view a specific card or all cards, and a string parameter for the card number.
        /// </summary>
        public void ViewCard(bool choose, string cardNumber){
            int i = 0;
            if(choose){
                bool check = false;
                foreach(Card card in _cards){
                    if(card.CardNumber == cardNumber){
                        card.DisplayDetails();
                        check = true;
                        return;
                    }
                }
                if(!check){
                    Console.WriteLine("Card not found.");
                }
            }else{
                Console.WriteLine("---------------------------------------------------------------------------------");
                foreach(Card card in _cards){
                    card.DisplayDetails();
                    i++;
                }
                if(i==0){
                    Console.WriteLine("No cards available.");
                }
            }
        }
        /// <summary>
        /// This public method suggests suitable cards based on the specified amount. It checks both debit and credit cards for eligibility and displays recommendations.
        /// </summary>
        public void SuggestCard(double amount){
            List<DebitCard> eligibleDebitCards = new List<DebitCard>();
            List<CreditCard> eligibleCreditCards = new List<CreditCard>();            
            foreach(Card card in _cards){
                if(card is DebitCard debitCard && debitCard.Balance >= amount && debitCard.TransactionLimit >= amount && debitCard.CardStatus == CardStatus.Active){
                    eligibleDebitCards.Add(debitCard);
                }
                else if(card is CreditCard creditCard && creditCard.AvailableCredit >= amount && creditCard.CardStatus == CardStatus.Active){
                    eligibleCreditCards.Add(creditCard);
                }
            }
            bool foundSuggestion = false;
            if(eligibleDebitCards.Count > 0){
                int bestDebitCardIndex = 0;
                List<DebitCard> sortedDebitCards = eligibleDebitCards.OrderByDescending(d => d.Balance).ToList();
                Console.WriteLine("Recommended Debit Cards:");
                foreach(DebitCard card in sortedDebitCards){
                    if(bestDebitCardIndex == 0){
                        Console.WriteLine("Debit Card "+  card.CardNumber + " with balance " + card.Balance + " and transaction limit " + card.TransactionLimit + "[most recommended]");
                        bestDebitCardIndex++;
                    }
                    else{
                        Console.WriteLine("Debit Card "+  card.CardNumber + " with balance " + card.Balance + " and transaction limit " + card.TransactionLimit);
                    }
                }
                foundSuggestion = true;
            }
            if(eligibleCreditCards.Count > 0){
                int bestCreditCardIndex = 0;
                List<CreditCard> sortedCreditCards = eligibleCreditCards.OrderBy(c => c.InterestRate).ToList();
                Console.WriteLine("Recommended Credit Cards:");
                foreach(CreditCard card in sortedCreditCards){
                    if(bestCreditCardIndex == 0){
                        Console.WriteLine("Credit Card "+  card.CardNumber + " with interest rate " + card.InterestRate + "% and available credit " + card.AvailableCredit + "[most recommended]");
                        bestCreditCardIndex++;
                    }
                    else{
                        Console.WriteLine("Credit Card "+  card.CardNumber + " with interest rate " + card.InterestRate + "% and available credit " + card.AvailableCredit);
                    }
                }
                foundSuggestion = true;
            }
            if(!foundSuggestion){
                Console.WriteLine("No suitable card found for this transaction.");
            }
        }
        /// <summary>
        /// This public method displays the user's activity log based on the specified frequency (daily or monthly).
        /// </summary>
        public void ViewActivityLog(bool isDaily){
            bool found = false;
            if(isDaily){
                Console.WriteLine("Daily Activity Log:");
                foreach(Card card in _cards){
                    foreach(Transaction transaction in card.Transactions){
                        if(transaction.TransactionTime >= DateTime.Now.AddHours(-24) && transaction.TransactionTime < DateTime.Now){
                            transaction.DisplayDetails();
                            found = true;
                        }
                    }
                }
            }else{
                Console.WriteLine("Monthly Activity Log:");
                foreach(Card card in _cards){
                    foreach(Transaction transaction in card.Transactions){
                        if(transaction.TransactionTime >= DateTime.Now.AddMonths(-1) && transaction.TransactionTime < DateTime.Now){
                            transaction.DisplayDetails();
                            found = true;
                        }
                    }
                }
            }
            if(!found){
                Console.WriteLine("No activity log is found!");
            }
        }
        /// <summary>
        /// This public method displays the user's activity report based on the specified frequency (monthly or yearly).
        /// </summary>
        public void ViewReport(bool isMonthly, int year, int? month){
            double totalExpenses = 0;
            double totalEarnings = 0;
            bool found = false;
            if(isMonthly){
                Console.WriteLine("Monthly Total Expenses Vs Earnings Report for Month (" + month + ") Year (" + year + "): ");
                foreach(Card card in _cards){
                    foreach(Transaction transaction in card.Transactions){
                        if(transaction.TransactionTime.Year == year && transaction.TransactionTime.Month == month && transaction.TransactionStatus == TransactionStatus.Completed){
                            found = true;
                            if(transaction.TransactionType == TransactionType.Expense || transaction.TransactionType == TransactionType.Transfer){
                                totalExpenses += transaction.Amount;
                            } 
                            else if(transaction.TransactionType == TransactionType.Income){
                                totalEarnings += transaction.Amount;
                            }
                        }
                    }
                }
                if(found){
                    Console.WriteLine("Total Monthly Expenses: " + totalExpenses);
                    Console.WriteLine("Total Monthly Earnings: " + totalEarnings);
                    Console.WriteLine("Total Monthly Expenses Vs Earnings (Total Earnings - Total Expenses): " + (totalEarnings - totalExpenses ));
                } else {
                    Console.WriteLine("No transactions found for the specified month.");
                }
            }
            else{
                Console.WriteLine("Yearly Total Expenses Vs Earnings Report for " + year + ":");
                foreach(Card card in _cards){
                    foreach(Transaction transaction in card.Transactions){
                        if(transaction.TransactionTime.Year == year && transaction.TransactionStatus == TransactionStatus.Completed){
                            found = true;
                            if(transaction.TransactionType == TransactionType.Expense || transaction.TransactionType == TransactionType.Transfer){
                                totalExpenses += transaction.Amount;
                            } 
                            else if(transaction.TransactionType == TransactionType.Income){
                                totalEarnings += transaction.Amount;
                            }
                        }
                    }
                }
                if(found){
                    Console.WriteLine("Total Yearly Expenses: " + totalExpenses);
                    Console.WriteLine("Total Yearly Earnings: " + totalEarnings);
                    Console.WriteLine("Total Yearly Expenses Vs Earnings (Total Earnings - Total Expenses): " + (totalEarnings - totalExpenses));
                } else {
                    Console.WriteLine("No transactions found for the specified year.");
                }                
            }
        }
        /// <summary>
        /// Property to get the username of the user.
        /// </summary>
        public string Username{
            get { return _username; }
        }
        /// <summary>
        /// Property to get the islogined status of the user.
        /// </summary>
        public bool IsLogined{
            get { return _isLogined; }
        }
        /// <summary>
        /// Property to get the list of cards of the user.
        /// </summary>
        public List<Card> Cards{
            get { return _cards; }
        }            
    }
}