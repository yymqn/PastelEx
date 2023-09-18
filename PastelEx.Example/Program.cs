using PastelExtended;
using System.Drawing;
using System.Text.RegularExpressions;

Console.WriteLine(PastelEx.GetInformation($"Modified {"string".Bg(Color.Orange)}!".Fg(Color.Aqua).Deco(Decoration.Bold)).OriginalLength);

// Supports default console colors and RGB colors even specified as hex string!
// Allowed hex string formats may or may not start with one '#' and should contain 3 hexadecimal
// chars #RGB or 6 hexadecimal chars #RRGGBB. You can also use System.Drawing.Color.
Console.WriteLine($"\n  This is an {"example".Fg("f0f")} line!".Fg(Color.White));

// Gradient text is supported in more, than just two RGB colors. Smooth transition is guaranteed, but
// it also depends on how much characters you want to colorize.
Console.WriteLine("  " + PastelEx.GradientBg(new string(' ', Console.WindowWidth / 2), new Color[]
{
    Color.Red, Color.Yellow, Color.Green,
}));

// Styles are also supported, which means you can have text in bold, or italic, but this depends
// if your current terminal font supports it.
Console.WriteLine($"  {"This text should be underlined".Deco(Decoration.Underline)}, and this text {"should be blinking".Deco(Decoration.SlowBlink)}!");

// Nesting is supported, for example, you can have gradient text with formatting inside.
Console.WriteLine(PastelEx.Gradient($"  This text {$"have {"different".Deco(Decoration.DoubleUnderline)} formats".Deco(Decoration.Italic)}, however, you {"can't mix".Deco(Decoration.Strikethrough)} another colors in gradient string.", new Color[] { Color.DeepPink, Color.DodgerBlue }));

// If you want, you can still use the default console colors.
Console.WriteLine($"  This is the default {"red color".Fg(ConsoleColor.Red)} defined by console and this is a {"blue color".Fg(ConsoleColor.Blue)}.");

// You can also use regular expressions to match specific parts and colorize them.
Console.WriteLine(Regex.Replace("  Running tasks: 5%, 78%, 0%, 32% (overall 28%)",
    @"(\d+%)",
    ctx => ctx.Value.Fg(Color.Yellow)));