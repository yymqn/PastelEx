using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PastelExtended;
internal class Formatter
{
    private const string _ending = "\u001b[0m";

    public static string CloseNestedString(string text, string format) =>
        text.Replace(_ending, $"{_ending}{format}");

    /*public static string CloseNestedString(string text, string format)
    {
        int lastIndex = Helper.LastIndexOf(text, _ending);
        if (lastIndex == -1)
            return text;

        Span<char> result = stackalloc char[text.Length + format.Length];

        text.AsSpan(0, lastIndex + _ending.Length - 1).CopyTo(result);
        format.AsSpan().CopyTo(result[(lastIndex + _ending.Length - 1)..]);
        text.AsSpan(lastIndex + _ending.Length - 1).CopyTo(result[(lastIndex + _ending.Length - 1 + format.Length)..]);

        return new string(result);
    }*/

    public static string GetRgbColorFormat(Color color, ColorPlane plane)
    {
        return $"\u001b[{plane switch
        {
            ColorPlane.Foreground => 38,
            ColorPlane.Background => 48,
            _ => throw new NotImplementedException()
        }};2;{color.R};{color.G};{color.B}m";
    }

    public static string ColorRgb(string text, Color color, ColorPlane plane)
    {
        var format = $"\u001b[{plane switch
        {
            ColorPlane.Foreground => 38,
            ColorPlane.Background => 48,
            _ => throw new NotImplementedException()
        }};2;{color.R};{color.G};{color.B}m";

        return $"{CloseNestedString($"{format}{text}", format)}{_ending}";
    }

    public static string ColorUniversal(string text, ConsoleColor color, ColorPlane plane)
    {
        var format = $"\u001b[{Mappers.FromConsoleColor(color, plane)}m";
        return $"{CloseNestedString($"{format}{text}", format)}{_ending}";
    }

    public static string ChangeStyle(string text, ReadOnlySpan<Decoration> decorations)
    {
        StringBuilder builder = new();
        for (int i = 0; i < decorations.Length; i++)
            builder.Append($"\u001b[{(byte)decorations[i]}m");

        var format = builder.ToString();
        return $"{CloseNestedString($"{format}{text}", format)}{_ending}";
    }
}