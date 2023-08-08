using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Pastel;
using PastelExtended;
using System.Drawing;
using System.Text;
using static Crayon.Output;

/*

Last benchmark, where PastelEx is extended version and Pastel is original version.
As you can see, PastelEx is much faster and much more memory efficient.

|                                     Method |         Mean |      Error |     StdDev |   Gen0 | Allocated |
|------------------------------------------- |-------------:|-----------:|-----------:|-------:|----------:|
|            Pastel_SimpleColorization_Color |   623.957 ns |  3.6250 ns |  2.8302 ns | 0.2403 |     504 B |
|     Pastel_SimpleColorization_ConsoleColor |   607.941 ns |  3.6457 ns |  3.2318 ns | 0.2403 |     504 B |
|   Pastel_SimpleColorization_HexStringColor |   675.031 ns |  5.8237 ns |  5.1625 ns | 0.2594 |     544 B |
|                        Pastel_Nested_Color | 1,997.118 ns |  9.2122 ns |  8.1663 ns | 0.8125 |    1705 B |
|                 Pastel_Nested_ConsoleColor | 1,956.879 ns | 21.2824 ns | 19.9076 ns | 0.8125 |    1705 B |
|               Pastel_Nested_HexStringColor | 2,081.262 ns | 28.3969 ns | 26.5625 ns | 0.8507 |    1785 B |
|                                            |              |            |            |        |           |
|          PastelEx_SimpleColorization_Color |     1.447 ns |  0.0156 ns |  0.0146 ns |      - |         - |
|   PastelEx_SimpleColorization_ConsoleColor |     1.333 ns |  0.0108 ns |  0.0101 ns |      - |         - |
| PastelEx_SimpleColorization_HexStringColor |     1.715 ns |  0.0302 ns |  0.0236 ns |      - |         - |
|                      PastelEx_Nested_Color |    19.340 ns |  0.4254 ns |  0.7224 ns | 0.0382 |      80 B |
|               PastelEx_Nested_ConsoleColor |    17.413 ns |  0.0800 ns |  0.0668 ns | 0.0382 |      80 B |
|             PastelEx_Nested_HexStringColor |    18.691 ns |  0.4293 ns |  0.5272 ns | 0.0382 |      80 B |
*/

//BenchmarkRunner.Run<CrayonVsPastelVsPastelExBenchmark>();
BenchmarkRunner.Run<Benchmark>();

// Three simple benchmarks to test Pastel vs PastelEx vs Crayon
[MemoryDiagnoser]
public class CrayonVsPastelVsPastelExBenchmark
{
    [Benchmark]
    public string Pastel() => ConsoleExtensions.Pastel($"This is my string", Color.Aqua);

    [Benchmark]
    public string PastelXX() => PastelEx.Pastel($"This is my string", Color.Aqua);

    [Benchmark]
    public string Crayon() => Cyan($"This is my string");
}

// Pastel vs PastelEx performance
[MemoryDiagnoser]
public class Benchmark
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
}