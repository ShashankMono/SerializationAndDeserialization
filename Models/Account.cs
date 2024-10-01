using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization.Models
{
    internal class Account
    {
        public string AccountNumber {  get; set; }
        public double  AccountBalance {  get; set; }
        public string AccountHolderName { get; set; }

        public Account(string AccountNumber, double AccountBalance, string AccountHolderName)
        {
            // The name of the variable should be same in the parameter because when deserialization happens
            // then the value should be properly assigned for that purpose the constructor parameter same should be 
            // same as the variable name
            this.AccountNumber = AccountNumber;
            this.AccountBalance = AccountBalance;
            this.AccountHolderName = AccountHolderName;
        }

        public string Deposite(double add)
        {
            AccountBalance += add;
            return $"Your have deposited {add} sucessfully and you current balance is: {AccountBalance}";
        }

        public string Withdrawal(double deduct) { 
            if(AccountBalance - deduct < 0)
            {
                return $"{deduct} cannot be withdraw Failed!";
            }
            AccountBalance -= deduct;
            return $"Sucessfully withdrawal {deduct} and current balance is: {AccountBalance}";
        }

        public string GetBalance()
        {
            return $"Your account balance is: {AccountBalance}";
        }

        public override string ToString()
        {
            return $"\nAccount number : {AccountNumber}\n" +
                $"Account holder name : {AccountHolderName}\n" +
                $"Account Balance : {AccountBalance}";
        }

    }
}
