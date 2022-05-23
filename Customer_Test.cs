using Xunit;
using TestverktygInlämning2;
using System.Collections.Generic;

namespace banktests;

public class Ida_Customer_Test
{
  [Fact]
  public void set_id_wasSet()
  {
    // Arrange
    var customer = new Customer();

    // Act
    customer.id = 123;

    // Assert
    Assert.Equal(123, customer.id);
  }

  [Fact]
  public void set_firstName_wasSet()
  {
    // Arrange
    var customer = new Customer();

    // Act
    customer.firstName = "Bennny";

    // Assert
    Assert.Equal("Bennny", customer.firstName);
  }

  [Fact]
  public void set_surName_wasSet()
  {
    // Arrange
    var customer = new Customer();

    // Act
    customer.surName = "Karlsson";

    // Assert
    Assert.Equal("Karlsson", customer.surName);
  }

  [Fact]
  public void set_personalNumber_wasSet()
  {
    // Arrange
    var customer = new Customer();

    // Act
    customer.personalNumber = "123456";

    // Assert
    Assert.Equal("123456", customer.personalNumber);
  }

  [Fact]
  public void setAndAdd_customerAccounts_wasAdded()
  {
    // Arrange
    var customer = new Customer();

    // Act
    customer.customerAccounts = new List<Account>();

    var account1 = new Account();
    account1.accountNumber = 12345;
    account1.accountType = "Test";
    account1.balance = 345.6F;

    var account2 = new Account();
    account2.accountNumber = 9987;
    account2.accountType = "Test2";
    account2.balance = 999.6F;

    // Lägg till båda kontona i kundens kontolista 
    customer.customerAccounts.Add(account1);
    customer.customerAccounts.Add(account2);

    // Assert
    Assert.Equal(2, customer.customerAccounts.Count);
    Assert.Equal("Test", customer.customerAccounts[0].accountType);
  }
}
