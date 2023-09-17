using System.Runtime.InteropServices;

namespace PastelExtended;
internal static partial class WinNative
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

    internal static bool EnableIfSupported()
    {
        if (PastelEx.IsWindows)
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (iStdOut == IntPtr.Zero)
            {
                return false;
            }

            if (!GetConsoleMode(iStdOut, out uint outConsoleMode))
            {
                return false;
            }

            outConsoleMode |= ENABLE_VIRTUAL_TERMINAL_PROCESSING;
            if (!SetConsoleMode(iStdOut, outConsoleMode))
            {
                return false;
            }
        }

        return true;
    }
}
