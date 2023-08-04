using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastelExtended;
public enum Decoration : byte
{
    Bold = 1,
    Dim = 2,
    Italic = 3,
    Underline = 4,
    SlowBlink = 5,
    RapidBlink = 6,
    Invert = 7,
    Strikethrough = 9,
    DoubleUnderline = 21
}

internal enum ColorPlane : byte
{
    Foreground,
    Background
}