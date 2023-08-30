using System.Drawing;
using System.Runtime.CompilerServices;

#if DEBUG
[assembly: InternalsVisibleTo("PastelEx.Tests")]
#endif
namespace PastelExtended;

/// <summary>
/// Method, which contains extension methods and more to use PastelEx.
/// </summary>
public static class PastelEx
{
    static PastelEx()
    {
        Settings = new();
        NoColor = Helper.IsNoColor();

        if (!NoColor &&
            (Supported = WinNative.EnableIfSupported()))
            Settings.Enabled = true;
        else
            Settings.Enabled = false;
    }

    /// <summary>
    /// Forcefully enables colored output.
    /// </summary>
    /// <remarks>
    /// Using this method might cause unexpected output on unsupported terminal. Call this method for
    /// testing only!
    /// </remarks>
    internal static void EnableTestingMode()
    {
        Supported = true;
        NoColor = false;
        Settings.Enabled = true;
    }

    internal static bool EnabledInternal => Supported && Settings.Enabled;

    /// <summary>
    /// PastelEx behaviour settings. Used for lifetime of this application, not stored on disk.
    /// </summary>
    public static PastelSettings Settings { get; }

    /// <summary>
    /// Checks whether the current terminal is capable of displaying ANSI color codes.
    /// </summary>
    public static bool Supported { get; private set; }

    /// <summary>
    /// Verifies if the current user has explicitly disabled colored output.
    /// </summary>
    public static bool NoColor { get; private set; }

    /// <summary>
    /// Returns <see langword="true"/> if PastelEx is enabled; otherwise, returns <see langword="false"/>.
    /// </summary>
    [Obsolete("Scheduled for removal. Use PastelEx.Settings instead.")]
    public static bool Enabled => EnabledInternal;

    /// <summary>
    /// Enables any future console output colors or styles.<br/>
    /// </summary>
    /// <remarks>Avoid calling this method to prevent unintended console styling activation on unsupported terminals.</remarks>
    [Obsolete("Scheduled for removal. Use PastelEx.Settings instead.")]
    public static void Enable() => Settings.Enabled = true;

    /// <summary>
    /// Disables any future console output colors or styles.<br/>
    /// </summary>
    [Obsolete("Scheduled for removal. Use PastelEx.Settings instead.")]
    public static void Disable() => Settings.Enabled = false;

    /// <summary>
    /// Colorizes the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, Color color) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, color, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, ConsoleColor consoleColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorDefault(input, consoleColor, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, byte color) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.Color8bit(input, color, ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, in ReadOnlySpan<char> hexColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified RGB color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The RGB color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, Color color) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, color, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="consoleColor">The console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, ConsoleColor consoleColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorDefault(input, consoleColor, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified 8-bit console color.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="color">The 8-bit console color to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, byte color) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.Color8bit(input, color, ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, in ReadOnlySpan<char> hexColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Decorates the input string with the specified text decoration.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decoration">The text decoration to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string Deco(this string input, Decoration decoration) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ChangeStyle(input, new ReadOnlySpan<Decoration>(decoration))}"
        : input;

    /// <summary>
    /// Decorates the input string with multiple text decorations.
    /// </summary>
    /// <param name="input">The input string to be decorated.</param>
    /// <param name="decorations">The array of text decorations to apply.</param>
    /// <returns>The decorated string.</returns>
    public static string Deco(this string input, params Decoration[] decorations) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ChangeStyle(input, decorations)}"
        : input;

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The array of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, params Color[] colors) => EnabledInternal ?
        Gradient(input, colors.AsSpan())
        : input;

    /// <summary>
    /// Creates a gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the gradient effect to.</param>
    /// <param name="colors">The span of colors to create the gradient with.</param>
    /// <returns>The string with gradient effect.</returns>
    public static string Gradient(string input, in ReadOnlySpan<Color> colors) => EnabledInternal ?
        Helper.CreateGradientEffect(input, ColorPlane.Foreground, colors)
        : input;

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The array of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, params Color[] colors) => EnabledInternal ?
        GradientBg(input, colors.AsSpan())
        : input;

    /// <summary>
    /// Creates a background gradient effect on the input string using the specified colors.
    /// </summary>
    /// <param name="input">The input string to apply the background gradient effect to.</param>
    /// <param name="colors">The span of colors to create the background gradient with.</param>
    /// <returns>The input string with background gradient effect.</returns>
    public static string GradientBg(string input, in ReadOnlySpan<Color> colors) => EnabledInternal ?
        Helper.CreateGradientEffect(input, ColorPlane.Background, colors)
        : input;

    internal static CompactColor defaultForeground = default;
    internal static CompactColor defaultBackground = default;

    /// <summary>
    /// Specifies default Console foreground color to be used.
    /// </summary>
    public static CompactColor Foreground
    {
        get => defaultForeground;
        set
        {
            var tmpValue = value with { Plane = ColorPlane.Foreground };

            defaultForeground = tmpValue;
            Formatter.foregroundFormat = tmpValue == default ?
                string.Empty : tmpValue.GetAnsiSequence();

            if (EnabledInternal && Settings.InstantRecolor)
                Console.Write(Formatter.foregroundFormat);
        }
    }

    /// <summary>
    /// Specifies default Console background color to be used.
    /// </summary>
    public static CompactColor Background
    {
        get => defaultBackground;
        set
        {
            var tmpValue = value with { Plane = ColorPlane.Background };

            defaultBackground = tmpValue;
            Formatter.backgroundFormat = tmpValue == default ?
                string.Empty : tmpValue.GetAnsiSequence();

            if (EnabledInternal && Settings.InstantRecolor)
                Console.Write(Formatter.backgroundFormat);
        }
    }

    /// <summary>
    /// Specifies default Console text decorations to be used.
    /// </summary>
    public static DecorationCollection Decorations => Formatter.sharedDecorations;

    /// <summary>
    /// Clears Console buffer with applying background colors if supported.
    /// </summary>
    public static void ClearConsole()
    {
        Console.Clear();

        if (EnabledInternal)
            Console.Write("\u001b[2J");
    }

    /// <summary>
    /// Refills current console buffer with empty chars. This will remove any old text, but refills entire background with current
    /// default <see cref="Background"/> color.
    /// </summary>
    public static void Refill()
    {
        if (EnabledInternal)
            Console.Write("\u001b[2J");
        else
            Console.Clear();
    }

    /// <summary>
    /// Removes all default colors and decorations.
    /// </summary>
    public static void Reset()
    {
        Decorations.Clear();
        Foreground = Color.Empty;
        Background = Color.Empty;

        if (EnabledInternal && Settings.InstantRecolor)
            Console.Write("\u001b[0m");
        Console.ResetColor();
    }
}