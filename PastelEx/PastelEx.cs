using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace PastelExtended;

public static class PastelEx
{
    static PastelEx()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            WinNative.EnableVirtualProcessing();

        if (Environment.GetEnvironmentVariable("NO_COLOR") is null)
            Enable();
        else
            Disable();
    }

    private static bool _enabled;
    public static void Enable() => _enabled = true;
    public static void Disable() => _enabled = false;

    public static string Pastel(this string input, Color color)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, color, ColorPlane.Foreground);
    }
    public static string Pastel(this string input, ConsoleColor consoleColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorUniversal(input, consoleColor, ColorPlane.Foreground);
    }
    public static string Pastel(this string input, string hexColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Foreground);
    }

    public static string PastelBg(this string input, Color color)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, color, ColorPlane.Background);
    }
    public static string PastelBg(this string input, ConsoleColor consoleColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorUniversal(input, consoleColor, ColorPlane.Background);
    }
    public static string PastelBg(this string input, string hexColor)
    {
        if (!_enabled)
            return input;

        return Formatter.ColorRgb(input, Helper.ParseFromHex(hexColor), ColorPlane.Background);
    }

    public static string PastelDeco(this string input, Decoration decoration)
    {
        if (!_enabled)
            return input;

        return Formatter.ChangeStyle(input, new ReadOnlySpan<Decoration>(decoration));
    }
    public static string PastelDeco(this string input, params Decoration[] decorations)
    {
        if (!_enabled)
            return input;

        return Formatter.ChangeStyle(input, decorations);
    }

    public static string Gradient(string input, params Color[] colors) =>
        Gradient(input, colors.AsSpan());
    public static string Gradient(string input, ReadOnlySpan<Color> colors)
    {
        if (!_enabled)
            return input;

        return Helper.CreateGradientEffect(input, ColorPlane.Foreground, colors);
    }

    public static string GradientBg(string input, params Color[] colors) => GradientBg(input, colors.AsSpan());
    public static string GradientBg(string input, ReadOnlySpan<Color> colors)
    {
        if (!_enabled)
            return input;

        return Helper.CreateGradientEffect(input, ColorPlane.Background, colors);
    }
}
