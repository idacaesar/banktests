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
    var customers = bank.GetCustomers();

    Assert.Equal(3, customers.Count);
  }

  //[Fact]
  //public void GetAccount_accountData()
  //{
  // Arrange
  //var bank = new Bank();

  // var currentDirectory = Directory.GetCurrentDirectory();
  //bank.Load(currentDirectory + "/../../../data.txt");

  // Act
  //var result = bank.GetAccount("1343113", 1005);

  // Assert
  // Assert.Equal("Manuel", );
  //}

}
