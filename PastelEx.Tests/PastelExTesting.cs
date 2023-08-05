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
        Assert.Equal(expected, input.Pastel(Color.FromArgb(r, g, b)));
    }

    [Theory]
    [InlineData(255, 0, 0, "This is a string", "\u001b[48;2;255;0;0mThis is a string\u001b[0m")]
    [InlineData(23, 255, 0, "This is a string", "\u001b[48;2;23;255;0mThis is a string\u001b[0m")]
    [InlineData(0, 100, 1, "This is a string", "\u001b[48;2;0;100;1mThis is a string\u001b[0m")]
    public void Proper_PastelBg_Formatting_No_Nesting(byte r, byte g, byte b, string input, string expected)
    {
        Assert.Equal(expected, input.PastelBg(Color.FromArgb(r, g, b)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[38;2;22;33;0mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[38;2;0;32;63mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[38;2;78;0;1mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[38;2;78;0;1m string\u001b[0m")]
    public void Proper_Pastel_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        Assert.Equal(expected, $"The {"nested".Pastel(Color.FromArgb(r2, g2, b2))} string".Pastel(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[48;2;22;33;0mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[48;2;0;32;63mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[48;2;78;0;1mThe \u001b[48;2;255;255;255mnested\u001b[0m\u001b[48;2;78;0;1m string\u001b[0m")]
    public void Proper_PastelBg_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        Assert.Equal(expected, $"The {"nested".PastelBg(Color.FromArgb(r2, g2, b2))} string".PastelBg(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData(22, 33, 0, 255, 255, 255, "\u001b[48;2;22;33;0mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;22;33;0m string\u001b[0m")]
    [InlineData(0, 32, 63, 255, 255, 255, "\u001b[48;2;0;32;63mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;0;32;63m string\u001b[0m")]
    [InlineData(78, 0, 1, 255, 255, 255, "\u001b[48;2;78;0;1mThe \u001b[38;2;255;255;255mnested\u001b[0m\u001b[48;2;78;0;1m string\u001b[0m")]
    public void Proper_Pastel_With_PastelBg_Formatting_Nested(byte r1, byte g1, byte b1, byte r2, byte g2, byte b2,
        string expected)
    {
        Assert.Equal(expected, $"The {"nested".Pastel(Color.FromArgb(r2, g2, b2))} string".PastelBg(Color.FromArgb(r1, g1, b1)));
    }

    [Theory]
    [InlineData("#ff00ff")]
    [InlineData("ff00ff")]
    [InlineData("#f0f")]
    [InlineData("f0f")]
    public void Proper_HexString_To_Color(string input)
    {
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
        Assert.Equal(expected, input.PastelDeco(decoration));
    }

    [Theory]
    [InlineData(Decoration.Invert, Decoration.Italic, "\u001b[7m\u001b[3mMy\u001b[0m\u001b[7mText\u001b[0m")]
    [InlineData(Decoration.Underline, Decoration.Bold, "\u001b[4m\u001b[1mMy\u001b[0m\u001b[4mText\u001b[0m")]
    [InlineData(Decoration.RapidBlink, Decoration.Strikethrough, "\u001b[6m\u001b[9mMy\u001b[0m\u001b[6mText\u001b[0m")]
    public void Proper_PastelDeco_Formatting_Nesting(Decoration globalDecoration, Decoration decoration2,
        string expected)
    {
        Assert.Equal(expected, $"{"My".PastelDeco(decoration2)}Text".PastelDeco(globalDecoration));
    }
}