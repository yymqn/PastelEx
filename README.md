# PastelEx
Highly inspired by syntax of [Pastel](https://github.com/silkfire/Pastel) and [Crayon](https://github.com/riezebosch/crayon). Without these libraries PastelEx won't exist.

# What this library can offer?
It basically offers almost everything what Crayon does, however, this library can provide you:
- Colorize with hex strings, ConsoleColor, Color or even 8-bit colors.
- More text decorations, like Blinking, gradient text and more.
- Lightweight! Very fast and memory efficient. (see bellow)

# Is it really that fast?
While string in C# is immutable, which means, you can't modify a string once you created it, PastelEx is fast enough to help you write a nice-looking console output without need of allocating a memory.

PastelEx does not allocate any memory (from benchmarks), when you aren't nesting colored strings. If you're, then there will be a small amount of bytes allocated because of string operations, which are needed to close the modified string.

Simple benchmark with nesting colors once:
```
|   Method |        Mean |     Error |    StdDev |   Gen0 | Allocated |
|--------- |------------:|----------:|----------:|-------:|----------:|
|   Pastel | 2,551.55 ns | 38.065 ns | 35.606 ns | 1.2665 |    2656 B |
| PastelEx |    25.78 ns |  0.550 ns |  0.715 ns | 0.0382 |      80 B |
|   Crayon |   453.23 ns |  8.953 ns | 12.254 ns | 0.6266 |    1312 B |
```

As you can see from the benchmark, PastelEx is the best in it's speed and allocated memory per operation.
While Crayon is not that bad at memory allocation and it's slightly slower.
Pastel has the worst results in this benchmark, it's about 102 times slower than PastelEx and allocated 33 times more memory than PastelEx!
