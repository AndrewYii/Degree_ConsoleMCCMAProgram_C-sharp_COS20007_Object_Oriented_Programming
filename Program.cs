using System;
using System.Collections.Generic;
using System.Globalization;
#nullable disable

namespace MultipleCreditCardManagement{
    class Program
    {
        static void Main(string[] args){
            List<User> users = new List<User>();
            bool exit = false;
            do{
                exit = MainMenu(users);
            }while(!exit);
        }
        /// <summary>
        ///  This static method handles the changing of the different displaying module (Register and Login Account) and has the parameter of users(List<User>) as the management database.
        /// </summary>
        static bool MainMenu(List<User> users){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to the Multiple Credit Card Management System!");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Register Account");
            Console.WriteLine("2. Login Account");
            Console.WriteLine("3. Exit");
            Console.Write("Your choice: ");
            string option = Console.ReadLine()!;
            Console.WriteLine("---------------------------------------------------------------------------------");
            
            switch(option){                    
                case "1":
                    RegisterAccount(users);
                    return false;
                case "2":
                    LoginAccount(users);
                    return false;
                case "3":
                    Console.WriteLine("Exiting the system. Goodbye!");
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    return true;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    return false;
            }
        }
        /// <summary>
        /// This static method handles the registration module which allows users to register the account with error handling such as cannot register the same username which has already existed in the database(users) and has the parameter of users(List<User>) to connect with the database.
        /// </summary>
        static void RegisterAccount(List<User> users){
            string username;
            string name;
            string password;
            bool usernameExists;
            
            Console.WriteLine("Registering a new account");
            Console.WriteLine("---------------------------------------------------------------------------------");
            
            do {
                Console.Write("Please enter your real name:");
                name = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(name)) {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));
            
            do{
                usernameExists = false;
                Console.Write("Please enter your username:");
                username = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(username)) {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                    continue;
                }
                foreach (User user in users){
                    if(user.Username == username){
                        usernameExists = true;
                        break;
                    }
                }
                if(usernameExists){
                    Console.WriteLine("Username already exists. Please try again.");
                }
            }while(usernameExists || string.IsNullOrWhiteSpace(username));
            
            do {
                Console.Write("Please enter your password:");
                password = Console.ReadLine()!;
                if (string.IsNullOrWhiteSpace(password)) {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(password));
            
            User newUser = new User(name, username, password);
            users.Add(newUser);
            Console.WriteLine("Account registered successfully!");
        }
        /// <summary>
        /// This method handles user login and verifies credentials and has also the parameter of users to connect with the database.
        /// </summary>
        static void LoginAccount(List<User> users){
            bool loginSuccess = false;
            
            Console.WriteLine("Logging in to your account");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter your username:");
            string loginUsername = Console.ReadLine()!;
            
            foreach(User user in users){
                if(user.Username == loginUsername){
                    loginSuccess = true;
                    Console.Write("Please enter your password:");
                    string loginPassword = Console.ReadLine()!;
                    user.Login(loginUsername,loginPassword);
                    
                    while(user.IsLogined){
                        UpdateCardStatuses(user);
                        Console.WriteLine("---------------------------------------------------------------------------------");
                        Console.WriteLine("Welcome " + user.Username + "!");
                        Console.WriteLine("---------------------------------------------------------------------------------");
                        UserMainMenu(user);
                    }
                    break;
                }
            }
            
            if(!loginSuccess){
                Console.WriteLine("User not found. Please go to register.");
            }
        }
        /// <summary>
        /// This static method updates the statuses of the user's cards, checking for expiration and calculating balances and has the parameter of user to track the logged in user.
        /// </summary>
        static void UpdateCardStatuses(User user){
            foreach(Card card in user.Cards){
                card.CheckExpired();
                if(card is DebitCard d1){
                    d1.CalculateRemainBalance();
                }
                else if(card is CreditCard c1){
                    c1.CalculateRemainCredit();
                }
            }
        }
        /// <summary>
        /// This static method displays the user main menu , handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void UserMainMenu(User user){
            Console.WriteLine("Please select an option: ");
            Console.WriteLine("1. User Profile Management");
            Console.WriteLine("2. Card Management");
            Console.WriteLine("3. Transaction Management");
            Console.WriteLine("4. Reporting Management");
            Console.WriteLine("5. Logout");
            Console.Write("Your choice: ");
            string option = Console.ReadLine()!;
            
            switch(option){
                case "1":
                    ProfileManagementMenu(user);
                    break;
                case "2":
                    CardManagementMenu(user);
                    break;
                case "3":
                    TransactionManagementMenu(user);
                    break;
                case "4":
                    ReportMenu(user);
                    break;
                case "5":
                    user.Logout();
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        /// <summary>
        /// This static method displays the user profile management menu , handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void ProfileManagementMenu(User user){
            bool exit = false;
            while(!exit){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("User Profile Management");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("1. View Profile");
                Console.WriteLine("2. Edit Profile");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Your choice: ");
                string option = Console.ReadLine()!;
                
                switch(option){
                    case "1":
                        user.ViewProfile();
                        break;
                    case "2":
                        user.EditProfile();
                        break;
                    case "3":
                        Console.WriteLine("Going back to Main Menu.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// This static method displays the user card management menu , handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void CardManagementMenu(User user){
            bool exit = false;
            while(!exit){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Card Management");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("1. View Cards");
                Console.WriteLine("2. Add Card");
                Console.WriteLine("3. Edit Card");
                Console.WriteLine("4. Remove Card");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Your choice: ");
                string option = Console.ReadLine()!;
                
                switch(option){
                    case "1":
                        ViewCardsMenu(user);
                        break;
                    case "2":
                        AddCardMenu(user);
                        break;
                    case "3":
                        EditCardMenu(user);
                        break;
                    case "4":
                        RemoveCardMenu(user);
                        break;
                    case "5":
                        Console.WriteLine("Going back to Main Menu.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// This static method displays the user view card menu , handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void ViewCardsMenu(User user){
            bool viewcard = false;
            while(!viewcard){
                int i = 1;
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Viewing card details");
                Console.WriteLine("Added Card Number:");
                foreach(Card card in user.Cards){
                    Console.WriteLine("Card " + i +" Number : "+ card.CardNumber);
                    i++;
                }
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1. View One Card");
                Console.WriteLine("2. View All Cards");
                Console.WriteLine("3. Go back");
                Console.Write("Your choice: ");
                string choice = Console.ReadLine()!;
                if(choice == "1"){
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.Write("Please enter the card number:");
                    string cardNumber1 = Console.ReadLine()!;
                    user.ViewCard(true, cardNumber1);
                    viewcard = true;
                }
                else if(choice == "2"){
                    user.ViewCard(false, null);
                    viewcard = true;
                }
                else if(choice =="3"){
                    Console.WriteLine("Going back to card management menu.");
                    viewcard = true;
                }
                else{
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
        /// <summary>
        /// This static method displays the user add card menu ,handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void AddCardMenu(User user){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Adding a new card");
            Console.WriteLine("Total number of cards: " + user.Cards.Count);
            Console.WriteLine("Added Card Number:");
            foreach(Card card in user.Cards){
                Console.WriteLine("Card " + i +" Number : "+ card.CardNumber);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the card number:");
            string cardNumber = Console.ReadLine()!;
            
            bool cardExists = false;
            foreach (Card card in user.Cards){
                if(card.CardNumber == cardNumber){
                    Console.WriteLine("Card number already exists. Please try again.");
                    cardExists = true;
                    break;
                }
            }
            
            if(!cardExists){
                Console.Write("Please enter the card holder name:");
                string cardHolderName = Console.ReadLine()!;
                Console.Write("Please enter the bank name:");
                string bankName = Console.ReadLine()!;
                DateTime expiryDate;
                bool validDate = false;
                do {
                    Console.Write("Please enter the expiry date (DD/MM/YYYY):");
                    string dateInput = Console.ReadLine()!;
                    if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out expiryDate)) {
                        validDate = true;
                    } else {
                        Console.WriteLine("Invalid date format. Please try again.");
                    }
                } while (!validDate);

                bool validtype = false;
                do{
                    Console.Write("Please enter the card type (Credit/Debit):");
                    string cardType = Console.ReadLine()!;
                    if(cardType.ToLower() == "credit"){
                        AddCreditCard(user, cardNumber, cardHolderName, bankName, expiryDate);
                        validtype = true;
                    }
                    else if(cardType.ToLower() == "debit"){
                        AddDebitCard(user, cardNumber, cardHolderName, bankName, expiryDate);
                        validtype = true;
                    }
                    else{
                        Console.WriteLine("Invalid card type. Please try again.");
                    }
                }while(!validtype);
            }
        }
        /// <summary>
        ///  This static method displays the user add credit card info,handles user choices (with error handling) and has the parameter of user to track the logged in user, card number, card holder name, bank name and expiry date to pass the parameters to the credit card constructor.
        /// </summary>
        static void AddCreditCard(User user, string cardNumber, string cardHolderName, string bankName, DateTime expiryDate){
            double creditLimit = 0;
            bool validCreditLimit = false;
            do {
                Console.Write("Please enter the credit limit:");
                if (double.TryParse(Console.ReadLine(), out creditLimit)) {
                    validCreditLimit = true;
                } else {
                    Console.WriteLine("Invalid credit limit. Please enter a numeric value.");
                }
            } while (!validCreditLimit);
            
            double interestRate = 0;
            bool validInterestRate = false;
            do {
                Console.Write("Please enter the interest rate:");
                if (double.TryParse(Console.ReadLine(), out interestRate)) {
                    validInterestRate = true;
                } else {
                    Console.WriteLine("Invalid interest rate. Please enter a numeric value.");
                }
            } while (!validInterestRate);
            
            int minimumPayment = 0;
            bool validMinimumPayment = false;
            do {
                Console.Write("Please enter the minimum payment:");
                if (int.TryParse(Console.ReadLine(), out minimumPayment)) {
                    validMinimumPayment = true;
                } else {
                    Console.WriteLine("Invalid minimum payment. Please enter an integer value.");
                }
            } while (!validMinimumPayment);
            
            CreditCard newCreditCard = new CreditCard(cardNumber, cardHolderName, bankName, expiryDate, creditLimit, interestRate, minimumPayment);
            user.AddCard(newCreditCard);
        }
        /// <summary>
        /// This static method displays the user add debit card info, handles user choices (with error handling) and has the parameter of user to track the logged in user, card number, card holder name, bank name and expiry date to pass the parameters to the debit card constructor.
        /// </summary>
        static void AddDebitCard(User user, string cardNumber, string cardHolderName, string bankName, DateTime expiryDate){
            double balance = 0;
            bool validBalance = false;
            do {
                Console.Write("Please enter the balance:");
                if (double.TryParse(Console.ReadLine(), out balance)) {
                    validBalance = true;
                } else {
                    Console.WriteLine("Invalid balance. Please enter a numeric value.");
                }
            } while (!validBalance);
            
            double transactionLimit = 0;
            bool validTransactionLimit = false;
            do {
                Console.Write("Please enter the transaction limit:");
                if (double.TryParse(Console.ReadLine(), out transactionLimit)) {
                    validTransactionLimit = true;
                } else {
                    Console.WriteLine("Invalid transaction limit. Please enter a numeric value.");
                }
            } while (!validTransactionLimit);
            
            DebitCard newDebitCard = new DebitCard(cardNumber, cardHolderName, bankName, expiryDate, balance, transactionLimit);
            user.AddCard(newDebitCard);
        }
        /// <summary>
        /// This static method displays the user edit card menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void EditCardMenu(User user){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Editing The Card");
            Console.WriteLine("Total number of cards: " + user.Cards.Count);
            Console.WriteLine("Added Card Number:");
            foreach(Card card in user.Cards){
                Console.WriteLine("Card " + i +" Number : "+ card.CardNumber);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the card number:");
            string cardNumberEdit = Console.ReadLine()!;
            user.EditCard(cardNumberEdit);
        }
        /// <summary>
        /// This static method displays the user remove card menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void RemoveCardMenu(User user){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Removing an added card");
            Console.WriteLine("Total number of cards: " + user.Cards.Count);
            Console.WriteLine("Added Card Number:");
            foreach(Card card in user.Cards){
                Console.WriteLine("Card " + i +" Number : "+ card.CardNumber);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the card number:");
            string cardNumberRemove = Console.ReadLine()!;
            user.RemoveCard(cardNumberRemove);
        }
        /// <summary>
        /// This static method displays the user transaction management menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void TransactionManagementMenu(User user){
            bool exit = false;
            while(!exit){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Transaction Management");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("1. Suggest Card for Transaction");
                Console.WriteLine("2. View Activity Log");
                Console.WriteLine("3. Select Card for Transaction Management");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Your choice: ");
                string option = Console.ReadLine()!;
                
                switch(option){
                    case "1":
                        SuggestCardMenu(user);
                        break;
                    case "2":
                        ViewActivityLogMenu(user);
                        break;
                    case "3":
                        SelectCardForTransactionsMenu(user);
                        break;
                    case "4":
                        Console.WriteLine("Going back to Main Menu.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// This static method displays the suggest card menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void SuggestCardMenu(User user){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Suggested Cards for Transaction Amount");
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the transaction amount:");
            if (double.TryParse(Console.ReadLine(), out double transactionAmount)) {
                user.SuggestCard(transactionAmount);
            } else {
                Console.WriteLine("Invalid amount. Please enter a numeric value.");
            }
        }

        static void ViewActivityLogMenu(User user){
            bool viewlog = false;
            while(!viewlog){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Viewing Activity Log");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1. View Daily Activity Log");
                Console.WriteLine("2. View Monthly Activity Log");
                Console.WriteLine("3. Go back");
                Console.Write("Your choice: ");
                string optionviewlog = Console.ReadLine()!;
                if(optionviewlog == "1"){
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    user.ViewActivityLog(true);
                    viewlog = true;
                }
                else if(optionviewlog == "2"){
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    user.ViewActivityLog(false);
                    viewlog = true;
                }
                else if(optionviewlog == "3"){
                    Console.WriteLine("Going back to transaction menu.");
                    viewlog = true;
                }
                else{
                    Console.WriteLine("Invalid option. Please try again.");
                }
            }
        }
        /// <summary>
        /// This static method displays the select card info, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void SelectCardForTransactionsMenu(User user){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Selecting a Card for Transactions");
            Console.WriteLine("Total number of cards: " + user.Cards.Count);
            Console.WriteLine("Added Card Number:");
            foreach(Card card in user.Cards){
                Console.WriteLine("Card " + i +" Number : "+ card.CardNumber);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the card number:");
            string cardNumberSelect = Console.ReadLine()!;
            
            Card selectedCard = null;
            foreach(Card card in user.Cards){
                if(card.CardNumber == cardNumberSelect){
                    selectedCard = card;
                    Console.WriteLine("Card selected successfully!");
                    break;
                }
            }
            
            if(selectedCard == null){
                Console.WriteLine("Card number not found. Please try again.");
                return;
            }
            
            CardTransactionMenu(selectedCard);
        }
        /// <summary>
        /// This static method displays the user card transaction menu, handles user choices (with error handling) and has the parameter of card to track the selected card.
        /// </summary>
        static void CardTransactionMenu(Card card){
            bool exit = false;
            while(!exit){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Selected Card: " + card.CardNumber);
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Please select an option: ");
                Console.WriteLine("1. View Transaction History");
                Console.WriteLine("2. Add Transaction");
                Console.WriteLine("3. Edit Transaction");
                Console.WriteLine("4. Remove Transaction");
                
                if(card is CreditCard){
                    Console.WriteLine("5. See Rewards Points");
                    Console.WriteLine("6. Pay Credit Card Bill");
                    Console.WriteLine("7. Go back");
                }
                else{
                    Console.WriteLine("5. Go back");
                }
                
                Console.Write("Your choice: ");
                string option = Console.ReadLine()!;
                
                switch(option){
                    case "1":
                        ViewTransactionHistoryMenu(card);
                        break;
                    case "2":
                        AddTransactionMenu(card);
                        break;
                    case "3":
                        EditTransactionMenu(card);
                        break;
                    case "4":
                        RemoveTransactionMenu(card);
                        break;
                    case "5":
                        if(card is CreditCard creditCard){
                            DisplayRewardsPointsMenu(creditCard);
                        }
                        else{
                            exit = true;
                        }
                        break;
                    case "6":
                        if(card is CreditCard creditCard2){
                            PayCreditCardBillMenu(creditCard2);
                        }
                        else{
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        break;
                    case "7":
                        if(card is CreditCard){
                            exit = true;
                        }
                        else{
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// This static method displays the user view transaction history menu, handles user choices (with error handling) and has the parameter of card to track the selected card.
        /// </summary>
        static void ViewTransactionHistoryMenu(Card card){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Transaction History for Card: " + card.CardNumber);
            Console.WriteLine("Total number of transaction: " + card.Transactions.Count);
            card.DisplayTransactionDetails();
        }
        /// <summary>
        /// This static method displays the user add transaction menu, handles user choices (with error handling) and has the parameter of card to track the selected card.
        /// </summary>
        static void AddTransactionMenu(Card card){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Adding a new transaction");
            Console.WriteLine("Total number of transaction: " + card.Transactions.Count);
            Console.WriteLine("Added Transaction ID:");
            foreach(Transaction transaction in card.Transactions){
                Console.WriteLine("Transaction " + i +" ID : "+ transaction.TransactionID);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            
            double transactionAmount = 0;
            bool validAmount = false;
            do {
                Console.Write("Please enter the transaction amount:");
                if (double.TryParse(Console.ReadLine(), out transactionAmount)) {
                    validAmount = true;
                } else {
                    Console.WriteLine("Invalid amount. Please enter a numeric value.");
                }
            } while (!validAmount);

            DateTime transactionDate;
            bool validDate = false;
            do {
                Console.Write("Please enter the transaction date and time (DD/MM/YYYY HH:MM):");
                string dateInput = Console.ReadLine()!;
                if (DateTime.TryParseExact(dateInput, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out transactionDate)) {
                    validDate = true;
                } else {
                    Console.WriteLine("Invalid date format. Please try again.");
                }
            } while (!validDate);

            Console.Write("Please enter the description:");
            string description = Console.ReadLine()!;
            Console.Write("Please enter the receiver:");
            string receiver = Console.ReadLine()!;
            Console.Write("Please enter the sender:");
            string sender = Console.ReadLine()!;

            string transactionType;
            bool validType = false;
            do {
                Console.Write("Please enter the transaction type (Transfer/Expense/Income):");
                transactionType = Console.ReadLine()!;
                if (Enum.TryParse<TransactionType>(transactionType, true, out _)) {
                    validType = true;
                } else {
                    Console.WriteLine("Invalid transaction type. Please enter Transfer, Expense, or Income.");
                }
            } while (!validType);
            
            Transaction newTransaction = new Transaction(transactionAmount, transactionDate, description, receiver, sender, (TransactionType)Enum.Parse(typeof(TransactionType), transactionType));
            
            if(card is CreditCard creditCard){
                if(creditCard.PreCheckRemainCredit(transactionAmount, (TransactionType)Enum.Parse(typeof(TransactionType), transactionType))){
                    card.AddTransaction(newTransaction);
                    creditCard.AddPoints(transactionAmount);
                }
            }
            else if(card is DebitCard debitCard){
                if(debitCard.PreCheckRemainBalance(transactionAmount, (TransactionType)Enum.Parse(typeof(TransactionType), transactionType))){
                    card.AddTransaction(newTransaction);
                }
            }
        }
        /// <summary>
        /// This static method displays the user edit transaction menu, handles user choices (with error handling) and has the parameter of card to track the selected card.
        /// </summary>
        static void EditTransactionMenu(Card card){
            int i =1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Editing a transaction");
            Console.WriteLine("Total number of transaction: " + card.Transactions.Count);
            foreach(Transaction transaction in card.Transactions){
                Console.WriteLine("Transaction " + i +" ID : "+ transaction.TransactionID);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the transaction ID:");
            string transactionID = Console.ReadLine()!;
            card.EditTransaction(transactionID);
        }
        /// <summary>
        /// This static method displays the user remove transaction menu, handles user choices (with error handling) and has the parameter of card to track the selected card.
        /// </summary>
        static void RemoveTransactionMenu(Card card){
            int i = 1;
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Removing a transaction");
            Console.WriteLine("Total number of transaction: " + card.Transactions.Count);
            foreach(Transaction transaction in card.Transactions){
                Console.WriteLine("Transaction " + i +" ID : "+ transaction.TransactionID);
                i++;
            }
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.Write("Please enter the transaction ID:");
            string transactionIDRemove = Console.ReadLine()!;
            card.RemoveTransaction(transactionIDRemove);
        }
        /// <summary>
        /// This static method displays the user view reward point menu (credit card), handles user choices (with error handling) and has the parameter of creditCard to track the selected credit card.
        /// </summary>
        static void DisplayRewardsPointsMenu(CreditCard creditCard){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Rewards Points: " + creditCard.Points);
            Console.WriteLine("---------------------------------------------------------------------------------");
            creditCard.DisplayReward();
        }
        /// <summary>
        /// This static method displays the user pay the bills charged on the credit card, handles user choices (with error handling) and has the parameter of creditCard to track the selected credit card.
        /// </summary>
        static void PayCreditCardBillMenu(CreditCard creditCard){
            Console.WriteLine("---------------------------------------------------------------------------------");
            Console.WriteLine("Paying Credit Card Bill");
            Console.WriteLine("---------------------------------------------------------------------------------");
            creditCard.CalculateChargedAmount();
            
            double paymentAmount = 0;
            bool validAmount = false;
            do {
                Console.Write("Please enter the amount to pay:");
                if (double.TryParse(Console.ReadLine(), out paymentAmount)) {
                    validAmount = true;
                } else {
                    Console.WriteLine("Invalid amount. Please enter a numeric value.");
                }
            } while (!validAmount);
            
            creditCard.PayBill(paymentAmount);
            creditCard.ResetCredit();
        }
        /// <summary>
        /// This static method displays the user report menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void ReportMenu(User user){
            bool exit = false;
            while(!exit){
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("Report Management");
                Console.WriteLine("---------------------------------------------------------------------------------");
                Console.WriteLine("1. Monthly Expenses vs Earning Report");
                Console.WriteLine("2. Annual Expenses vs Earning Report");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Your choice: ");
                string option = Console.ReadLine()!;
                
                switch(option){
                    case "1":
                        MonthlyReportMenu(user);
                        break;
                    case "2":
                        AnnualReportMenu(user);
                        break;
                    case "3":
                        Console.WriteLine("Going back to Main Menu.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        /// <summary>
        /// This static method displays the user monthly report menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void MonthlyReportMenu(User user){
            Console.WriteLine("---------------------------------------------------------------------------------");
            int month = 0;
            bool validMonth = false;
            do {
                Console.Write("Enter the month (Ex: January=1) :");
                if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12) {
                    validMonth = true;
                } else {
                    Console.WriteLine("Invalid month. Please enter a number between 1 and 12.");
                }
            } while (!validMonth);
            
            int year = 0;
            bool validYear = false;
            do {
                Console.Write("Enter the year (Ex: 2023) :");
                if (int.TryParse(Console.ReadLine(), out year) && year > 0) {
                    validYear = true;
                } else {
                    Console.WriteLine("Invalid year. Please enter a positive number.");
                }
            } while (!validYear);
            
            user.ViewReport(true, year, month);
        }
        /// <summary>
        /// This static method displays the user annual report menu, handles user choices (with error handling) and has the parameter of user to track the logged in user.
        /// </summary>
        static void AnnualReportMenu(User user){
            Console.WriteLine("---------------------------------------------------------------------------------");
            int year = 0;
            bool validYear = false;
            do {
                Console.Write("Enter the year (Ex: 2023) :");
                if (int.TryParse(Console.ReadLine(), out year) && year > 0) {
                    validYear = true;
                } else {
                    Console.WriteLine("Invalid year. Please enter a positive number.");
                }
            } while (!validYear);
            
            user.ViewReport(false, year, null);
        }
    }
}

