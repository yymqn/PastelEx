# PastelEx
Highly inspired by syntax of [Pastel](https://github.com/silkfire/Pastel) and [Crayon](https://github.com/riezebosch/crayon). Without these libraries PastelEx won't exist. Tested on Windows and Linux.

# What this library can offer?
It basically offers almost everything what Crayon does, however, this library can provide you:
- Colorize with hex strings, ConsoleColor, Color or even 8-bit colors.
- More text decorations, like Blinking, gradient text and more.
- Lightweight! Very fast and memory efficient. (see bellow)
- ... more

# Is it really that fast?
While string in C# is immutable, which means, you can't modify a string once you created it, PastelEx is fast enough to help you write a nice-looking console output without need of allocating a memory.

PastelEx does not allocate any memory (from benchmarks), when you aren't nesting colored strings. If you're, then there will be a small amount of bytes allocated because of string operations, which are needed to close the modified string.

Simple benchmark with nesting colors once:
```
|   Method |        Mean |     Error |    StdDev |   Gen0 | Allocated |
|--------- |------------:|----------:|----------:|-------:|----------:|
|   Pastel | 2,551.55 ns | 38.065 ns | 35.606 ns | 1.2665 |    2656 B | <-- worst
| PastelEx |    25.78 ns |  0.550 ns |  0.715 ns | 0.0382 |      80 B | <-- best
|   Crayon |   453.23 ns |  8.953 ns | 12.254 ns | 0.6266 |    1312 B |
```

As you can see from the benchmark, PastelEx is the best in it's speed and allocated memory per operation.
While Crayon is not that bad at memory allocation and it's slightly slower.
Pastel has the worst results in this benchmark, it's about 102 times slower than PastelEx and allocated 33 times more memory than PastelEx!

## What about benchmark without nesting?
PastelEx results are incredible! While both Crayon and Pastel allocated less memory, then PastelEx allocates zero of memory and it's even faster!
```
|   Method |       Mean |     Error |    StdDev |   Gen0 | Allocated |
|--------- |-----------:|----------:|----------:|-------:|----------:|
|   Pastel | 627.296 ns | 8.8328 ns | 8.2622 ns | 0.2327 |     488 B | <-- worst
| PastelEx |   3.733 ns | 0.0218 ns | 0.0193 ns |      - |         - | <-- best
|   Crayon | 167.562 ns | 0.3926 ns | 0.3065 ns | 0.2103 |     440 B |
```

**But why?**
Pastel uses regex every time it colorizes any string. Crayon uses StringBuilder to build output string, but PastelEx does not use any of them. Regex is very expensive and slow, Crayon uses StringBuilder, which is fast, but is allocated on heap. PastelEx uses simple string interpolation and stack allocated memory in most cases to process strings.

## Is it tested?
Code itself is tested, but it does not guarantee to work on all terminals.
You should never explicitly call `PastelEx.Enable()`! For Windows, PastelEx automatically checks on first use, if current terminal SHOULD be capable to show Ansi color codes and tries to enable it (virtual terminal processing), if needed - otherwise it'll disable PastelEx to show non-colored output.

![image](https://github.com/k-iro/PastelEx/assets/88717056/b492385e-5bb9-4b7f-8c76-ecb55cb20b8a)
