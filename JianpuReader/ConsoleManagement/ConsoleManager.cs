using System;
using System.Runtime.InteropServices;

public class ConsoleManager
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;
    private const int STD_OUTPUT_HANDLE = -11;

    public struct COORD
    {
        public short X;
        public short Y;
    }

    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }

    public struct CONSOLE_SCREEN_BUFFER_INFO
    {
        public COORD dwSize;
        public COORD dwCursorPosition;
        public ushort wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
    }

    public static void MaximizeConsoleWindow()
    {
        IntPtr consoleWindow = GetConsoleWindow();
        ShowWindow(consoleWindow, SW_MAXIMIZE);
    }

    public static void SetConsoleWidth(int width)
    {
        if (width < 1 || width > 32766)
        {
            throw new ArgumentOutOfRangeException("width", "Width must be between 1 and 32766.");
        }

        // Set the buffer width to the specified width
        Console.SetBufferSize(width, Console.BufferHeight);

        // Set the window width to the maximum allowed value
        Console.SetWindowSize(Console.LargestWindowWidth, Console.WindowHeight);
    }


}
