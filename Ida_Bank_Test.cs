using Xunit;
using TestverktygInlämning2;
using System.Collections.Generic;
using Xunit.Abstractions;

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
    bank.Load("/Users/idacaesar/Documents/GitHub/banktests/data.txt");

    // Act
    var customers = bank.GetCustomers();

    // Assert
    //System.Console.WriteLine(customers[0].personalNumber);
    Assert.Equal(3, customers.Count);
    Assert.Equal("19911111", customers[0].personalNumber);
  }
}
