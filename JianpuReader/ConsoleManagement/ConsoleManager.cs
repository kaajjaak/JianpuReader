using System.Runtime.InteropServices;

public class ConsoleManager
{
    [DllImport("kernel32.dll", ExactSpelling = true)]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;

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

        Console.SetBufferSize(width, Console.BufferHeight);
        Console.SetWindowSize(Math.Min(width, Console.LargestWindowWidth), Console.WindowHeight);
    }
}
