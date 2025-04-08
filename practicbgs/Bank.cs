using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicbgs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Bank
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
                return clients[clientFullName].Accounts.Sum(a => a.Balance);
            }
            return 0;
        }

        public static decimal GetTotalBalanceForAllClients()
        {
            return clients.Values.Sum(client => GetTotalBalanceForClient(client.FullName));
        }

        public static void DisplayCreditAccounts()
        {
            Console.WriteLine("\nКредитные счета:");
            foreach (var client in clients.Values)
            {
                foreach (var account in client.Accounts.Where(a => a.Type == AccountType.Credit))
                {
                    Console.WriteLine($"Клиент: {client.FullName}, Счет: {account.Type}, Баланс: {account.Balance}");
                }
            }
        }

        public static void DisplayDebitAccounts()
        {
            Console.WriteLine("\nДебетовые счета:");
            foreach (var client in clients.Values)
            {
                foreach (var account in client.Accounts.Where(a => a.Type == AccountType.Debit))
                {
                    Console.WriteLine($"Клиент: {client.FullName}, Счет: {account.Type}, Баланс: {account.Balance}");
                }
            }
        }

        public static void GenerateRandomClients(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                string fullName = $"Клиент_{i + 1}";
                CreateClient(fullName);
                int accountCount = random.Next(1, 4); // Создать от 1 до 3 счетов
                for (int j = 0; j < accountCount; j++)
                {
                    CreateAccount(fullName, (AccountType)random.Next(0, 2), random.Next(1000, 10000));
                }
            }
        }

        public static void LoadData()
        {
            var data = DataManager.LoadData();
            if (data != null)
            {
                clients = data;
                Console.WriteLine("Данные успешно загружены.");
            }
            else
            {
                Console.WriteLine("Не удалось загрузить данные.");
            }
        }

        public static void SaveData()
        {
            DataManager.SaveData(clients);
        }
    }

