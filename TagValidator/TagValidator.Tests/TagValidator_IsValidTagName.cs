using Xunit;
using TagValidator;

namespace TagValidator.Tests;
// Unit tests for IsValidTagName method
public class UnitTest_IsValidTagName
{
    private readonly TagValidator _tagValidator;

    public UnitTest_IsValidTagName()
    {
        _tagValidator = new TagValidator();
    }
    // Test cases where given string is a valid tag name.
    [Theory]
    [InlineData("div")]
    [InlineData("_div")]
    [InlineData("dxml")]
    [InlineData("d2-s.d_")]
    public void IsValidTagName_Test_ReturnTrue(string value)
    {
        var result = _tagValidator.IsValidTagName(value);

        Assert.True(result);
    }
    // Test cases where given string is NOT a valid tag name.
    [Theory]
    [InlineData("5iv")]
    [InlineData("@div")]
    [InlineData("xml")]
    [InlineData("Xml")]
    [InlineData("XML")]
    [InlineData("di v")]
    public void IsValidTagName_Test_ReturnFalse(string value)
    {
        var result = _tagValidator.IsValidTagName(value);

        Assert.False(result);
    }
}
