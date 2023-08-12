using PastelExtended;
using System.Drawing;

namespace PastelEx.Tests;

public class PastelExTesting
{
    static void Enable() => PastelExtended.PastelEx.Enable();
    const string END = $"{CSI}0m";
    const string CSI = "\u001b[";
    const string MESSAGE = "Message?";

    #region Simple Coloring (no nesting)
    [Fact]
    public void SimpleColoring_Fg_Color()
    {
        Enable();
        Assert.Equal($"{END}{CSI}38;2;255;255;255m{MESSAGE}{END}",
            MESSAGE.Fg(Color.White));
    }

    [Fact]
    public void SimpleColoring_Bg_Color()
    {
        Enable();
        Assert.Equal($"{END}{CSI}48;2;255;255;255m{MESSAGE}{END}",
            MESSAGE.Bg(Color.White));
    }

    [Fact]
    public void SimpleColoring_Fg_ConsoleColor()
    {
        Enable();
        Assert.Equal($"{END}{CSI}97m{MESSAGE}{END}",
            MESSAGE.Fg(ConsoleColor.White));
    }

    [Fact]
    public void SimpleColoring_Bg_ConsoleColor()
    {
        Enable();
        Assert.Equal($"{END}{CSI}107m{MESSAGE}{END}",
            MESSAGE.Bg(ConsoleColor.White));
    }

    [Fact]
    public void SimpleColoring_Fg_ByteColor()
    {
        Enable();
        Assert.Equal($"{END}{CSI}38;5;69m{MESSAGE}{END}",
            MESSAGE.Fg(69));
    }

    [Fact]
    public void SimpleColoring_Bg_ByteColor()
    {
        Enable();
        Assert.Equal($"{END}{CSI}48;5;69m{MESSAGE}{END}",
            MESSAGE.Bg(69));
    }
    #endregion

    #region Nested
    [Fact]
    public void Nested_Fg_Color()
    {
        Enable();
        Assert.Equal($"{END}{CSI}38;2;255;255;255m{END}{CSI}38;2;255;255;255m{CSI}38;2;0;0;0mMessage?{END}{CSI}38;2;255;255;255mMessage?{END}",
            $"{MESSAGE.Fg(Color.Black)}{MESSAGE}".Fg(Color.White));
    }

    [Fact]
    public void Nested_Bg_Color()
    {
        Enable();
        Assert.Equal($"{END}{CSI}48;2;255;255;255m{END}{CSI}48;2;255;255;255m{CSI}48;2;0;0;0mMessage?{END}{CSI}48;2;255;255;255mMessage?{END}",
            $"{MESSAGE.Bg(Color.Black)}{MESSAGE}".Bg(Color.White));
    }
    #endregion

    #region Text decorations (no nesting)
    [Fact]
    public void SimpleDecoration()
    {
        Assert.Equal($"{END}{CSI}{(byte)Decoration.Underline}m{MESSAGE}{END}",
            MESSAGE.Deco(Decoration.Underline));
    }
    #endregion
}