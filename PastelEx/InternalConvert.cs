using System.Collections.Immutable;
using System.Drawing;

namespace PastelExtended;
internal static class InternalConvert
{
    public static byte FromConsoleColor(ConsoleColor color, ColorPlane plane)
    {
        return plane switch
        {
            ColorPlane.Foreground => color switch
            {
                ConsoleColor.Black => 30,
                ConsoleColor.DarkRed => 31,
                ConsoleColor.DarkGreen => 32,
                ConsoleColor.DarkYellow => 33,
                ConsoleColor.DarkBlue => 34,
                ConsoleColor.DarkMagenta => 35,
                ConsoleColor.DarkCyan => 36,
                ConsoleColor.Gray => 37,
                ConsoleColor.DarkGray => 90,
                ConsoleColor.Red => 91,
                ConsoleColor.Green => 92,
                ConsoleColor.Yellow => 93,
                ConsoleColor.Blue => 94,
                ConsoleColor.Magenta => 95,
                ConsoleColor.Cyan => 96,
                ConsoleColor.White => 97,
                _ => throw new NotImplementedException()
            },
            ColorPlane.Background => color switch
            {
                ConsoleColor.Black => 40,
                ConsoleColor.DarkRed => 41,
                ConsoleColor.DarkGreen => 42,
                ConsoleColor.DarkYellow => 43,
                ConsoleColor.DarkBlue => 44,
                ConsoleColor.DarkMagenta => 45,
                ConsoleColor.DarkCyan => 46,
                ConsoleColor.Gray => 47,
                ConsoleColor.DarkGray => 100,
                ConsoleColor.Red => 101,
                ConsoleColor.Green => 102,
                ConsoleColor.Yellow => 103,
                ConsoleColor.Blue => 104,
                ConsoleColor.Magenta => 105,
                ConsoleColor.Cyan => 106,
                ConsoleColor.White => 107,
                _ => throw new NotImplementedException()
            },
            _ => throw new NotImplementedException(),
        };
    }

    static readonly int[] _consoleColorToColor = {
        0x000000,
        0x000080,
        0x008000,
        0x008080,
        0x800000,
        0x800080,
        0x808000,
        0xC0C0C0,
        0x808080,
        0x0000FF,
        0x00FF00,
        0x00FFFF,
        0xFF0000,
        0xFF00FF,
        0xFFFF00,
        0xFFFFFF
    };

    public static Color ConsoleColorToColor(ConsoleColor consoleColor)
    {
        return Color.FromArgb(_consoleColorToColor[(int)consoleColor]);
    }

    public static ConsoleColor ColorToConsoleColor(Color originalColor)
    {
        ConsoleColor ret = default;
        double rr = originalColor.R, gg = originalColor.G, bb = originalColor.B, delta = double.MaxValue;

        foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
        {
            var name = Enum.GetName(typeof(ConsoleColor), cc)!;
            var color = Color.FromName(name == "DarkYellow" ? "Orange" : name);

            var t = Math.Pow(color.R - rr, 2.0) + Math.Pow(color.G - gg, 2.0) + Math.Pow(color.B - bb, 2.0);
            if (t == 0d)
                return cc;
            if (t < delta)
            {
                delta = t;
                ret = cc;
            }
        }

        return ret;
    }
}
