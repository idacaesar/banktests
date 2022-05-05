/*

Man ska till exempel kunna ändra kundens namn samt hämta information om kunden (personnummer, för- och efternamn samt hämta information om kundens konton (kontonummer, saldo, kontotyp)). 
Dessutom ska man kunna hantera kundens konto(n). Implementera metoder som säkerställer ovanstående krav i klassen Bank nedan.
(Bank inkluderar förslag på metoder. Komplettera dessa med fler metoder om det behövs).
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestverktygInlämning2
{
    public class Customer
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string surName { get; set; }
        public string personalNumber { get; set; }

        public List<Account> customerAccounts;
    }
}

