using System.Drawing;
using System.Runtime.CompilerServices;

namespace PastelExtended;
/// <summary>
/// Represents a single type, which can hold both Color and ConsoleColor. This struct is read-only.
/// </summary>
public readonly struct CompactColor
{
    private Color? ColorType { get; init; } = null;
    private ConsoleColor? ConsoleColorType { get; init; } = null;

    internal bool IsColor => ColorType is not null && ConsoleColorType is null;
    internal bool IsConsoleColor => ConsoleColorType is not null && ColorType is null;

    /// <summary>
    /// Creates a new default instance of this object.
    /// </summary>
    /// <remarks>Use Create() static method instead.</remarks>
    public CompactColor() { }

    internal ColorPlane Plane { get; init; }

    /// <summary>
    /// Creates a new instance of <see cref="CompactColor"/> holding a <see cref="Color"/> color type.
    /// </summary>
    /// <param name="color">Color to be used.</param>
    /// <returns>A new <see cref="CompactColor"/> instance.</returns>
    public static CompactColor Create(Color color)
    {
        return new()
        {
            ColorType = color
        };
    }

    /// <summary>
    /// Creates a new instance of <see cref="CompactColor"/> holding a <see cref="Color"/> color type.
    /// </summary>
    /// <param name="hexColor">Color in hex format to be used.</param>
    /// <returns>A new <see cref="CompactColor"/> instance.</returns>
    public static CompactColor Create(ReadOnlySpan<char> hexColor)
    {
        return new()
        {
            ColorType = Helper.ParseFromHex(hexColor)
        };
    }

    /// <summary>
    /// Creates a new instance of <see cref="CompactColor"/> holding a <see cref="ConsoleColor"/> color type.
    /// </summary>
    /// <param name="consoleColor">ConsoleColor to be used.</param>
    /// <returns>A new <see cref="CompactColor"/> instance.</returns>
    public static CompactColor Create(ConsoleColor consoleColor)
    {
        return new()
        {
            ConsoleColorType = consoleColor
        };
    }

    /// <summary>
    /// Implicitly converts a <see cref="Color"/> into a new instance of <see cref="CompactColor"/> by calling its static <see cref="Create(Color)"/> method.
    /// </summary>
    /// <param name="color">A new <see cref="CompactColor"/> instance.</param>
    public static implicit operator CompactColor(Color color) => Create(color);

    /// <summary>
    /// Implicitly converts a <see cref="ConsoleColor"/> into a new instance of <see cref="CompactColor"/> by calling its static <see cref="Create(Color)"/> method.
    /// </summary>
    /// <param name="consoleColor">A new <see cref="CompactColor"/> instance.</param>
    public static implicit operator CompactColor(ConsoleColor consoleColor) => Create(consoleColor);

    internal readonly string GetAnsiSequence()
    {
        if (ConsoleColorType is not null)
        {
            var ansiCode = Formatter.GetDefaultColorFormat((ConsoleColor)ConsoleColorType, Plane);
            return ansiCode;
        }

        if (ColorType is not null)
        {
            var ansiCode = Formatter.GetRgbColorFormat((Color)ColorType, Plane);
            return ansiCode;
        }

        return string.Empty;
    }

    /// <summary>
    /// Explicitly converts the <see cref="CompactColor"/> type to it's original <see cref="ConsoleColor"/> value.
    /// </summary>
    /// <param name="compactColor">The original value. Throws <see cref="InvalidOperationException"/> exception when not possible to return original value.</param>
    /// <exception cref="InvalidOperationException"/>
    public static explicit operator ConsoleColor(CompactColor compactColor)
    {
        if (compactColor == default ||
            compactColor.ConsoleColorType is null)
            throw new InvalidOperationException($"Instance not set to {nameof(ConsoleColor)}.");

        return (ConsoleColor)compactColor.ConsoleColorType;
    }

    /// <summary>
    /// Explicitly converts the <see cref="CompactColor"/> type to it's original <see cref="Color"/> value.
    /// </summary>
    /// <param name="compactColor">The original value. Throws <see cref="InvalidOperationException"/> exception when not possible to return original value.</param>
    /// <exception cref="InvalidOperationException"/>
    public static explicit operator Color(CompactColor compactColor)
    {
        if (compactColor == default ||
            compactColor.ColorType is null)
            throw new InvalidOperationException($"Instance not set to {nameof(Color)}.");

        return (Color)compactColor.ColorType;
    }

    #region To make possible use of 'default' keyword

    /// <summary>
    /// Compares two <see cref="CompactColor"/>s so they must equal.
    /// </summary>
    /// <param name="right">The first <see cref="CompactColor"/> to compare.</param>
    /// <param name="left">The second <see cref="CompactColor"/> to compare.</param>
    /// <returns>Returns <see langword="true"/> if these objects are equal.</returns>
    public static bool operator ==(CompactColor right, CompactColor left) => right.Equals(left);

    /// <summary>
    /// Compares two <see cref="CompactColor"/>s so they must not equal.
    /// </summary>
    /// <param name="right">The first <see cref="CompactColor"/> to compare.</param>
    /// <param name="left">The second <see cref="CompactColor"/> to compare.</param>
    /// <returns>Returns <see langword="true"/> if these objects are not equal.</returns>
    public static bool operator !=(CompactColor right, CompactColor left) => !(right == left);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => base.Equals(obj);

    /// <inheritdoc/>
    public override int GetHashCode() => base.GetHashCode();

    #endregion
}