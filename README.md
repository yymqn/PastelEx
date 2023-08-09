# PastelEx
Highly inspired by the syntax of [Pastel](https://github.com/silkfire/Pastel) and [Crayon](https://github.com/riezebosch/crayon). Without these libraries, PastelEx wouldn't exist.
Tested on both Windows and Linux.

# Inspiration
Adding ANSI color codes can be a confusing and challenging task to do manually. This library makes coloring or styling console output a very easy task.

# How It Works
Using a simple syntax, like `"My string".Pastel(Color.Red)`, wraps your string in a special Unicode character sequence that instructs the terminal: "This text should be colored."
You can also do it manually, but using this library offers you a few more benefits: ease of use and automatic checks for terminal support of ANSI color codes.

# Any Performance Benefits?
A string in C# is immutable, meaning you can't modify it once created. However, PastelEx is efficient enough to help you create a visually appealing console output without requiring a lot of memory allocation.
PastelEx doesn't allocate any memory when you're not nesting colors. Below, you can see a benchmark with nested colors.

```
|   Method |        Mean |     Error |    StdDev |   Gen0 | Allocated |
|--------- |------------:|----------:|----------:|-------:|----------:|
|   Pastel | 2,551.55 ns | 38.065 ns | 35.606 ns | 1.2665 |    2656 B | <-- worst
| PastelEx |    25.78 ns |  0.550 ns |  0.715 ns | 0.0382 |      80 B | <-- best
|   Crayon |   453.23 ns |  8.953 ns | 12.254 ns | 0.6266 |    1312 B |
```

PastelEx performs the best in terms of performance and memory allocation!

## Has It Been Tested?
The code itself is tested to ensure that it outputs colors and styles as the programmer intended.
However, you should never explicitly call `PastelEx.Enable()`! For Windows, PastelEx automatically checks on the first use if the current terminal can display ANSI color codes. It tries to enable them if necessary; otherwise, it disables PastelEx to show non-colored output.
Tested on both Windows and Linux.

# NO_COLOR
This library checks whether the user has explicitly disabled ANSI color codes by adding an environment variable `NO_COLOR` with any value. If this environment variable exists, color output will automatically be disabled.

![Example Image](img/example1.png)