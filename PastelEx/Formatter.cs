using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PastelExtended;
internal class Formatter
{
    private const string _ending = "\u001b[0m";

    public static string CloseNestedString(string text, ReadOnlySpan<char> format) =>
        text.Replace(_ending, $"{_ending}{format}");

    public static string GetRgbColorFormat(Color color, ColorPlane plane)
    {
        return $"\u001b[{plane switch
        {
            ColorPlane.Foreground => 38,
            ColorPlane.Background => 48,
            _ => throw new NotImplementedException()
        }};2;{color.R};{color.G};{color.B}m";
    }

    public static string ColorRgb(ReadOnlySpan<char> text, Color color, ColorPlane plane)
    {
        var format = $"\u001b[{plane switch
        {
            ColorPlane.Foreground => 38,
            ColorPlane.Background => 48,
            _ => throw new NotImplementedException()
        }};2;{color.R};{color.G};{color.B}m";

        return $"{CloseNestedString($"{format}{text}", format)}{_ending}";
    }

    public static string ColorUniversal(ReadOnlySpan<char> text, ConsoleColor color, ColorPlane plane)
    {
        var format = $"\u001b[{Mappers.FromConsoleColor(color, plane)}m";
        return $"{CloseNestedString($"{format}{text}", format)}{_ending}";
    }

    public static string ChangeStyle(ReadOnlySpan<char> text, ReadOnlySpan<Decoration> decorations)
    {
        Span<char> chars = stackalloc char[6 * decorations.Length];
        int length = 0;

        for (int i = 0; i < decorations.Length; i++)
        {
            var format = $"\u001b[{(byte)decorations[i]}m";
            format.CopyTo(chars[length..]);

            length += format.Length;
        }

        return $"{CloseNestedString($"{chars[..length]}{text}", chars[..length])}{_ending}";
    }
}