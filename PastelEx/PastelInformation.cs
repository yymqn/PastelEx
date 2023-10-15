using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastelExtended;
/// <summary>
/// Gets the properties of the string, which have been modified by PastelEx, like it's original length excluding any colors and formats.
/// </summary>
public readonly struct PastelInformation
{
    private readonly string _modifiedString;
    internal PastelInformation(string modifiedString)
    {
        _modifiedString = modifiedString;
        var stringSpan = modifiedString.AsSpan();
        int length = 0;

        for (int i = 0; i < stringSpan.Length; i++)
        {
            var current = stringSpan[i];

            if (current == '\u001b')
            {
                i += stringSpan[i..].IndexOf('m');
                continue;
            }

            length++;
        }

        OriginalLength = length;
    }

    /// <summary>
    /// An operator to explicitly convert a <see cref="string"/> into a <see cref="PastelInformation"/>.
    /// </summary>
    /// <param name="modifiedString">The original string to be used.</param>
    public static explicit operator PastelInformation(string modifiedString)
    {
        return PastelEx.GetInformation(modifiedString);
    }

    /// <summary>
    /// The original length of the string, excluding any of the formats applied.
    /// </summary>
    public int OriginalLength { get; }
}
