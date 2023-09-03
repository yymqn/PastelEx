![Logo](https://raw.githubusercontent.com/k-iro/PastelEx/master/img/logo.png)

# PastelEx 💥
**Elevate Console Output with Colors and Styles**

Simplify coloring console text with PastelEx, inspired by [Pastel](https://github.com/silkfire/Pastel) and [Crayon](https://github.com/riezebosch/crayon).

Tested on Windows and Linux terminals.
Download on [nuget](https://www.nuget.org/packages/PastelEx)!

## 🎨 Inspiration
This library simplifies the process of applying ANSI color codes to strings, mitigating several potential drawbacks:
- If a terminal lacks ANSI support, the output might display the string with visible ANSI codes, as the terminal wouldn't recognize them as formatting instructions.
- Manually composing ANSI codes can be intricate for humans, involving the need to write out each code explicitly.

## ⚙️ How It Works
Example:
```
using PastelExtended;
using System.Drawing;

Console.WriteLine($"This text is {"yellow".Fg("#ffff00")}!".Fg(Color.White));
Console.WriteLine(PastelEx.Gradient("This text is gradient.", new[] { Color.Magenta, Color.Aqua }));
```

If needed or desired, you can modify PastelEx's behavior using the `PastelSettings` class.
To adjust the settings, simply modify the `PastelEx.Settings` property.

## ❓ When to Use
For adding color or decoration in small console apps.

## 🚫 NO_COLOR Compatibility
Respects the `NO_COLOR` environment variable. If this variable exists with any value, colors and decorations will be disabled by default.

## 🧪 Tested
Tested for accurate rendering. If you find any issues, please report them via the [issues](https://github.com/k-iro/PastelEx/issues) page.

## ⚡ Performance
Efficient, minimal memory use.

Benchmark (lower values are better):
```
|   Method  |      Mean   |   Gen 0   | Allocated |
|-----------|------------:|----------:|----------:|
|   Pastel  | 2,551.55 ns |  1.2665   |   2656 B  |
| PastelEx  |    25.78 ns |  0.0382   |     80 B  |
|   Crayon  |   453.23 ns |  0.6266   |   1312 B  |
```

![Example Image](https://raw.githubusercontent.com/k-iro/PastelEx/master/img/example.png)