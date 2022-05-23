using Xunit;
using TestverktygInlämning2;
using System.Collections.Generic;
using Xunit.Abstractions;
using System.IO;
using System.Linq;
using System;
using Moq;

namespace banktests;

public class Ida_Bank_Test
{
  private ITestOutputHelper _testOutputHelper;

  public Ida_Bank_Test(ITestOutputHelper testOutputHelper)
  {
    _testOutputHelper = testOutputHelper;
  }

  [Fact]
  public void getCustomers_dataWasReturned()
  {
    // Arrange
    var bank = new Bank();

    _testOutputHelper.WriteLine("Loading data");
    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var customers = bank.GetCustomers();

    // Assert
    Assert.Equal(3, customers.Count);
    Assert.Equal("19911111", customers[0].personalNumber);
  }

  [Fact]
  public void load_wrongPath_errorReturned()
  {
    // Arrange
    var bank = new Bank();

    // Act and Assert
    _testOutputHelper.WriteLine("Loading data from wrong path");
    Assert.Throws<FileNotFoundException>(() => bank.Load("ootasctx.txt"));
  }

  [Theory]
  [InlineData("Linda", "0101011904")]
  [InlineData("Bruno", "0202021903")]
  public void addCustomer_returnsTrue(string name, string personalNumber)
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddCustomer(name, personalNumber);
    _testOutputHelper.WriteLine("Adding customer with name " + name + " and personalnumber " + personalNumber);


    // Assert
    Assert.Equal(true, result);
  }

  [Theory]
  [InlineData("Linda", "0101011904")]
  [InlineData("Bruno", "0202021903")]
  public void addCustomer_customerWasAdded(string name, string personalNumber)
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddCustomer(name, personalNumber);

    // Assert
    var customers = bank.GetCustomers();

    Assert.Equal(4, customers.Count);
    Assert.Equal(personalNumber, customers[3].personalNumber);
    _testOutputHelper.WriteLine("Comparing expected personalnumber " + personalNumber + " with actual personalnumber " + customers[3].personalNumber);

  }


  [Fact]
  public void addCustomer_twoCustomersWithSamePersonalNumber_returnsFalse()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result1 = bank.AddCustomer("Malte", "150204");
    var result2 = bank.AddCustomer("Karl", "150204");

    // Assert
    Assert.Equal(true, result1);
    Assert.Equal(false, result2);
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
  public void getCustomerInfo_wrongPersonalNumber_throwsException()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act and assert
    Assert.Throws<NullReferenceException>(() => bank.GetCustomerInfo("19760000"));
  }

  [Fact]
  public void GetCustomer_customerDataReturned()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.GetCustomer("19860107");

    // Assert
    Assert.Equal("19860107", result.personalNumber);
    Assert.Equal("Linnea", result.firstName);
  }

  [Fact]
  public void GetCustomer_wrongPersonalNumber_returnsNull()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.GetCustomer("1987777");

    // Assert
    Assert.Null(result);
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
  public void removeCustomer_wrongPersonalNumber_noCustomerRemoved()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    bank.RemoveCustomer("19768888");

    // Assert
    var customers = bank.GetCustomers();
    Assert.Equal(3, customers.Count);
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
  public void AddAccount_emptyPersonalNumber_returnsMinus1()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.AddAccount("");

    // Assert
    Assert.Equal(-1, result);
  }

  [Fact]
  public void GetAccount_accountDataReturned()
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
  public void GetAccount_wrongAccountId_returnsNull()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    Account result = bank.GetAccount("19760314", 9999);

    // Assert
    Assert.Null(result);
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
  public void GetAccountInfo_mock_returnsStringinfo()
  {
    // Arrange
    var bankMock = new Moq.Mock<Bank>();
    bankMock.Setup(b => b.GetAccountInfo(It.IsAny<string>(), It.IsAny<int>())).Returns("1001 5000 debit");

    var bank = bankMock.Object;


    // Act
    var result = bank.GetAccountInfo("19860107", 1003);

    // Assert
    Assert.Equal("1001 5000 debit", result);
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
  public void Deposit_wrongAccountId_returnsFalse()
  {
    // Arrange
    var bank = new Bank();

    var currentDirectory = Directory.GetCurrentDirectory();
    bank.Load(currentDirectory + "/../../../data.txt");

    // Act
    var result = bank.Deposit("19911111", 3333, 5000);

    // Assert
    Assert.Equal(false, result);
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
  public void Withdraw_mock_returnsTrue()
  {
    // Arrange
    var bankMock = new Moq.Mock<Bank>();
    bankMock.Setup(b => b.Withdraw(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<float>())).Returns(true);

    var bank = bankMock.Object;

    // Act
    var result = bank.Withdraw("19911111", 1001, 1000);

    // Assert
    Assert.Equal(true, result);
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
