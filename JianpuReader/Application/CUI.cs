using JianpuReader.Controller;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Application
{
    internal class CUI
    {
        private bool finished = false;
        private DomainController _dc;

        public CUI(DomainController dc)
        {
            _dc = dc;
        }
        public void start()
        {

            bool restart;
            ConsoleManager.MaximizeConsoleWindow();
            while (true)
            {
                try
                {
                    _dc.loadDevices();
                    List<string> deviceList = _dc.getDeviceList();
                    Console.WriteLine("Available MIDI devices:");
                    for (int i = 0; i < deviceList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {deviceList[i]}");
                    }

                    Console.Write("Enter the number of the input device you want to use or press R to refresh: ");

                    string inputDeviceStr = Console.ReadLine().Trim();

                    // Check if the user wants to refresh the device list
                    if (inputDeviceStr.Equals("R", StringComparison.OrdinalIgnoreCase))
                    {
                        // If the user wants to refresh, start the loop again
                        continue;
                    }

                    // Try to parse the input string as an integer
                    if (int.TryParse(inputDeviceStr, out int inputDeviceIndex))
                    {
                        if (inputDeviceIndex >= 1 && inputDeviceIndex <= deviceList.Count)
                        {
                            _dc.pickDevice(inputDeviceIndex);
                            break;
                        }
                    }

                    Console.WriteLine("Please enter a valid device number!".Pastel(ConsoleColor.Red));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.Pastel(ConsoleColor.Red));
                    Console.ReadKey();
                    return;
                }
            }
            Console.Clear();

            string filePath;

#if DEBUG
            string songsDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Songs\");
#else
    string songsDirectory = "Songs/";
#endif

            // Get all .mid files in the Songs directory
            string[] midiFiles = Directory.GetFiles(songsDirectory, "*.mid");

            // List all .mid files
            Console.WriteLine("Select a MIDI file:");
            for (int i = 0; i < midiFiles.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(midiFiles[i])}");
            }

            // Prompt user to select a file
            int selection;
            while (true)
            {
                Console.Write("Enter the number of the MIDI file you want to play: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out selection) && selection >= 1 && selection <= midiFiles.Length)
                {
                    break;
                }
                Console.WriteLine("Invalid selection. Please try again.");
            }

            // Set the selected file path
            filePath = midiFiles[selection - 1];

            _dc.selectFile(filePath);
            if (DomainController.song == null)
            {
                Console.WriteLine("Song Invalid".Pastel(ConsoleColor.Red));
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }
            _dc.addHandler(clearConsole);

            do
            {
                Console.Clear();
                Console.WriteLine(DomainController.song.ToString());

                // Keep the program running until the user presses a key
                restart = WaitForKeyPress();

            } while (restart);
        }
        private bool WaitForKeyPress()
        {
            Console.WriteLine("Press 'R' to restart or any other key to exit");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            return keyInfo.Key == ConsoleKey.R;
        }

        public void clearConsole(object? sender, MidiEventReceivedEventArgs e)
        {

            if (e.Event.EventType == MidiEventType.NoteOn && ((NoteOnEvent)e.Event).Velocity > 0)
            {
                if (DomainController.song == null)
                {
                    Console.Clear();
                    Console.WriteLine("Song Invalid".Pastel(ConsoleColor.Red));
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }
                // Move the cursor position to the beginning of the console
                Console.SetCursorPosition(0, 0);
                if (DomainController.song.isCompleted)
                {
                    Console.Clear();
                    Console.WriteLine("Song Finished!");
                    Console.WriteLine();
                    Console.WriteLine(DomainController.song.showFullString());
                    return;
                }

                // Write the updated song representation to the console
                Console.WriteLine(DomainController.song.ToString());
                Console.WriteLine();
            }

        }
    }
}
