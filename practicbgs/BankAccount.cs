using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace practicbgs
{
    using System;
    using System.Collections.Generic;
    using System.Security.Principal;
    using System.Text.Json;

    internal class BankAccount
    {
        private static Dictionary<string, Client> clients = new Dictionary<string, Client>();

        public static void CreateClient(string fullName)
        {
            if (!clients.ContainsKey(fullName))
            {
                clients[fullName] = new Client(fullName);
                Console.WriteLine($"Клиент {fullName} успешно создан.");
            }
            else
            {
                Console.WriteLine($"Клиент {fullName} уже существует.");
            }
        }

        public static void CreateAccount(string clientFullName, AccountType accountType, decimal initialBalance)
        {
            if (clients.ContainsKey(clientFullName))
            {
                var account = new Account(accountType, initialBalance);
                clients[clientFullName].Accounts.Add(account);
                Console.WriteLine($"Счет {accountType} для клиента {clientFullName} успешно создан.");
            }
            else
            {
                Console.WriteLine($"Клиент {clientFullName} не найден.");
            }
        }

        public static void DisplayAllClients()
        {
            Console.WriteLine("\nСписок всех клиентов:");
            foreach (var client in clients.Values)
            {
                Console.WriteLine($"Клиент: {client.FullName}");
                foreach (var account in client.Accounts)
                {
                    Console.WriteLine($"  Счет: {account.Type}, Баланс: {account.Balance}");
                }
            }
        }

        public static void DisplayClientAccounts(string clientFullName)
        {
            if (clients.ContainsKey(clientFullName))
            {
                Console.WriteLine($"\nСчета клиента {clientFullName}:");
                foreach (var account in clients[clientFullName].Accounts)
                {
                    Console.WriteLine($"  Счет: {account.Type}, Баланс: {account.Balance}");
                }
            }
            else
            {
                Console.WriteLine($"Клиент {clientFullName} не найден.");
            }
        }

        public static decimal GetTotalBalanceForClient(string clientFullName)
        {
            if (clients.ContainsKey(clientFullName))
            {
                decimal totalBalance = 0;
                foreach (var account in clients[clientFullName].Accounts)
                {
                    totalBalance += account.Balance;
                }
                return totalBalance;
            }
            return 0;
        }

        public static decimal GetTotalBalanceForAllClients()
        {
            decimal totalBalance = 0;
            foreach (var client in clients.Values)
            {
                totalBalance += GetTotalBalanceForClient(client.FullName);
            }
            return totalBalance;
        }

        public static void DisplayCreditAccounts()
        {
            Console.WriteLine("\nКредитные счета:");
            foreach (var client in clients.Values)
            {
                foreach (var account in client.Accounts)
                {
                    if (account.Type == AccountType.Credit)
                    {
                        Console.WriteLine($"Клиент: {client.FullName}, Счет: {account.Type}, Баланс: {account.Balance}");
                    }
                }
            }
        }

        public static void DisplayDebitAccounts()
        {
            Console.WriteLine("\nДебетовые счета:");
            foreach (var client in clients.Values)
            {
                foreach (var account in client.Accounts)
                {
                    if (account.Type == AccountType.Debit)
                    {
                        Console.WriteLine($"Клиент: {client.FullName}, Счет: {account.Type}, Баланс: {account.Balance}");
                    }
                }
            }
        }

        public static void SaveData(string filePath)
        {
            string jsonString = JsonSerializer.Serialize(clients);
            System.IO.File.WriteAllText(filePath, jsonString);
        }
    }

    public class Client
    {
        public string FullName { get; }
        public List<Account> Accounts { get; } = new List<Account>();

        public Client(string fullName)
        {
            FullName = fullName;
        }
    }

    public class Account
    {
        public AccountType Type { get; }
        public decimal Balance { get; private set; }

        public Account(AccountType type, decimal initialBalance)
        {
            Type = type;
            Balance = initialBalance;
        }
    }

    public enum AccountType
    {
        Debit,
        Credit
    }

