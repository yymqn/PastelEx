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
    |            Pastel_SimpleColorization_Color |   627.534 ns |  5.0872 ns |  4.2480 ns | 0.2403 |     504 B |
    |     Pastel_SimpleColorization_ConsoleColor |   620.943 ns |  5.0290 ns |  4.1994 ns | 0.2403 |     504 B |
    |   Pastel_SimpleColorization_HexStringColor |   676.346 ns |  2.8735 ns |  2.6879 ns | 0.2594 |     544 B |
    |                        Pastel_Nested_Color | 2,022.655 ns |  9.4569 ns |  8.8460 ns | 0.8125 |    1705 B |
    |                 Pastel_Nested_ConsoleColor | 1,986.229 ns | 10.2794 ns |  9.1124 ns | 0.8125 |    1705 B |
    |               Pastel_Nested_HexStringColor | 2,105.130 ns | 13.8201 ns | 12.9274 ns | 0.8507 |    1785 B |
    |          PastelEx_SimpleColorization_Color |     3.714 ns |  0.0032 ns |  0.0027 ns |      - |         - |
    |   PastelEx_SimpleColorization_ConsoleColor |     1.581 ns |  0.0137 ns |  0.0121 ns |      - |         - |
    | PastelEx_SimpleColorization_HexStringColor |     3.036 ns |  0.0465 ns |  0.0435 ns |      - |         - |
    |                      PastelEx_Nested_Color |    23.726 ns |  0.0608 ns |  0.0539 ns | 0.0382 |      80 B |
    |               PastelEx_Nested_ConsoleColor |    17.174 ns |  0.1406 ns |  0.1174 ns | 0.0382 |      80 B |
    |             PastelEx_Nested_HexStringColor |    24.651 ns |  0.1570 ns |  0.1311 ns | 0.0382 |      80 B |
    |                     PastelEx_SimpleStyling |     1.224 ns |  0.0142 ns |  0.0126 ns |      - |         - |
    |                     PastelEx_NestedStyling |    17.501 ns |  0.0589 ns |  0.0492 ns | 0.0306 |      64 B |
    |                   PastelEx_SimpleGradience |     1.648 ns |  0.0880 ns |  0.1047 ns |      - |         - |

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
    public string PastelEx_SimpleColorization_Color() => PastelEx.Pastel("This is my colorized string", Color.Aqua);
    [Benchmark]
    public string PastelEx_SimpleColorization_ConsoleColor() => PastelEx.Pastel("This is my colorized string", ConsoleColor.Cyan);
    [Benchmark]
    public string PastelEx_SimpleColorization_HexStringColor() => PastelEx.Pastel("This is my colorized string", "#00ffff");

    [Benchmark]
    public string PastelEx_Nested_Color() => PastelEx.Pastel($"This is {PastelEx.PastelBg("my colorized", Color.White)} string", Color.Aqua);
    [Benchmark]
    public string PastelEx_Nested_ConsoleColor() => PastelEx.Pastel($"This is {PastelEx.PastelBg("my colorized", ConsoleColor.White)} string", ConsoleColor.Cyan);
    [Benchmark]
    public string PastelEx_Nested_HexStringColor() => PastelEx.Pastel($"This is {PastelEx.PastelBg("my colorized", "#ffffff")} string", "#00ffff");
    #endregion

    #region Other PastelEx features
    [Benchmark]
    public string PastelEx_SimpleStyling() => PastelEx.PastelDeco("This text is cool!", Decoration.Italic);
    [Benchmark]
    public string PastelEx_NestedStyling() => PastelEx.PastelDeco($"This text is {PastelEx.PastelDeco("cool", Decoration.Underline)}!", Decoration.Italic);

    static readonly Color[] gradientColors = { Color.Red, Color.Yellow, Color.Lime };
    [Benchmark]
    public string PastelEx_SimpleGradience() => PastelEx.Gradient($"This text will be gradient ...", gradientColors);

    #endregion
}
#pragma warning restore CA1822