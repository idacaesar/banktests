using Xunit;
using TestverktygInlämning2;
using System.Collections.Generic;
using Xunit.Abstractions;
using System.IO;
using System.Linq;

namespace banktests;

public class Ida_Bank_Test
{
  private ITestOutputHelper testOutputHelper;

  public Ida_Bank_Test(ITestOutputHelper helper)
  {
    testOutputHelper = helper;
  }

  [Fact]
  public void getCustomers_dataWasLoaded()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var customers = bank.GetCustomers();

    // Assert
    Assert.Equal(3, customers.Count);
    Assert.Equal("19911111", customers[0].personalNumber);
  }

  [Fact]
  public void addCustomer_returnedTrue()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddCustomer("Linda", "0101011904");

    // Assert
    Assert.Equal(true, result);
  }

  [Fact]
  public void addCustomer_customerWasAdded()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddCustomer("Linda", "0101011904");

    // Assert
    var customers = bank.GetCustomers();

    Assert.Equal(4, customers.Count);
    Assert.Equal("0101011904", customers[3].personalNumber);
  }

  [Fact]
  public void getCustomerInfo_returnInfo()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.GetCustomerInfo("19760314");

    // Assert
    Assert.Equal("Manuel", result[0]);
    Assert.Equal("19760314", result[1]);
    Assert.Equal("1005 debit 200", result[2]);
  }

  [Fact]
  public void GetCustomer_customerData()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.GetCustomer("19860107");

    // Assert
    Assert.Equal("19860107", result.personalNumber);
  }

  [Fact]
  public void changeCustomerName_returnedTrue()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.ChangeCustomerName("Benny", "19911111");

    // Assert
    var customers = bank.GetCustomers();
    Assert.Equal(true, result);
    Assert.Contains(customers, customer => customer.personalNumber == "19911111" && customer.firstName == "Benny");
  }

  [Fact]
  public void changeCustomerName_missingPersonalNumber_returnsFalse()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.ChangeCustomerName("Benny", "19873015");

    // Assert
    Assert.Equal(false, result);
  }

  [Fact]
  public void removeCustomer_customerRemoved()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    bank.RemoveCustomer("19760314");

    // Assert
    var customers = bank.GetCustomers();
    Assert.Equal(2, customers.Count);
    Assert.DoesNotContain(customers, customer => customer.personalNumber == "19760314");
  }

  [Fact]
  public void AddAccount_accountAdded()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddAccount("19860107");

    // Assert
    var customer = bank.GetCustomer("19860107");

    Assert.Equal(3, customer.customerAccounts.Count);
  }

  [Fact]
  public void GetAccount_accountData()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    Account result = bank.GetAccount("19760314", 1005);

    // Assert
    Assert.Equal("debit", result.accountType);
    Assert.Equal(1005, result.accountNumber);
    Assert.Equal(200, result.balance);
  }

  [Fact]
  public void GetAccountInfo_returnInfo()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.GetAccountInfo("19860107", 1003);

    // Assert
    // Otydlig specifikation i kod, vi antar att metoden ska retunera en string som inte är null eller tom
    Assert.Equal(false, string.IsNullOrEmpty(result));
  }


  [Fact]
  public void Deposit_balanceIncreased()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.Deposit("19911111", 1001, 5000);

    // Assert
    var account = bank.GetAccount("19911111", 1001);
    Assert.Equal(10000, account.balance);
  }

  [Fact]
  public void Withdraw_balanceReduction()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.Withdraw("19911111", 1001, 1000);

    // Assert
    var account = bank.GetAccount("19911111", 1001);
    Assert.Equal(4000, account.balance);
  }

  [Fact]
  public void CloseAccount_accountClosed_returnsBalance()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.CloseAccount("19760314", 1005);

    // Assert
    Assert.Equal(false, string.IsNullOrEmpty(result));
  }

  [Fact]
  public void CloseAccount_accountClosed_accountWasRemoved()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.CloseAccount("19760314", 1005);

    // Assert
    var account = bank.GetAccount("19760314", 1005);
    Assert.Null(account);
  }

}