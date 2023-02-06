using Xunit;
using TagValidator;

namespace TagValidator.Tests;

// Unit tests for DetermineXml method
public class UnitTest_DetermineXml
{
    private readonly TagValidator _tagValidator;

    public UnitTest_DetermineXml()
    {
        _tagValidator = new TagValidator();
    }
    // Test cases where given string is valid XML.
    [Theory]
    [InlineData("<Design><Code>hello world</Code></Design>")]
    [InlineData("<DIV>This is the first line <![CDATA[<div>]]></DIV>")]
    [InlineData("<DIV>>>  ![cdata[]] <![CDATA[<div>]>]]>]]>>]</DIV>")]
    [InlineData("<note><to>Tove</to><from>Jani</from> <heading>Reminder</heading><body>Don't forget me this weekend!</body></note>")]
    public void DertermineXml_Test_ReturnTrue(string xml)
    {
        var result = _tagValidator.DetermineXml(xml);

        Assert.True(result);
    }
    // Test cases where given string is NOT valid XML.
    [Theory]
    [InlineData("<Design><Code>hello world</Code></Design><People>")]
    [InlineData("<People><Design><Code>hello world</People></Code></Design>")]
    [InlineData("<People age=”1”>hello world</People>")]
    [InlineData("<A>  <B> </A>   </B>")]
    [InlineData("ABCDE")]
    [InlineData("<note><to>Tove</to><from>Jani</from> <heading>Reminder</pheading><body>Don't forget me this weekend!</body></note>")]
    public void DertermineXml_Test_ReturnFalse(string xml)
    {
        var result = _tagValidator.DetermineXml(xml);

        Assert.False(result);
    }
}
