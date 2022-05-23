using Xunit;
using TestverktygInlämning2;
using System.Globalization;

namespace banktests;

public class Ida_Account_Test
{
  // Testatanrop_indata_förväntatresultat
  [Fact]
  public void setAccountNumber_wasSet()
  {
    // Arrange
    var account = new Account();

    // Act
    account.accountNumber = 12354;

    // Assert
    Assert.Equal(12354, account.accountNumber);

  }

  [Fact]
  public void setAccountType_wasSet()
  {
    // Arrange
    var account = new Account();

    // Act
    account.accountType = "Test";

    // Assert
    Assert.Equal("Test", account.accountType);

  }

  [Fact]
  public void setBalance_wasSet()
  {
    // Arrange
    var account = new Account();

    // Act
    account.balance = 123.5F;

    // Assert
    Assert.Equal(123.5F, account.balance);

  }

  [Fact]
  public void ToString_returnsCorrectly()
  {
    // Arrange
    CultureInfo.CurrentCulture = new CultureInfo("en-GB");

    var account = new Account();
    account.accountNumber = 12345;
    account.accountType = "Test";
    account.balance = 345.6F;

    // Act
    var actual = account.ToString();

    // Assert
    Assert.Equal("12345 Test 345.6", actual);

  }

  [Fact]
  public void ToString_otherData_returnsCorrectly()
  {
    // Arrange
    CultureInfo.CurrentCulture = new CultureInfo("en-GB");

    var account = new Account();
    account.accountNumber = 11111;
    account.accountType = "Hej";
    account.balance = 123.4F;

    // Act
    var actual = account.ToString();

    // Assert
    Assert.Equal("11111 Hej 123.4", actual);

  }
}
