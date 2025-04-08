using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practicbgs
{
    using System.Collections.Generic;

    public class Client
    {
        public string FullName { get; }
        public List<Account> Accounts { get; } = new List<Account>();

        // Исправлено имя конструктора на Client
        public Client(string fullName)
        {
            FullName = fullName;
        }
    }
}
