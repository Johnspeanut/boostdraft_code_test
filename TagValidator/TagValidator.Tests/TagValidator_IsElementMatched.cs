using Xunit;
using TagValidator;

namespace TagValidator.Tests;

// Unit tests for IsElementMatched method
public class UnitTest_IsElementMatched
{
    private readonly TagValidator _tagValidator;

    public UnitTest_IsElementMatched()
    {
        _tagValidator = new TagValidator();
        _tagValidator.Stack.Push("div");
    }
    // Test case where given tag name is balanced-match.
    [Theory]
    [InlineData("div")]
    public void IsElementMatched_Test_ReturnTrue(string tagname)
    {
        var result = _tagValidator.IsElementMatched(tagname, true);
        Assert.True(result);
    }

    // Test case where given tag name is NOT balanced-match.
    [Theory]
    [InlineData("span")]

    public void IsElementMatched_Test_ReturnFalse(string tagname)
    {
        var result = _tagValidator.IsElementMatched(tagname, true);
        Assert.False(result);
    }
}