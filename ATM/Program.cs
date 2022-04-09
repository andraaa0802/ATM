using System;
using System.Collections.Generic;
using System.Linq;
namespace ConsoleApp2
{
    class Program
    {
        BankAccount selectedBankAccount = null;
        Bill selectedBill=null;

        class BankAccount
        {
            public string OwnerName { get; set; }
            public string Id { get; set; }
            public int Balance { get; set; }

            public BankAccount(string OwnerName, string Id, int Balance)
            {
                this.OwnerName = OwnerName;
                this.Id = Id;
                this.Balance = Balance;
            }
        }
        class Bill
        {
            public string Company { get; set; }
            public string Id { get; set; }  
            public int Value { get; set; }

            public Bill (string Company, string Id, int Value)
            {
                this.Company = Company;
                this.Id = Id;
                this.Value = Value;
            }
        }
        
       
        List<BankAccount> accounts;

        Program()
        {
            accounts = new List<BankAccount>
            {
                new BankAccount("Popescu Maria", "4958", 1230),
                new BankAccount("Marcu Ionut", "3571", 5015),
                new BankAccount("Pop Ioana", "1256", 9560),
                new BankAccount("Vidican Elena", "8642", 452),
                new BankAccount("Badea Mihai", "9136", 1963)
            };
        }

        void ShowUserOptions()
        {
            Console.WriteLine("\n1.Insert card");
            Console.WriteLine("2.Withdraw card");
            Console.WriteLine("3.Block card\n");
        }

        void ShowCardOptions()
        {
            Console.WriteLine("\n1.Withdraw money");
            Console.WriteLine("2.Deposit");
            Console.WriteLine("3.Pay bills");
            Console.WriteLine("4.Show balance\n");
        }
        void ShowBalance(int amount)
        {
            Console.WriteLine("Your current balance is: " + amount);
           
        }
        void DepositMoney()
        {
            Console.WriteLine("Insert the amount you want to deposit:");
            int amount;
            if (int.TryParse(Console.ReadLine(), out amount))
            {
                selectedBankAccount.Balance += amount;
                Console.WriteLine("Operation great success");
                Console.WriteLine("Your new balance is: " +selectedBankAccount.Balance);
            }
        }
        void WithdrawMoney()
        {
            Console.WriteLine("Insert the amount you want to withdraw");

            int amount;
            if (int.TryParse(Console.ReadLine(), out amount))
            {
                if (selectedBankAccount.Balance <= amount)
                {
                    Console.WriteLine("Not enough funds");
                    return;
                }

                selectedBankAccount.Balance -= amount;
                Console.WriteLine("Operation great success");
                Console.WriteLine("Your new balance is: " + selectedBankAccount.Balance);
            }
        }
        List<Bill> bills = new List<Bill>
        {
            new Bill("Digi","123",220),
            new Bill("Digi","442",185),
            new Bill("Digi","336",280),
            new Bill("Orange","652",120),
            new Bill("Orange","174",420)
        };
        void PayBill()
        {
            Console.WriteLine("Available bills: ");
            Console.WriteLine("1. Digi, id - 123");
            Console.WriteLine("2. Digi, id - 442");
            Console.WriteLine("3. Digi, id - 336");
            Console.WriteLine("4. Orange, id - 652");
            Console.WriteLine("5. Orange, id - 174");

            Console.WriteLine("Insert the bill's ID: ");
            var id=Console.ReadLine();
            selectedBill = null;
            foreach(var Bill in bills)
            {
                if (Bill.Id != id)
                    continue;
                selectedBill= Bill;
                break;
            }
            if (selectedBill == null)
                throw new BillNotFound();
            else
            {
               
                if (selectedBill.Value == 0)
                    Console.WriteLine("This bill was already paid");
                else
                {   
                    int sum = selectedBill.Value;
                    Console.WriteLine("You have to pay: " + sum);
                    
                    if (sum > selectedBankAccount.Balance)
                        Console.WriteLine("Insufficient funds");
                    else
                    {
                        selectedBill.Value -= sum;
                        selectedBankAccount.Balance -= sum;
                        Console.WriteLine("Operation great success");
                        Console.WriteLine("Your new balance is: " + selectedBankAccount.Balance);
                    }
                }
            }
        }
        void BlockCard()
        {
            Console.WriteLine("Insert the card and its PIN:");
            var pin = Console.ReadLine();

            selectedBankAccount = null;

            foreach (var bankAccount in accounts)
            {
                if (bankAccount.Id != pin)
                    continue;

                selectedBankAccount = bankAccount;
                break;
            }

            if (selectedBankAccount == null)
                throw new BankAccountNotFound();
            else
            {
                var itemToRemove = accounts.Single(r => r.Id == pin);
                if (itemToRemove!=null) accounts.Remove(itemToRemove);
                Console.WriteLine("Yor card is blocked now");
            }
        }
        void InsertCard()
        {
            Console.WriteLine("Card inserted, please provide a PIN");
            var pin = Console.ReadLine();

            selectedBankAccount = null;

            foreach (var bankAccount in accounts)
            {
                if (bankAccount.Id != pin)
                    continue;

                selectedBankAccount = bankAccount;
                break;
            }

            if (selectedBankAccount == null)
                throw new BankAccountNotFound();

            ShowCardOptions();

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        WithdrawMoney();
                        break;
                    case 2:
                        DepositMoney();
                        break;
                    case 3:
                        PayBill();
                        break;
                    case 4:
                        ShowBalance(selectedBankAccount.Balance);
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Program myProgram = new Program();

            while (true)
            {
                myProgram.ShowUserOptions();
                var input = Console.ReadLine();
                int choice;

                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                myProgram.InsertCard();
                            }
                            catch (BankAccountNotFound)
                            {
                                Console.WriteLine("No account was found");
                            }
                            break;
                        case 2:
                            Console.WriteLine("Your card was withdrawn");
                            break;
                        case 3:
                            try
                            {
                                myProgram.BlockCard();
                            }
                            catch (BankAccountNotFound)
                            {
                                Console.WriteLine("No account was found");
                            }
                            break;

                    }
                }
                else
                {

                }
            }


        }
    }

    class BankAccountNotFound : Exception { }
    class BillNotFound : Exception { }
}
