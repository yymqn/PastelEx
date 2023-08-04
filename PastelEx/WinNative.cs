using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PastelExtended;
internal partial class WinNative
{
    [LibraryImport(Kernel32DllName, EntryPoint = "GetConsoleMode")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetConsoleMode(nint hConsoleHandle, out uint lpMode);

    [LibraryImport(Kernel32DllName, EntryPoint = "SetConsoleMode")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool SetConsoleMode(nint hConsoleHandle, uint dwMode);

    [LibraryImport(Kernel32DllName, EntryPoint = "GetStdHandle", SetLastError = true)]
    internal static partial nint GetStdHandle(int nStdHandle);

    private const string Kernel32DllName = "kernel32";

    private const int STD_OUTPUT_HANDLE = -11;
    private const uint ENABLE_PROCESSED_OUTPUT = 0x0001;
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    internal static void EnableVirtualProcessing()
    {
        var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
        _ = GetConsoleMode(iStdOut, out var outConsoleMode)
                && SetConsoleMode(iStdOut, outConsoleMode | ENABLE_PROCESSED_OUTPUT | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
    }
}
