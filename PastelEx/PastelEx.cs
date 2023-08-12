using System;
using System.Drawing;

namespace PastelExtended;

/// <summary>
/// Method, which contains extension methods and more to use PastelEx.
/// </summary>
public static class PastelEx
{
    static PastelEx()
    {
        if (Environment.GetEnvironmentVariable("NO_COLOR") is null &&
            (_supported = WinNative.EnableIfSupported()))
            Enable();
        else
            Disable();
    }

    internal static bool _enabled;
    private static readonly bool _supported;

    /// <summary>
    /// <see langword="true"/> if current terminal should support ANSI color codes; otherwise <see langword="false"/>
    /// </summary>
    public static bool IsSupported => _supported;

    /// <summary>
    /// <see langword="true"/> if PastelEx is enabled; otherwise <see langword="false"/>
    /// </summary>
    public static bool Enabled => _enabled;

    /// <summary>
    /// Enables any future console output colors or styles.<br/>
    /// </summary>
    /// <remarks>Avoid calling this method to prevent unintended console styling activation on unsupported terminals.</remarks>
    public static void Enable() => _enabled = true;

    /// <summary>
    /// Disables any future console output colors or styles.<br/>
    /// </summary>
    public static void Disable() => _enabled = false;

    /// <summary>
    /// Colorizes the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, Color color) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, color, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, ConsoleColor consoleColor) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorDefault(input, consoleColor, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, byte color) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.Color8bit(input, color, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, in ReadOnlySpan<char> hexColor) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, Color color) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, color, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, ConsoleColor consoleColor) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorDefault(input, consoleColor, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, byte color) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.Color8bit(input, color, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, in ReadOnlySpan<char> hexColor) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Decorates the input string with the specified text decoration.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decoration">The text decoration to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string Deco(this string input, Decoration decoration) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ChangeStyle(input, new ReadOnlySpan<Decoration>(decoration))}"
        : input;

    /// <summary>
    /// Decorates the input string with multiple text decorations.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decorations">The array of text decorations to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string Deco(this string input, params Decoration[] decorations) => _enabled ?
        $"{Formatter.DefaultFormat}{Formatter.ChangeStyle(input, decorations)}"
        : input;

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The array of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, params Color[] colors) => _enabled ?
        Gradient(input, colors.AsSpan())
        : input;

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The span of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, in ReadOnlySpan<Color> colors) => _enabled ?
        Helper.CreateGradientEffect(input, ColorPlane.Foreground, colors)
        : input;

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The array of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, params Color[] colors) => _enabled ?
        GradientBg(input, colors.AsSpan())
        : input;

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The span of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, in ReadOnlySpan<Color> colors) => _enabled ?
        Helper.CreateGradientEffect(input, ColorPlane.Background, colors)
        : input;

    internal static Color defaultForeground = Color.Empty;
    internal static Color defaultBackground = Color.Empty;

    /// <summary>
    /// Specifies default Console foreground color to be used.
    /// </summary>
    public static Color Foreground
    {
        get => defaultForeground;
        set
        {
            defaultForeground = value;
            Formatter.foregroundFormat = Formatter.GetRgbColorFormat(value, ColorPlane.Foreground);

            if (_enabled && value != default)
                Console.Write(Formatter.DefaultFormat);
        }
    }

    /// <summary>
    /// Specifies default Console background color to be used.
    /// </summary>
    public static Color Background
    {
        get => defaultBackground;
        set
        {
            defaultBackground = value;
            Formatter.backgroundFormat = Formatter.GetRgbColorFormat(value, ColorPlane.Background);

            if (_enabled && value != default)
                Console.Write(Formatter.DefaultFormat);
        }
    }

    /// <summary>
    /// Specifies default Console text decorations to be used.
    /// </summary>
    public static DecorationList Decorations => Formatter.sharedDecorations;

    /// <summary>
    /// Clears Console buffer with applying background colors if supported.
    /// </summary>
    public static void ClearConsole()
    {
        Console.Clear();

        if (_enabled)
            Console.Write("\u001b[2J");
    }

    /// <summary>
    /// Refills current console buffer with empty chars, with clearing the buffer.
    /// </summary>
    public static void Refill()
    {
        if (_enabled)
            Console.Write("\u001b[2J");
        else
            Console.Clear();
    }

    /// <summary>
    /// Removes all default decorations and colors.
    /// </summary>
    public static void Reset()
    {
        Decorations.Clear();
        Foreground = Color.Empty;
        Background = Color.Empty;

        if (_enabled)
            Console.Write("\u001b[0m");
        Console.ResetColor();
    }
}