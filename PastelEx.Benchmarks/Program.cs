using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Pastel;
using PastelExtended;
using System.Drawing;

/*

  * The benchmark results provided bellow are intended to give you a general idea of the relative
    performance of the different methods. Please keep in mind that these results are based on a specific test environment
    and may vary on different machines or under different conditions.

  * Benchmark results are updated with every new version!


    |                                     Method |         Mean |     Error |    StdDev |   Gen0 | Allocated |
    |------------------------------------------- |-------------:|----------:|----------:|-------:|----------:|
    |            Pastel_SimpleColorization_Color |   631.358 ns | 8.7994 ns | 8.2309 ns | 0.2403 |     504 B |
    |     Pastel_SimpleColorization_ConsoleColor |   639.742 ns | 3.3789 ns | 3.1606 ns | 0.2403 |     504 B |
    |   Pastel_SimpleColorization_HexStringColor |   689.632 ns | 5.8124 ns | 5.4369 ns | 0.2594 |     544 B |
    |                        Pastel_Nested_Color | 2,013.374 ns | 4.1165 ns | 3.6492 ns | 0.8125 |    1705 B |
    |                 Pastel_Nested_ConsoleColor | 2,214.842 ns | 9.3486 ns | 8.7447 ns | 0.8125 |    1705 B |
    |               Pastel_Nested_HexStringColor | 2,095.999 ns | 5.3368 ns | 4.4564 ns | 0.8507 |    1785 B |
    |          PastelEx_SimpleColorization_Color |     4.357 ns | 0.0343 ns | 0.0321 ns |      - |         - |
    |   PastelEx_SimpleColorization_ConsoleColor |     2.315 ns | 0.0203 ns | 0.0190 ns |      - |         - |
    | PastelEx_SimpleColorization_HexStringColor |     3.935 ns | 0.0520 ns | 0.0435 ns |      - |         - |
    |                      PastelEx_Nested_Color |    26.588 ns | 0.1691 ns | 0.1581 ns | 0.0382 |      80 B |
    |               PastelEx_Nested_ConsoleColor |    20.523 ns | 0.3256 ns | 0.2718 ns | 0.0382 |      80 B |
    |             PastelEx_Nested_HexStringColor |    28.025 ns | 0.5471 ns | 0.4850 ns | 0.0382 |      80 B |
    |                     PastelEx_SimpleStyling |     2.707 ns | 0.0322 ns | 0.0269 ns |      - |         - |
    |                     PastelEx_NestedStyling |    22.820 ns | 0.4083 ns | 0.3619 ns | 0.0306 |      64 B |
    |                   PastelEx_SimpleGradience |     2.297 ns | 0.0074 ns | 0.0066 ns |      - |         - |

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