using PastelExtended;
using System.Drawing;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace PastelEx.Tests;

public class PastelExTesting
{
    static void Enable() => PastelExtended.PastelEx.Enable();
    static void Disable() => PastelExtended.PastelEx.Disable();

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
        Enable();
        Assert.Equal($"{END}{CSI}{(byte)Decoration.Underline}m{MESSAGE}{END}",
            MESSAGE.Deco(Decoration.Underline));
    }
    #endregion

    #region Disabling/Enabling
    [Fact]
    public void DisablingColorization()
    {
        Enable();
        Assert.NotEqual(MESSAGE, MESSAGE.Deco(Decoration.Invert));

        Disable();
        Assert.Equal(MESSAGE.Fg(Color.Red), MESSAGE.Bg(Color.Pink).Deco(Decoration.Dim, Decoration.Strikethrough));

        Enable();
    }
    #endregion

    #region Default colors and decorations
    [Fact]
    public void Default_Fg()
    {
        Enable();
        PastelExtended.PastelEx.Foreground = Color.White;

        Assert.Equal($"{END}{CSI}38;2;255;255;255m{CSI}107m{MESSAGE}{END}{CSI}38;2;255;255;255m",
            MESSAGE.Bg(ConsoleColor.White));

        PastelExtended.PastelEx.Foreground = default;
    }

    [Fact]
    public void Default_Bg()
    {
        Enable();
        PastelExtended.PastelEx.Background = Color.White;

        Assert.Equal($"{END}{CSI}48;2;255;255;255m{CSI}107m{MESSAGE}{END}{CSI}48;2;255;255;255m",
            MESSAGE.Bg(ConsoleColor.White));

        PastelExtended.PastelEx.Background = default;
    }

    [Fact]
    public void Default_Decoration()
    {
        Enable();
        PastelExtended.PastelEx.Decorations.Add(Decoration.Italic);

        Assert.Equal($"{END}{CSI}3m{CSI}107m{MESSAGE}{END}{CSI}3m",
            MESSAGE.Bg(ConsoleColor.White));

        PastelExtended.PastelEx.Decorations.Clear();
    }
    #endregion
}