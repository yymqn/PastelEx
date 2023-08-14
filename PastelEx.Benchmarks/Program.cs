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
    |            Pastel_SimpleColorization_Color |   634.269 ns |  7.9934 ns |  7.4770 ns | 0.2403 |     504 B |
    |     Pastel_SimpleColorization_ConsoleColor |   653.720 ns | 13.0893 ns | 21.5060 ns | 0.2403 |     504 B |
    |   Pastel_SimpleColorization_HexStringColor |   713.199 ns | 13.8742 ns | 18.5216 ns | 0.2594 |     544 B |
    |                        Pastel_Nested_Color | 2,088.252 ns |  9.2292 ns |  8.6330 ns | 0.8125 |    1705 B |
    |                 Pastel_Nested_ConsoleColor | 2,062.392 ns | 14.6417 ns | 12.2265 ns | 0.8125 |    1705 B |
    |               Pastel_Nested_HexStringColor | 2,185.443 ns | 18.4817 ns | 16.3835 ns | 0.8507 |    1785 B |
    |          PastelEx_SimpleColorization_Color |     3.852 ns |  0.0252 ns |  0.0224 ns |      - |         - |
    |   PastelEx_SimpleColorization_ConsoleColor |     1.056 ns |  0.0409 ns |  0.0383 ns |      - |         - |
    | PastelEx_SimpleColorization_HexStringColor |     2.902 ns |  0.0735 ns |  0.0688 ns |      - |         - |
    |                      PastelEx_Nested_Color |    29.303 ns |  0.4584 ns |  0.4288 ns | 0.0382 |      80 B |
    |               PastelEx_Nested_ConsoleColor |    21.367 ns |  0.3201 ns |  0.2499 ns | 0.0382 |      80 B |
    |             PastelEx_Nested_HexStringColor |    30.726 ns |  0.6725 ns |  1.2957 ns | 0.0382 |      80 B |
    |                     PastelEx_SimpleStyling |     1.378 ns |  0.0785 ns |  0.0696 ns |      - |         - |
    |                     PastelEx_NestedStyling |    21.551 ns |  0.1874 ns |  0.1753 ns | 0.0306 |      64 B |
    |                   PastelEx_SimpleGradience |     1.262 ns |  0.0395 ns |  0.0350 ns |      - |         - |

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