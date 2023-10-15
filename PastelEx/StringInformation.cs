using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastelExtended;
/// <summary>
/// Gets the properties of the string, which have been modified by PastelEx, like it's original length excluding any colors and formats.
/// </summary>
public readonly struct StringInformation
{
    internal StringInformation(string modifiedString)
    {
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
    /// The original length of the string, excluding any of the formats applied.
    /// </summary>
    public int OriginalLength { get; }
}
