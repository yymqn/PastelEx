using PastelExtended;
using System.Drawing;
using System.Text.RegularExpressions;

/*
 *  PastelEx is about 6,1 times faster and 2,1 times more efficient in
 *  terms of memory allocation even with more features!
 */

// Supports default console colors and RGB colors even specified as hex string!
// Allowed hex string formats may or may not start with one '#' and should contain 3 hexadecimal
// chars #RGB or 6 hexadecimal chars #RRGGBB. You can also use System.Drawing.Color.
Console.WriteLine($"This is an {"example".Pastel("f0f")} line!".Pastel(Color.White));

// Gradient text is supported in more, than just two RGB colors. Smooth transition is guaranteed, but
// it also depends on how much characters you want to colorize.
Console.WriteLine(PastelEx.GradientBg(new string(' ', Console.WindowWidth), new Color[]
{
    Color.Red, Color.Yellow, Color.Green,
}));

// Styles are also supported, which means you can have text in bold, or italic, but this depends
// if your current terminal font supports it.
Console.WriteLine($"{"This text should be underlined".PastelDeco(Decoration.Underline)}, and this text {"should be blinking".PastelDeco(Decoration.RapidBlink)}!");

// Nesting is supported, for example, you can have gradient text with formatting inside.
Console.WriteLine(PastelEx.Gradient($"This text {$"have {"different".PastelDeco(Decoration.DoubleUnderline)} formats".PastelDeco(Decoration.Italic)}, however, you {"can't mix".PastelDeco(Decoration.Strikethrough)} another colors in gradient string.", new Color[] { Color.DeepPink, Color.DodgerBlue }));

// If you want, you can still use the default console colors.
Console.WriteLine($"This is the default {"red color".Pastel(ConsoleColor.Red)} defined by console and this is a {"blue color".Pastel(ConsoleColor.Blue)}.");

// You can also use regular expressions to match specific parts and colorize them.
var regex = new Regex(@"(\d+%)");
Console.WriteLine(regex.Replace("Current task 76%      Overall 4%", ctx => ctx.Value.Pastel(Color.Yellow)));

// 8-bit colors have exatly 256 combinations. If you want, you can use them
// instead of the default RGB colors, if you want, or for compatibility pursposes.
Console.WriteLine("8-bit colors are also supported!".Pastel(140));
Console.ReadLine();