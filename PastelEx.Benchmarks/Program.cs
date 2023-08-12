using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Pastel;
using PastelExtended;
using System.Drawing;

/*

  * The benchmark results provided bellow are intended to give you a general idea of the relative
    performance of the different methods. Please keep in mind that these results are based on a specific test environment
    and may vary on different machines or under different conditions.


    |                                     Method |         Mean |      Error |     StdDev |   Gen0 | Allocated |
    |------------------------------------------- |-------------:|-----------:|-----------:|-------:|----------:|
    |            Pastel_SimpleColorization_Color |   628.842 ns |  4.4861 ns |  4.1963 ns | 0.2403 |     504 B |
    |     Pastel_SimpleColorization_ConsoleColor |   624.715 ns |  3.3022 ns |  2.9273 ns | 0.2403 |     504 B |
    |   Pastel_SimpleColorization_HexStringColor |   666.525 ns |  2.9819 ns |  2.4900 ns | 0.2594 |     544 B |
    |                        Pastel_Nested_Color | 1,985.768 ns |  9.5572 ns |  8.9398 ns | 0.8125 |    1705 B |
    |                 Pastel_Nested_ConsoleColor | 1,990.269 ns | 24.3285 ns | 18.9941 ns | 0.8125 |    1705 B |
    |               Pastel_Nested_HexStringColor | 2,122.618 ns | 41.4472 ns | 40.7067 ns | 0.8507 |    1785 B |
    |          PastelEx_SimpleColorization_Color |     4.030 ns |  0.1042 ns |  0.1200 ns |      - |         - |
    |   PastelEx_SimpleColorization_ConsoleColor |     1.377 ns |  0.0702 ns |  0.0657 ns |      - |         - |
    | PastelEx_SimpleColorization_HexStringColor |     3.041 ns |  0.0949 ns |  0.0842 ns |      - |         - |
    |                      PastelEx_Nested_Color |    24.482 ns |  0.1689 ns |  0.1580 ns | 0.0382 |      80 B |
    |               PastelEx_Nested_ConsoleColor |    18.630 ns |  0.1168 ns |  0.1035 ns | 0.0382 |      80 B |
    |             PastelEx_Nested_HexStringColor |    24.961 ns |  0.1878 ns |  0.1569 ns | 0.0382 |      80 B |
    |                     PastelEx_SimpleStyling |     1.271 ns |  0.0231 ns |  0.0205 ns |      - |         - |
    |                     PastelEx_NestedStyling |    18.276 ns |  0.0572 ns |  0.0477 ns | 0.0306 |      64 B |
    |                   PastelEx_SimpleGradience |     1.519 ns |  0.0084 ns |  0.0074 ns |      - |         - |

*/

BenchmarkRunner.Run<Benchmark>();
Console.Read();

[MemoryDiagnoser]
#pragma warning disable CA1050
#pragma warning disable CA1822
public class Benchmark
#pragma warning restore CA1050
{
    #region Pastel
    [Benchmark]
    public string Pastel_SimpleColorization_Color() => ConsoleExtensions.Pastel("This is my colorized string", Color.Aqua);
    [Benchmark]
    public string Pastel_SimpleColorization_ConsoleColor() => ConsoleExtensions.Pastel("This is my colorized string", ConsoleColor.Cyan);
    [Benchmark]
    public string Pastel_SimpleColorization_HexStringColor() => ConsoleExtensions.Pastel("This is my colorized string", "#00ffff");

    [Benchmark]
    public string Pastel_Nested_Color() => ConsoleExtensions.Pastel($"This is {ConsoleExtensions.PastelBg("my colorized", Color.White)} string", Color.Aqua);
    [Benchmark]
    public string Pastel_Nested_ConsoleColor() => ConsoleExtensions.Pastel($"This is {ConsoleExtensions.PastelBg("my colorized", ConsoleColor.White)} string", ConsoleColor.Cyan);
    [Benchmark]
    public string Pastel_Nested_HexStringColor() => ConsoleExtensions.Pastel($"This is {ConsoleExtensions.PastelBg("my colorized", "#ffffff")} string", "#00ffff");
    #endregion

    #region PastelEx
    [Benchmark]
    public string PastelEx_SimpleColorization_Color() => PastelEx.Fg("This is my colorized string", Color.Aqua);
    [Benchmark]
    public string PastelEx_SimpleColorization_ConsoleColor() => PastelEx.Fg("This is my colorized string", ConsoleColor.Cyan);
    [Benchmark]
    public string PastelEx_SimpleColorization_HexStringColor() => PastelEx.Fg("This is my colorized string", "#00ffff");

    [Benchmark]
    public string PastelEx_Nested_Color() => PastelEx.Fg($"This is {PastelEx.Bg("my colorized", Color.White)} string", Color.Aqua);
    [Benchmark]
    public string PastelEx_Nested_ConsoleColor() => PastelEx.Fg($"This is {PastelEx.Bg("my colorized", ConsoleColor.White)} string", ConsoleColor.Cyan);
    [Benchmark]
    public string PastelEx_Nested_HexStringColor() => PastelEx.Fg($"This is {PastelEx.Bg("my colorized", "#ffffff")} string", "#00ffff");
    #endregion

    #region Other PastelEx features
    [Benchmark]
    public string PastelEx_SimpleStyling() => PastelEx.Deco("This text is cool!", Decoration.Italic);
    [Benchmark]
    public string PastelEx_NestedStyling() => PastelEx.Deco($"This text is {PastelEx.Deco("cool", Decoration.Underline)}!", Decoration.Italic);

    static readonly Color[] gradientColors = { Color.Red, Color.Yellow, Color.Lime };
    [Benchmark]
    public string PastelEx_SimpleGradience() => PastelEx.Gradient($"This text will be gradient ...", gradientColors);

    #endregion
}
#pragma warning restore CA1822