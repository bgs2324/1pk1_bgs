using practicbgs;
using System;

class Program
{
    static void Main(string[] args)
    {
        Bank.LoadData();

        // Генерация случайных клиентов и счетов
        Bank.GenerateRandomClients(5);

        // Демонстрация работы методов
        Bank.DisplayAllClients();

        // Пример вывода информации по конкретному клиенту
        string clientToCheck = "Клиент_1"; // Измените на нужное имя клиента
        Bank.DisplayClientAccounts(clientToCheck);

        // Вывод суммы на всех счетах конкретного клиента
        decimal totalBalance = Bank.GetTotalBalanceForClient(clientToCheck);
        Console.WriteLine($"Общая сумма на счетах клиента {clientToCheck}: {totalBalance}");

        // Вывод суммы на всех счетах всех клиентов
        decimal totalBalanceAllClients = Bank.GetTotalBalanceForAllClients();
        Console.WriteLine($"Общая сумма на счетах всех клиентов: {totalBalanceAllClients}");

        // Вывод кредитных и дебетовых счетов
        Bank.DisplayCreditAccounts();
        Bank.DisplayDebitAccounts();

        // Сохранение данных перед завершением работы приложения
        Bank.SaveData();

        Console.WriteLine("Данные сохранены. Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}

