using System;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using JianpuReader.Midi;
using Pastel;
using JianpuReader.Controller;

internal class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        DomainController dc = new DomainController();
        bool restart;
        MidiDeviceManager midiDeviceManager = new MidiDeviceManager(new MidiEventHandler(new MidiChecker()));
        while (true)
        {
            try
            {
                List<string> deviceList = dc.getDeviceList();
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
                        dc.pickDevice(inputDeviceIndex);
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
        char inputChar;
        do
        {
            Console.WriteLine("How do you want to import the file:");
            Console.WriteLine("1. Import from file");
            Console.WriteLine("2. Import from Songs Folder");
            inputChar = Console.ReadKey().KeyChar;
            if (inputChar == '1')
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "MIDI files (*.mid)|*.mid|All files (*.*)|*.*";
                openFileDialog.Title = "Select a MIDI file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = openFileDialog.FileName;
                    Console.WriteLine("Selected file: " + selectedFile);
                }
            }
        } while (true);
        
        

        MidiFileManager midiFileManager = new MidiFileManager();
        string filePath;

#if DEBUG
        filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Songs\Fur_Elise_Easy_Piano.mid");
#else
            filePath = @"Songs\Fur_Elise_Easy_Piano.mid";
#endif

        midiFileManager.MidiFile = MidiFile.Read(filePath);
        midiDeviceManager.EventHandler.MidiChecker.Song = midiFileManager.Song;
        midiDeviceManager.addHandlers();
        do
        {
            ConsoleManager.MaximizeConsoleWindow();
            midiFileManager.ReadFile();
            Console.Clear();
            Console.WriteLine(midiFileManager.Song.ToString());

            midiDeviceManager.EventHandler.MidiChecker.Song = midiFileManager.Song;

            // Keep the program running until the user presses a key
            restart = WaitForKeyPress();

            
        } while (restart);
        // Stop listening for events
        midiDeviceManager.InputDevice.StopEventsListening();
    }

    private static bool WaitForKeyPress()
    {
        Console.WriteLine("Press 'R' to restart or any other key to exit");
        
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        return keyInfo.Key == ConsoleKey.R;
    }
}
