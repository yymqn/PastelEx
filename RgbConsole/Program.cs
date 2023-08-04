using PastelExtended;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using static Crayon.Output;

/*var f = Formatter.ColorForeground($"Hello, {Formatter.ColorForeground($"w{Formatter.ColorForeground("or", Color.Orange)}ld", Color.Blue)}!", Color.Red);
var pastel = $"Hello, {$"w{"or".Pastel(Color.Orange)}ld".Pastel(Color.Blue)}!".Pastel(Color.Red);

Console.WriteLine(f);
Console.WriteLine(pastel);

Console.WriteLine(Formatter.ColorForeground("Hello", Color.Orange));*/

Console.WriteLine($"Your current health status is {"very good".Pastel("f0f").PastelDec(Decoration.RapidBlink, Decoration.DoubleUnderline)}!".Pastel(Color.White));

// broken a bit
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

Console.ReadKey(true);