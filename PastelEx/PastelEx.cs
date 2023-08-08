using System.Drawing;

namespace PastelExtended;

public static class PastelEx
{
    static PastelEx()
    {
        if (Environment.GetEnvironmentVariable("NO_COLOR") is null &&
            WinNative.EnableIfSupported())
            Enable();
        else
            Disable();
    }

    private static bool _enabled;
    public static void Enable() => _enabled = true;
    public static void Disable() => _enabled = false;

    /// <summary>
    /// Colorizes the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Pastel(this string input, Color color)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, color, ColorPlane.Foreground);
    }

    /// <summary>
    /// Colorizes the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Pastel(this string input, ConsoleColor consoleColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorDefault(input, consoleColor, ColorPlane.Foreground);
    }

    /// <summary>
    /// Colorizes the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Pastel(this string input, byte color)
    {
        if (!_enabled)
            return input;

        return Formatter.Color8bit(input, color, ColorPlane.Foreground);
    }

    /// <summary>
    /// Colorizes the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Pastel(this string input, in ReadOnlySpan<char> hexColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Foreground);
    }

    /// <summary>
    /// Colorizes the background of the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string PastelBg(this string input, Color color)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, color, ColorPlane.Background);
    }

    /// <summary>
    /// Colorizes the background of the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string PastelBg(this string input, ConsoleColor consoleColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorDefault(input, consoleColor, ColorPlane.Background);
    }

    /// <summary>
    /// Colorizes the background of the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string PastelBg(this string input, byte color)
    {
        if (!_enabled)
            return input;

        return Formatter.Color8bit(input, color, ColorPlane.Background);
    }

    /// <summary>
    /// Colorizes the background of the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string PastelBg(this string input, in ReadOnlySpan<char> hexColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Background);
    }

    /// <summary>
    /// Decorates the input string with the specified text decoration.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decoration">The text decoration to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string PastelDeco(this string input, Decoration decoration)
    {
        if (!_enabled)
            return input;

        return Formatter.ChangeStyle(input, new ReadOnlySpan<Decoration>(decoration));
    }

    /// <summary>
    /// Decorates the input string with multiple text decorations.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decorations">The array of text decorations to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string PastelDeco(this string input, params Decoration[] decorations)
    {
        if (!_enabled)
            return input;

        return Formatter.ChangeStyle(input, decorations);
    }

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The array of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, params Color[] colors) => Gradient(input, colors.AsSpan());

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The span of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, in ReadOnlySpan<Color> colors)
    {
        if (!_enabled)
            return input;

        return Helper.CreateGradientEffect(input, ColorPlane.Foreground, colors);
    }

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The array of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, params Color[] colors) => GradientBg(input, colors.AsSpan());

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The span of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, in ReadOnlySpan<Color> colors)
    {
        if (!_enabled)
            return input;

        return Helper.CreateGradientEffect(input, ColorPlane.Background, colors);
    }
}
