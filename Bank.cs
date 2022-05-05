/*
Klassen Bank ska innehålla en lista med alla kunder.
Klassen ska innehålla ett antal metoder som hanterar kunder och dess konton.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestverktygInlämning2
{
    public class Bank
    {
        List<Customer> customerList;

        // Läser in text filen och befolkar listan som ska innehålla kunderna.
        public virtual void Load(string path)
        {
            customerList = new List<Customer>();
            string[] lines = System.IO.File.ReadAllLines(path);
            List<string> list = new List<string>();
            foreach (string line in lines)
            {
                list = line.Split(':').ToList();

                string personalNumber = list[2];
                AddCustomer(list[1], personalNumber);
                Customer customer = GetCustomer(personalNumber);
                
                customer.customerAccounts = new List<Account>();

                int index = 3;
                while (index < list.Count-1) {
                    Account account = new Account()
                    {
                        accountNumber = int.Parse(list[index]),
                        accountType = list[index+1],
                        balance = float.Parse(list[index + 2], CultureInfo.InvariantCulture.NumberFormat)
                    };
                    customer.customerAccounts.Add(account);
                    index += 3;
                }
            }
        }

        // Returnerar bankens alla kunder (personnummer och namn).
        public virtual List<Customer> GetCustomers()
        {
            return this.customerList;
        }


        /*
        Skapar en ny kund med namn och personnummer. Kunden skapas endast om det inte finns någon kund med personnumret som angetts. Returnerar True om kunden skapades annars returneras False.
        */
        public virtual bool AddCustomer(string Name, string PersonalNumber)
        {
            bool successfull = false;
            Customer cust = new Customer();
            cust.firstName = Name;
            cust.personalNumber = PersonalNumber;
            this.customerList.Add(cust);
            return successfull;
        }

        public virtual Customer GetCustomer(string PersonalNumber)
        {
            Customer resultCustomer = null;
            foreach (Customer customer in this.customerList)
            {
                if (customer.personalNumber == PersonalNumber)
                {
                    resultCustomer = customer;
                    break;
                }
            }
            return resultCustomer;
        }

        /*
        Returnerar information om kunden inklusive dennes konton. Första platsen i listan är förslagsvis reserverad för kundens namn och personnummer sedan följer informationen om kundens konton.
        */
        public virtual List<string> GetCustomerInfo(string PersonalNumber)
        {
            List<string> customerInfo = new List<string>();
            Customer customer = GetCustomer(PersonalNumber);
            customerInfo.Add(customer.firstName);
            customerInfo.Add(customer.personalNumber);
            foreach (Account account in customer.customerAccounts)
            {
                customerInfo.Add(account.ToString());
            }
            return customerInfo;
        }

        /*
        Byter namn på kund, returnerar True om namnet ändrades annars returnerar det False (om kunden inte fanns).
        */
        public virtual bool ChangeCustomerName(string Name, string PersonalNumber)
        {
            bool successful = false;
            Customer customer = GetCustomer(PersonalNumber);
            if (customer != null)
            {
                customer.firstName = Name;
                successful = true;
            }
            return successful;
        }

        /*
        Tar bort kund med personnumret som angetts ur banken, alla kundens eventuella konton tas också bort och resultatet returneras. Listan som returneras ska innehålla information om alla konton som togs bort, saldot som kunden får tillbaka.
        */
        public virtual void RemoveCustomer(string PersonalNumber)
        {
            Customer customer = GetCustomer(PersonalNumber);
            customerList.Remove(customer);
        }

        /*
        Skapar ett konto till kunden med personnumret som angetts, returnerar kontonumret som det skapade kontot fick alternativt returneras –1 om inget konto skapades.
        */
        public virtual int AddAccount(string PersonalNumber)
        {
            Customer customer = GetCustomer(PersonalNumber);

            int accountCode = -1;
            if (PersonalNumber != "")
            {
                Random rnd = new Random();
                accountCode = rnd.Next(1000, 1999);
                customer.customerAccounts.Add(new Account() { accountNumber = accountCode, accountType = "debit", balance = 0});
            }
            return accountCode;
        }

        public virtual Account GetAccount(string PersonalNumber, int AccountId)
        {
            Customer customer = GetCustomer(PersonalNumber);
            Account gottenAccount = null;

            if (customer != null)
            {
                foreach (Account account in customer.customerAccounts)
                {
                    if (account.accountNumber == AccountId)
                    {
                        gottenAccount = account;
                        break;
                    }
                }
            }

            return gottenAccount;
        }

        /*
        Returnerar Textuell presentation av kontot med kontonummer som tillhör kunden (kontonummer, saldo, kontotyp).
        */
        public virtual string GetAccountInfo(string PersonalNumber, int AccountId)
        {
            string accountDetails = "";
            return accountDetails;
        }

        // Gör en insättning på kontot, returnerar true om det gick bra annars false.
        public virtual bool Deposit(string PersonalNumber, int AccountId, float Amount)
        {
            Account account = GetAccount(PersonalNumber, AccountId);
            bool successfull = false;
            if (account != null)
            {
                account.balance += Amount;
                successfull = true;
            }
            return successfull;
        }

        // Gör ett uttag på kontot, returnerar true om det gick bra annars false.
        public virtual bool Withdraw(string PersonalNumber, int AccountId, float Amount)
        {
            bool successfull = false;
            return successfull;
        }

        // Avslutar ett konto. Textuell presentation av kontots saldo ska genereras och returneras.
        public virtual string CloseAccount(string PersonalNumber, int AccountId)
        {
            return "";
        }
    }
}
