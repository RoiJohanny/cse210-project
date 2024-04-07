using System;
using System.Collections.Generic;

public class Bank
{
    private List<Account> accounts;

    public Bank()
    {
        accounts = new List<Account>();
    }

    public Account CreateAccount(string accountHolderName)
    {
        Account newAccount = new Account(accountHolderName);
        accounts.Add(newAccount);
        return newAccount;
    }

    public void PrintAllStatements()
    {
        foreach (Account account in accounts)
        {
            account.PrintStatement();
            Console.WriteLine();
        }
    }
}

public class Account
{
    private static int accountCount = 0;

    public string AccountNumber { get; private set; }
    public string AccountHolderName { get; private set; }
    public double Balance { get; private set; }

    private List<string> transactionHistory;

    public Account(string accountHolderName)
    {
        AccountNumber = GenerateAccountNumber();
        AccountHolderName = accountHolderName;
        Balance = 0;
        transactionHistory = new List<string>();
    }

    private string GenerateAccountNumber()
    {
        accountCount++;
        return "ACC" + accountCount.ToString().PadLeft(4, '0');
    }

    public void Deposit(double amount)
    {
        Balance += amount;
        transactionHistory.Add($"Deposit: +{amount:C}. Balance: {Balance:C}");
    }

    public bool Withdraw(double amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }

        Balance -= amount;
        transactionHistory.Add($"Withdrawal: -{amount:C}. Balance: {Balance:C}");
        return true;
    }

    public void PrintStatement()
    {
        Console.WriteLine($"Account Number: {AccountNumber}");
        Console.WriteLine($"Account Holder: {AccountHolderName}");
        Console.WriteLine($"Balance: {Balance:C}");
        Console.WriteLine("Transaction History:");
        foreach (string transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Bank bank = new Bank();

        Account account1 = bank.CreateAccount("John Doe");
        account1.Deposit(1000);
        account1.Withdraw(200);

        Account account2 = bank.CreateAccount("Jane Smith");
        account2.Deposit(500);
        account2.Withdraw(100);

        bank.PrintAllStatements();
    }
}
