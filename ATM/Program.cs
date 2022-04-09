using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Program
    {
        BankAccount selectedBankAccount = null;

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
            Console.WriteLine("1.Insert card");
            Console.WriteLine("2.Withdraw card");
            Console.WriteLine("3.Block card");
        }

        void ShowCardOptions()
        {
            Console.WriteLine("1.Withdraw money");
            Console.WriteLine("2.Deposit");
            Console.WriteLine("3.Pay bills");
            Console.WriteLine("4.Show balance");
        }
        void ShowBalance(int amount)
        {
            if (amount != null) Console.WriteLine("Your current balance is: " + amount);
            else Console.WriteLine("Your current balance is null");
        }
        void DepositMoney()
        {
            Console.WriteLine("Insert the amount you want to deposit:");
            int amount;
            if (int.TryParse(Console.ReadLine(), out amount))
            {
                selectedBankAccount.Balance += amount;
                Console.WriteLine("Operation great success");
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
                            catch (BankAccountNotFound ex)
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

    class BankAccountNotFound : Exception
    {

    }
}
