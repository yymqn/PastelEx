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
    /// Represents strikethrough text.
    /// </summary>
    Strikethrough = 9,

    /// <summary>
    /// Represents double or bold underline text.
    /// </summary>
    DoubleUnderline = 21
}

internal enum ColorPlane : byte
{
    Foreground = 38,
    Background = 48
}