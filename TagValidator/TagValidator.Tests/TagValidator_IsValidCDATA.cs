using Xunit;
using TagValidator;

namespace TagValidator.Tests;

// Unit tests for IsValidCDATA method
public class UnitTest_IsValidCDATA
{
    private readonly TagValidator _tagValidator;

    public UnitTest_IsValidCDATA()
    {
        _tagValidator = new TagValidator();
    }
    // Test case where given string is a valid CDATA.
    [Theory]
    [InlineData("[CDATA[<div>]>]]>]]")]
    public void IsValidCDATA_Test_ReturnTrue(string value)
    {
        var result = _tagValidator.IsValidCDATA(value);

        Assert.True(result);
    }
    // Test cases where given string is NOT a valid CDATA.
    [Theory]
    [InlineData("<Design><Code>hello world</Code></Design><People>")]
    [InlineData("<People><Design><Code>hello world</People></Code></Design>")]
    [InlineData("<People age=”1”>hello world</People>")]
    [InlineData("<A>  <B> </A>   </B>")]
    [InlineData("ABCDE")]
    [InlineData("<note><to>Tove</to><from>Jani</from> <heading>Reminder</pheading><body>Don't forget me this weekend!</body></note>")]
    public void IsValidCDATA_Test_ReturnFalse(string value)
    {
        var result = _tagValidator.IsValidCDATA(value);

        Assert.False(result);
    }
}