﻿using Microsoft.IdentityModel.Protocols;
using Serialization.Models;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Text.Json;

namespace Serialization
{
    internal class Program
    {
        public static Account account = null;
        public static Account[] DataBase = new Account[0];
        public static string path = ConfigurationManager.AppSettings["Path"].ToString();

        static void Main(string[] args)
        {

            //Console.WriteLine((File.Exists(path)) ? Deserialization(path) : Serialization(path));

            if (File.Exists(path))
            {
                LoadAccount();
            }
            else
            {
                CreateAccount();
            }

        }

        public static void CreateAccount()
        {
            Console.WriteLine("Enter account number:");
            string number = Console.ReadLine();
            Console.WriteLine("Enter Account holder name :");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Opening balance should be non negative");
            double balance = double.Parse(Console.ReadLine());

            account = new Account(number,balance,name);

            FileStream fs = File.Create(path);
            fs.Close();// I was getting error of file is used by other process which means after creating file
            // the process/file not getting closed 
            Serializer.Serialize(path);
            ChangeOrAddAccountMenu();

            Resize(account);
        }

        public static void LoadAccount() { 
            DataBase = Serializer.Deserialization(path);
            ChangeOrAddAccountMenu();
        }

        public static void ChangeOrAddAccountMenu()
        {
            bool[] run = { true };
            while (run[0])
            {
                Console.WriteLine($"\n0.Exit\n" +
                    $"1. Create new Account\n" +
                    $"2. Change Account\n");

                int choice = int.Parse(Console.ReadLine());
                ExecuteCases2(choice,run);
            }

        }

        public static void ExecuteCases2(int choice,bool[] run)
        {
            switch (choice)
            {
                case 0:
                    Serializer.Serialize(path);
                    run[0] = false;
                    break;
                    
                case 1:
                    CreateAccount();
                    Console.WriteLine($"Welcome {account.AccountHolderName}");
                    DisplayMenu();
                    break;
                    
                case 2:
                    ManipulateAccount();
                    break;

                default:
                    Console.WriteLine("Please choose correct option");
                    break;
            }
        }

        public static void ManipulateAccount()
        {
            var count = 1;
            foreach (var acc in DataBase)
            {
                Console.WriteLine($"{count++}=> {acc.AccountHolderName}");
            }
            Console.WriteLine("Enter the index number of the account to be used");
            int choice = int.Parse(Console.ReadLine());
            account = DataBase[choice-1];
            Console.WriteLine($"\nWelcome {account.AccountHolderName}");
            DisplayMenu();
        }

        public static void DisplayMenu()
        {
            bool[] run = { true };
            while (run[0])
            {
                Console.WriteLine($"\n1.Deposite\n" +
                    $"2.withdraw\n" +
                    $"3.Display Balance\n" +
                    $"4.Show account details\n" +
                    $"5.Exit\n");
                int choice = int.Parse( Console.ReadLine() );
                ExecuteCases(choice,run);
            }
        }

        public static void ExecuteCases(int choice,bool[] run)
        {
            switch (choice) {
                case 1:
                    Deposite();
                    break;
                    
                case 2:
                    Withdraw();
                    break;
                    
                case 3:
                    Console.WriteLine(account.GetBalance());
                    break;
                    
                case 4:
                    Console.WriteLine(account.ToString());
                    break;

                 case 5:
                    run[0]= false;
                    break;

                default:
                    Console.WriteLine("Please choose correct option");
                    break;
            }
        }

        public static void Deposite()
        {
            Console.WriteLine("Enter amount to be deposite");
            double amount = double.Parse(Console.ReadLine());
            Console.WriteLine(account.Deposite(amount));
        }
        public static void Withdraw()
        {
            Console.WriteLine("Enter amount to be Withdraw");
            double amount = double.Parse(Console.ReadLine());
            Console.WriteLine(account.Withdrawal(amount));
        }

        public static void Resize(Account account)
        {
            Array.Resize(ref DataBase, DataBase.Length + 1);
            DataBase[DataBase.Length-1] = account;
        }

    }
}
