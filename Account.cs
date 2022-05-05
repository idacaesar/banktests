/*
Man ska kunna utföra transaktioner (insättning/uttag), hämta kontonummer, samt presentera kontot (visa kontonummer, saldo, kontotyp).
Implementera metoder som säkerställer ovanstående krav i klassen Bank. 
(Bank klassdesign inkluderar metoderna som ska användas. Komplettera
dessa med en implementation eller fler metoder om det behövs.)
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestverktygInlämning2
{
    public class Account
    {
        public int accountNumber { get; set; }     // det kan inte finnas flera konton med samma kontonummer
        public string accountType { get; set; }
        public float balance { get; set; }

        public override string ToString()
        {
            return accountNumber.ToString() + " " + accountType + " " + balance.ToString();
        }
    }
}
