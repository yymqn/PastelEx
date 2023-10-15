namespace PastelExtended;
/// <summary>
/// Represents a list containing text decorations.
/// </summary>
public sealed class DecorationCollection
{
    internal DecorationCollection() { }
    readonly HashSet<Decoration> decorationList = new();
    private readonly object _sync = new();

    /// <summary>
    /// Adds a decoration to the list.
    /// </summary>
    /// <param name="decoration">The decoration.</param>
    public void Add(Decoration decoration)
    {
        lock (_sync)
        {
            decorationList.Add(decoration);
            updated = true;

            if (PastelEx.EnabledInternal && PastelEx.Settings.InstantRecolor)
            {
                Console.Write(Formatter.DefaultFormat);
            }
        }
    }

    /// <summary>
    /// Removes a decoration from the list.
    /// </summary>
    /// <param name="decoration">The decoration.</param>
    /// <returns><see langword="true"/> if value has been removed; otherwise <see langword="false"/></returns>
    public bool Remove(Decoration decoration)
    {
        lock (_sync)
        {
            var value = decorationList.Remove(decoration);
            updated = true;

            if (value && PastelEx.EnabledInternal && PastelEx.Settings.InstantRecolor)
            {
                Console.Write(Formatter.DefaultFormat);
            }

            return value;
        }
    }

    /// <summary>
    /// Determines, if decoration is already added.
    /// </summary>
    /// <param name="decoration"></param>
    /// <returns><see langword="true"/> if value exists; otherwise <see langword="false"/></returns>
    public bool Has(Decoration decoration) => decorationList.Contains(decoration);

    /// <summary>
    /// Removes all decorations from the list.
    /// </summary>
    public void Clear()
    {
        lock (_sync)
        {
            decorationList.Clear();
            updated = true;

            if (PastelEx.EnabledInternal && PastelEx.Settings.InstantRecolor)
            {
                Console.Write(Formatter.DefaultFormat);
            }
        }
    }

    private bool updated = false;
    private string cache = string.Empty;

    /// <summary>
    /// Total count of currently set decorations.
    /// </summary>
    public int Count => decorationList.Count;

    internal new string ToString()
    {
        lock (_sync)
        {
            if (!updated)
            {
                return cache;
            }

            updated = false;
            Span<char> chars = stackalloc char[6 * decorationList.Count];
            int length = 0;

            for (int i = 0; i < decorationList.Count; i++)
            {
                var format = $"\u001b[{(byte)decorationList.ElementAt(i)}m";
                format.CopyTo(chars[length..]);
                length += format.Length;
            }

            cache = chars[..length].ToString();
            return cache;
        }
    }
}
