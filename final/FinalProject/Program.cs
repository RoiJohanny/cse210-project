using System;
using System.Collections.Generic;

// Abstraction
public abstract class Account
{
    private string accountNumber;
    protected double balance;
    protected List<string> transactionHistory;

    public Account(string accountNumber)
    {
        this.accountNumber = accountNumber;
        balance = 0;
        transactionHistory = new List<string>();
    }

    public void Deposit(double amount)
    {
        balance += amount;
        transactionHistory.Add($"Deposit: +{amount}");
        Console.WriteLine($"{amount} deposited successfully.");
    }

    public virtual void Withdraw(double amount)
    {
        if (amount <= balance)
        {
            balance -= amount;
            transactionHistory.Add($"Withdrawal: -{amount}");
            Console.WriteLine($"{amount} withdrawn successfully.");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public abstract void PrintStatement();
}

// Inheritance
public class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber) : base(accountNumber) { }

    public override void PrintStatement()
    {
        Console.WriteLine($"Savings Account Statement: Balance = {balance}");
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }
    }
}

public class CheckingAccount : Account
{
    public CheckingAccount(string accountNumber) : base(accountNumber) { }

    public override void PrintStatement()
    {
        Console.WriteLine($"Checking Account Statement: Balance = {balance}");
        Console.WriteLine("Transaction History:");
        foreach (var transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }
    }
}

// Authentication
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}

// Account Management
public class Bank
{
    private Dictionary<string, Account> accounts;
    private Dictionary<string, User> users;

    public Bank()
    {
        accounts = new Dictionary<string, Account>();
        users = new Dictionary<string, User>();
    }

    public void AddAccount(string username, string password, Account account)
    {
        User user = new User(username, password);
        users.Add(username, user);
        accounts.Add(username, account);
        Console.WriteLine("Account created successfully.");
    }

    public Account Authenticate(string username, string password)
    {
        if (users.ContainsKey(username) && users[username].Password == password)
        {
            Console.WriteLine("Authentication successful.");
            return accounts[username];
        }
        else
        {
            Console.WriteLine("Authentication failed. Invalid username or password.");
            return null;
        }
    }
}

// Polymorphism
class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();
        bank.AddAccount("user1", "password1", new SavingsAccount("SA001"));
        bank.AddAccount("user2", "password2", new CheckingAccount("CA001"));

        Account authenticatedAccount = bank.Authenticate("user1", "password1");
        if (authenticatedAccount != null)
        {
            authenticatedAccount.Deposit(1000);
            authenticatedAccount.Withdraw(500);
            authenticatedAccount.PrintStatement();
        }
    }
}
