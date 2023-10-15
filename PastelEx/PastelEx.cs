using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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
    /// Colorizes the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the text.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, in ReadOnlySpan<char> hexColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Foreground)}"
        : input;

    /// <summary>
    /// Colorizes the foreground of the input string using the specified <see cref="CompactColor"/> struct.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="compactColor">The <see cref="CompactColor"/> to be used with set value.</param>
    /// <returns>The colorized input string.</returns>
    public static string Fg(this string input, CompactColor compactColor) => compactColor.IsColor ?
        input.Fg((Color)compactColor) :
        compactColor.IsConsoleColor ? input.Fg((ConsoleColor)compactColor) :
        input;

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
    /// Colorizes the background of the input string using the specified hex color value.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="hexColor">The hex color value to apply to the background.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, in ReadOnlySpan<char> hexColor) => EnabledInternal ?
        $"{Formatter.DefaultFormat}{Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Background)}"
        : input;

    /// <summary>
    /// Colorizes the background of the input string using the specified <see cref="CompactColor"/> struct.
    /// </summary>
    /// <param name="input">The input string to be colorized.</param>
    /// <param name="compactColor">The <see cref="CompactColor"/> to be used with set value.</param>
    /// <returns>The input string with background colorized.</returns>
    public static string Bg(this string input, CompactColor compactColor) => compactColor.IsColor ?
        input.Bg((Color)compactColor) :
        compactColor.IsConsoleColor ? input.Bg((ConsoleColor)compactColor) :
        input;

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

    private const string eraseLine = "\u001b[2K";
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

    static readonly bool _isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    internal static bool IsWindows => _isWindows;

    /// <summary>
    /// Clears Console buffer.
    /// </summary>
    public static void Clear()
    {
        if (!EnabledInternal)
        {
            Console.Clear();
            return;
        }

        if (IsWindows)
        {
#pragma warning disable CA1416
            var cursorVisible = Console.CursorVisible;
            var cursorSize = Console.CursorSize;

            Console.Write($"\u001bc{Formatter.DefaultFormat}\u001b[J");

            Console.CursorVisible = cursorVisible;
            Console.CursorSize = cursorSize;
#pragma warning restore CA1416
            return;
        }

        Console.Write($"\u001bc{Formatter.DefaultFormat}\u001b[J");
    }

    /// <summary>
    /// Refills current console buffer with empty chars. This will remove any old text, but refills entire background with current
    /// default <see cref="Background"/> color.
    /// </summary>
    [Obsolete("Use PastelEx.Clear instead. This method flickers.")]
    public static void Refill()
    {
        if (EnabledInternal)
            Console.Write("\u001b[2J");
        else
            Console.Clear();
    }

    /// <summary>
    /// Gets informations of the string, like it's original length without any formats.
    /// </summary>
    /// <param name="input">The string which have been modified by PastelEx.</param>
    /// <returns>The information of the string.</returns>
    public static PastelInformation GetInformation(string input) =>
        new(input);

    /// <summary>
    /// Removes all default colors and decorations, resets console to it's defaults.
    /// </summary>
    public static void ResetPalette()
    {
        Decorations.Clear();
        Foreground = Color.Empty;
        Background = Color.Empty;

        if (EnabledInternal && Settings.InstantRecolor)
            Console.Write("\u001b[0m");
        Console.ResetColor();
    }

    /// <summary>
    /// Erases current line with moving cursor to the beginning of the line.
    /// </summary>
    public static void EraseLine()
    {
        if (EnabledInternal)
        {
            Console.Write($"{eraseLine}\r");
        }
        else
        {
            if (_redirectedOutput)
                return;

            var top = Console.CursorTop;
            Console.Write($"\r{new string(' ', Console.WindowWidth)}");
            Console.SetCursorPosition(0, top);
        }
    }

    /// <summary>
    /// Erases current line with moving cursor to the beginning of the line. The line will be overwritten to the string in the span.
    /// </summary>
    public static void EraseLine(ReadOnlySpan<char> newText)
    {
        if (EnabledInternal)
        {
            Console.Write($"{eraseLine}\r{newText}");
        }
        else
        {
            if (_redirectedOutput)
            {
                Console.Out.Write(newText);
                return;
            }

            var top = Console.CursorTop;
            Console.Write($"\r{new string(' ', Console.WindowWidth)}");
            Console.SetCursorPosition(0, top);
            Console.Out.Write(newText);
            Console.SetCursorPosition(0, top);
        }
    }

    private static readonly bool _redirectedOutput = Console.IsOutputRedirected;

    /// <summary>
    /// Saves current console buffer and creates a new one. After the action has completed with of without any exception,
    /// old console buffer will be restored.
    /// </summary>
    /// <param name="action">An action to be performed on the alternate screen.</param>
    /// <remarks>Might not be supported on every terminal. This method is not thread safe and
    /// shouldn't be called multiple times at once!</remarks>
    public static void AlternateScreen(Action action)
    {
        if (!EnabledInternal)
        {
            action();
            return;
        }

        var (x, y) = (0, 0);
        if (!_redirectedOutput)
        {
            x = Console.CursorLeft;
            y = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
        }

        try
        {
            Console.Write("\u001b[?1049h");
            action();
        }
        finally
        {
            Console.Write("\u001b[?1049l");

            if (!_redirectedOutput)
            {
                Console.CursorLeft = x;
                Console.CursorTop = y;
            }
        }
    }
}