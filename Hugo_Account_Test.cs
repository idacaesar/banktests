using Xunit;
using TestverktygInlämning2;

namespace banktests;

public class Hugo_Account_Test
{
    // Testatanrop_indata_förväntatresultat
    [Fact]
    public void setAccountNumber_wasSet()
    {
        // Arrange
        var account = new Account();

        // Act
        account.accountNumber = 12345;

        // Assert
        Assert.Equal(12345, account.accountNumber);
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
        account.balance = 1900.2F;

        // Assert
        Assert.Equal(1900.2F, account.balance);
    }

    [Fact]
    public void ToString_returnsCorrectly()
    {
        // Arrange
        var account = new Account();
        account.accountNumber = 12345;
        account.accountType = "Test";
        account.balance = 1900.2F;

        // Act
        var actual = account.ToString();

        // Assert
        Assert.Equal("12345 Test 1900.2", actual);
    }
}