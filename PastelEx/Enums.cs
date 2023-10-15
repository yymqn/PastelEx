namespace PastelExtended;
/// <summary>
/// Available text decorations.
/// </summary>
public enum Decoration : byte
{
    /// <summary>
    /// Represents bold or more intensive text.
    /// </summary>
    Bold = 1,

    /// <summary>
    /// Represents less intensive text.
    /// </summary>
    Dim = 2,

    /// <summary>
    /// Represents italic text.
    /// </summary>
    Italic = 3,

    /// <summary>
    /// Represents underlined text.
    /// </summary>
    Underline = 4,

    /// <summary>
    /// Represents slowly blinking text.
    /// </summary>
    SlowBlink = 5,

    /// <summary>
    /// Represents rapidly blinking text.
    /// </summary>
    RapidBlink = 6,

    /// <summary>
    /// Represents text with inverted foreground and background colors.
    /// </summary>
    Invert = 7,

    /// <summary>
    /// Represents hidden text. Not widely supported.
    /// </summary>
    Conceal = 8,

    /// <summary>
    /// Represents strikethrough text.
    /// </summary>
    Strikethrough = 9,

    /// <summary>
    /// Represents double or bold underline text.
    /// </summary>
    DoubleUnderline = 21
}

/// <summary>
/// Available color palettes to be used.
/// </summary>
public enum ColorPalette : byte
{
    /// <summary>
    /// Uses only <see cref="System.Drawing.Color"/> to get colors to the console. All other color types will be
    /// automatically converted into the nearest <see cref="System.Drawing.Color"/> type.
    /// </summary>
    Color,

    /// <summary>
    /// Uses only <see cref="System.ConsoleColor"/> to get colors to the console. All other color types will be
    /// automatically converted into the nearest <see cref="System.ConsoleColor"/> type.
    /// </summary>
    ConsoleColor,

    /// <summary>
    /// Both <see cref="System.ConsoleColor"/> and <see cref="System.Drawing.Color"/> can be used without any conversions.
    /// </summary>
    /// <remarks>
    /// If you're using this option, which is enabled by default, be aware as user's console palette might be different what expected,
    /// resulting in an unexpected results when mixing both <see cref="System.ConsoleColor"/> and <see cref="System.Drawing.Color"/>.
    /// </remarks>
    Both
}

internal enum ColorPlane : byte
{
    Foreground = 38,
    Background = 48
}