using PastelExtended;
using System.Drawing;

namespace PastelEx.Tests;

public class PastelExTesting
{
    [Theory]
    [InlineData(255, 0, 0, "This is a string", "\u001b[38;2;255;0;0mThis is a string\u001b[0m")]
    [InlineData(23, 255, 0, "This is a string", "\u001b[38;2;23;255;0mThis is a string\u001b[0m")]
    [InlineData(0, 100, 1, "This is a string", "\u001b[38;2;0;100;1mThis is a string\u001b[0m")]
    public void Proper_Pastel_Formatting_No_Nesting(byte r, byte g, byte b, string input, string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, input.Pastel(Color.FromArgb(r, g, b)));
    }

    [Theory]
    [InlineData(255, 0, 0, "This is a string", "\u001b[48;2;255;0;0mThis is a string\u001b[0m")]
    [InlineData(23, 255, 0, "This is a string", "\u001b[48;2;23;255;0mThis is a string\u001b[0m")]
    [InlineData(0, 100, 1, "This is a string", "\u001b[48;2;0;100;1mThis is a string\u001b[0m")]
    public void Proper_PastelBg_Formatting_No_Nesting(byte r, byte g, byte b, string input, string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, input.PastelBg(Color.FromArgb(r, g, b)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[38;2;22;33;0mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[38;2;0;32;63mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[38;2;78;0;1mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;78;0;1m string\u001b[0m")]
    public void Proper_Pastel_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, $"The {"nested".Pastel(Color.FromArgb(r2, g2, b2))} string".Pastel(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[48;2;22;33;0mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[48;2;0;32;63mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[48;2;78;0;1mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;78;0;1m string\u001b[0m")]
    public void Proper_PastelBg_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, $"The {"nested".PastelBg(Color.FromArgb(r2, g2, b2))} string".PastelBg(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[48;2;22;33;0mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[48;2;0;32;63mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[48;2;78;0;1mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;78;0;1m string\u001b[0m")]
    public void Proper_Pastel_With_PastelBg_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, $"The {"nested".Pastel(Color.FromArgb(r2, g2, b2))} string".PastelBg(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData("#ff00ff")]
    [InlineData("ff00ff")]
    [InlineData("#f0f")]
    [InlineData("f0f")]
    public void Proper_HexString_To_Color(string input)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal("\u001b[38;2;255;0;255mTest\u001b[0m", "Test".Pastel(input));
        Assert.Equal("\u001b[48;2;255;0;255mTest\u001b[0m", "Test".PastelBg(input));
    }

    [Theory]
    [InlineData(Decoration.Invert, "This text is styled.", "\u001b[7mThis text is styled.\u001b[0m")]
    [InlineData(Decoration.Underline, "This text is styled.", "\u001b[4mThis text is styled.\u001b[0m")]
    [InlineData(Decoration.RapidBlink, "This text is styled.", "\u001b[6mThis text is styled.\u001b[0m")]
    public void Proper_PastelDeco_Formatting_No_Nesting(Decoration decoration,
        string input, string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, input.PastelDeco(decoration));
    }

    [Theory]
    [InlineData(Decoration.Invert, Decoration.Italic, "\u001b[7m\u001b[3mMy\u001b[0m\u001b[7mText\u001b[0m")]
    [InlineData(Decoration.Underline, Decoration.Bold, "\u001b[4m\u001b[1mMy\u001b[0m\u001b[4mText\u001b[0m")]
    [InlineData(Decoration.RapidBlink, Decoration.Strikethrough, "\u001b[6m\u001b[9mMy\u001b[0m\u001b[6mText\u001b[0m")]
    public void Proper_PastelDeco_Formatting_Nesting(Decoration globalDecoration, Decoration decoration2,
        string expected)
    {
        PastelExtended.PastelEx.Enable();
        Assert.Equal(expected, $"{"My".PastelDeco(decoration2)}Text".PastelDeco(globalDecoration));
    }

    [Theory]
    [InlineData(255, "This is a string", "\u001b[38;5;255mThis is a string\u001b[0m")]
    [InlineData(23, "This is a string", "\u001b[38;5;23mThis is a string\u001b[0m")]
    [InlineData(100, "This is a string", "\u001b[38;5;100mThis is a string\u001b[0m")]
    public void Proper_Pastel8bit_Formatting_No_Nesting(byte color, string input, string expected)
    {
        Assert.Equal(expected, input.Pastel(color));
    }

    [Theory]
    [InlineData(255, "This is a string", "\u001b[48;5;255mThis is a string\u001b[0m")]
    [InlineData(23, "This is a string", "\u001b[48;5;23mThis is a string\u001b[0m")]
    [InlineData(100, "This is a string", "\u001b[48;5;100mThis is a string\u001b[0m")]
    public void Proper_PastelBg8bit_Formatting_No_Nesting(byte color, string input, string expected)
    {
        Assert.Equal(expected, input.PastelBg(color));
    }

    [Theory]
    [InlineData(22, 255, "\u001b[38;5;22mThe \u001b[38;5;255mnested\u001b[0m\u001b[38;5;22m string\u001b[0m")]
    [InlineData(0, 255, "\u001b[38;5;0mThe \u001b[38;5;255mnested\u001b[0m\u001b[38;5;0m string\u001b[0m")]
    [InlineData(78, 255, "\u001b[38;5;78mThe \u001b[38;5;255mnested\u001b[0m\u001b[38;5;78m string\u001b[0m")]
    public void Proper_Pastel8bit_Formatting_Nested(byte color1, byte color2, string expected)
    {
        var nestedString = "nested".Pastel(color2);
        var input = $"The {nestedString} string".Pastel(color1);

        Assert.Equal(expected, input);
    }

    [Theory]
    [InlineData(22, 255, "\u001b[48;5;22mThe \u001b[48;5;255mnested\u001b[0m\u001b[48;5;22m string\u001b[0m")]
    [InlineData(0, 255, "\u001b[48;5;0mThe \u001b[48;5;255mnested\u001b[0m\u001b[48;5;0m string\u001b[0m")]
    [InlineData(78, 255, "\u001b[48;5;78mThe \u001b[48;5;255mnested\u001b[0m\u001b[48;5;78m string\u001b[0m")]
    public void Proper_PastelBg8bit_Formatting_Nested(byte color1, byte color2, string expected)
    {
        var nestedString = "nested".PastelBg(color2);
        var input = $"The {nestedString} string".PastelBg(color1);

        Assert.Equal(expected, input);
    }

    [Theory]
    [InlineData(22, 255, "\u001b[48;5;22mThe \u001b[38;5;255mnested\u001b[0m\u001b[48;5;22m string\u001b[0m")]
    [InlineData(0, 255, "\u001b[48;5;0mThe \u001b[38;5;255mnested\u001b[0m\u001b[48;5;0m string\u001b[0m")]
    [InlineData(78, 255, "\u001b[48;5;78mThe \u001b[38;5;255mnested\u001b[0m\u001b[48;5;78m string\u001b[0m")]
    public void Proper_Pastel8bit_With_PastelBg8bit_Formatting_Nested(byte color1, byte color2, string expected)
    {
        var nestedString = "nested".Pastel(color2);
        var input = $"The {nestedString} string".PastelBg(color1);

        Assert.Equal(expected, input);
    }

    [Fact]
    public void Proper_GradienceEffect()
    {
        string expectedOutput = "\u001b[38;2;255;0;0ma\u001b[38;2;170;42;0mb\u001b[38;2;85;85;0mc\u001b[38;2;0;128;0md\u001b[38;2;0;64;127me\u001b[0m";
        var gradientString = PastelExtended.PastelEx.Gradient("abcde", Color.Red, Color.Green, Color.Blue);

        Assert.Equal(expectedOutput, gradientString);
    }
}