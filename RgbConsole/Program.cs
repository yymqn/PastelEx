using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
//using Pastel;
using PastelExtended;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using static Crayon.Output;

// PastelEx 10x faster, 0.5 ratio of memory

// WORKING CODE
Console.WriteLine($"Your current health status is {"very good".Pastel("f0f").PastelDec(Decoration.RapidBlink, Decoration.DoubleUnderline)}!".Pastel(Color.White));

var smth =
    PastelEx.Gradient(
        $"|||{$"|||{"|||".PastelDec(Decoration.Underline)}|||".PastelDec(Decoration.RapidBlink)}|||".PastelDec(Decoration.Italic),
        new Color[] { Color.Red, Color.Yellow, Color.Lime });
Console.WriteLine(smth);

Console.WriteLine($"AAAAAAAAAAAAAAAAA{$"AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA{"AAAAAAAA".PastelDec(Decoration.SlowBlink, Decoration.Invert)}AAAAAAAAAA".PastelDec(Decoration.Underline)}AAAAAAAAAAAAAAAAAAAAAAA".PastelDec(Decoration.Italic));

Console.WriteLine(PastelEx.GradientBg(new string(' ', Console.WindowWidth), new Color[]
{
    Color.Red,
    Color.Yellow,
    Color.Green,
}));

Console.Write(new string('#', Console.WindowWidth));


//Console.WriteLine(ConsoleExtensions.Pastel($"Hello, {ConsoleExtensions.Pastel($"wo{ConsoleExtensions.PastelBg("oooooooo", Color.White)}oorld", Color.Blue)}!", Color.Red));
//BenchmarkRunner.Run<PastelBenchmark>();

/*[MemoryDiagnoser]
public class PastelBenchmark
{
    [Benchmark]
    public string OriginalPastelVersion()
    {
        return ConsoleExtensions.Pastel($"Hello, {ConsoleExtensions.Pastel($"wo{ConsoleExtensions.PastelBg("oooooooo", Color.White)}oorld", Color.Blue)}!", Color.Red);
    }

    [Benchmark]
    public string PastelExtendedVersion()
    {
        return PastelEx.Pastel($"Hello, {PastelEx.Pastel($"wo{PastelEx.PastelBg("oooooooo", Color.White)}oorld", Color.Blue)}!", Color.Red);
    }
}*/