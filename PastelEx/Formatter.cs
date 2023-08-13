using System.Drawing;

namespace PastelExtended;
internal class Formatter
{
    internal static string foregroundFormat = string.Empty;
    internal static string backgroundFormat = string.Empty;
    internal static readonly DecorationList sharedDecorations = new();

    private const string _endCode = "\u001b[0m";
    internal static string DefaultFormat => $"{_endCode}{foregroundFormat}{backgroundFormat}{sharedDecorations.ToString()}";

    public static string CloseNestedString(string text, in ReadOnlySpan<char> format)
    {
        return text.
            Replace(DefaultFormat, $"{DefaultFormat}{format}");
    }

    public static string GetRgbColorFormat(Color color, ColorPlane plane)
    {
        if (color == Color.Empty)
            return string.Empty;

        return $"\u001b[{(byte)plane};2;{color.R};{color.G};{color.B}m";
    }

    public static string ColorRgb(in ReadOnlySpan<char> text, Color color, ColorPlane plane)
    {
        var format = GetRgbColorFormat(color, plane);
        return $"{CloseNestedString($"{format}{text}", format)}{DefaultFormat}";
    }

    public static string Color8bit(in ReadOnlySpan<char> text, byte color, ColorPlane plane)
    {
        var format = $"\u001b[{(byte)plane};5;{color}m";
        return $"{CloseNestedString($"{format}{text}", format)}{DefaultFormat}";
    }

    public static string ColorDefault(in ReadOnlySpan<char> text, ConsoleColor color, ColorPlane plane)
    {
        var format = $"\u001b[{Mappers.FromConsoleColor(color, plane)}m";
        return $"{CloseNestedString($"{format}{text}", format)}{DefaultFormat}";
    }

    public static string ChangeStyle(in ReadOnlySpan<char> text, in ReadOnlySpan<Decoration> decorations)
    {
        Span<char> chars = stackalloc char[6 * decorations.Length];
        int length = 0;

        for (int i = 0; i < decorations.Length; i++)
        {
            var format = $"\u001b[{(byte)decorations[i]}m";
            format.CopyTo(chars[length..]);

            length += format.Length;
        }

        return $"{CloseNestedString($"{chars[..length]}{text}", chars[..length])}{DefaultFormat}";
    }
}