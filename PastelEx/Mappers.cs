using System.Collections.Immutable;

namespace PastelExtended;
internal class Mappers
{
    private static readonly ImmutableDictionary<ConsoleColor, byte> foregroundConsoleColors = new Dictionary<ConsoleColor, byte>
    {
        [ConsoleColor.Black] = 30,
        [ConsoleColor.DarkRed] = 31,
        [ConsoleColor.DarkGreen] = 32,
        [ConsoleColor.DarkYellow] = 33,
        [ConsoleColor.DarkBlue] = 34,
        [ConsoleColor.DarkMagenta] = 35,
        [ConsoleColor.DarkCyan] = 36,
        [ConsoleColor.Gray] = 37,
        [ConsoleColor.DarkGray] = 90,
        [ConsoleColor.Red] = 91,
        [ConsoleColor.Green] = 92,
        [ConsoleColor.Yellow] = 93,
        [ConsoleColor.Blue] = 94,
        [ConsoleColor.Magenta] = 95,
        [ConsoleColor.Cyan] = 96,
        [ConsoleColor.White] = 97
    }.ToImmutableDictionary();

    private static readonly ImmutableDictionary<ConsoleColor, byte> backgroundConsoleColors = new Dictionary<ConsoleColor, byte>
    {
        [ConsoleColor.Black] = 40,
        [ConsoleColor.DarkRed] = 41,
        [ConsoleColor.DarkGreen] = 42,
        [ConsoleColor.DarkYellow] = 43,
        [ConsoleColor.DarkBlue] = 44,
        [ConsoleColor.DarkMagenta] = 45,
        [ConsoleColor.DarkCyan] = 46,
        [ConsoleColor.Gray] = 47,
        [ConsoleColor.DarkGray] = 100,
        [ConsoleColor.Red] = 101,
        [ConsoleColor.Green] = 102,
        [ConsoleColor.Yellow] = 103,
        [ConsoleColor.Blue] = 104,
        [ConsoleColor.Magenta] = 105,
        [ConsoleColor.Cyan] = 106,
        [ConsoleColor.White] = 107
    }.ToImmutableDictionary();

    public static byte FromConsoleColor(ConsoleColor color, ColorPlane plane)
        => plane switch
        {
            ColorPlane.Foreground => foregroundConsoleColors[color],
            ColorPlane.Background => backgroundConsoleColors[color],
            _ => throw new NotImplementedException()
        };
}
