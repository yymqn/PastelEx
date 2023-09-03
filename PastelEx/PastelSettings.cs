namespace PastelExtended;
/// <summary>
/// Settings class to modify PastelEx runtime behaviour.
/// </summary>
public sealed class PastelSettings
{
    internal PastelSettings()
    { }

    /// <summary>
    /// If set to <see langword="true"/>, changes to color or style using the <see cref="PastelEx.Foreground"/>,
    /// <see cref="PastelEx.Background"/>, and <see cref="PastelEx.Decorations"/> properties, or the <see cref="PastelEx.Reset"/>
    /// methods, will be immediately applied to the console output. However, overall performance might degrade when frequently
    /// modifying any of the previously specified properties or methods. If set to <see langword="false"/>, the updates will only
    /// take effect when using any of the PastelEx methods (not properties) to modify the output.
    /// </summary>
    public bool InstantRecolor { get; set; } = true;

    /// <summary>
    /// If set to <see langword="true"/>, the console output will be colored as intended. If colored output is not supported,
    /// the <see cref="PastelEx.Supported"/> property will be set to <see langword="false"/>, and this property will have no
    /// effect on the console output.
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// A color palette to be used for coloring the console. If the color type you use does not match the required palette,
    /// it will be automatically converted into the nearest opposite color.
    /// </summary>
    /// <remarks>
    /// If you're using both <see cref="System.Drawing.Color"/> and <see cref="ConsoleColor"/>, then you might consider
    /// changing this to one of <see cref="ColorPalette.Color"/> or <see cref="ColorPalette.ConsoleColor"/>.
    /// </remarks>
    public ColorPalette Palette { get; set; } = ColorPalette.Both;
}
