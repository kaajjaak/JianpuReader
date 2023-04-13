using System;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using JianpuReader.Midi;
using Pastel;

internal class Program
{
    private static void Main(string[] args)
    {
        bool restart;

        do
        {
            ConsoleManager.MaximizeConsoleWindow();

            MidiDeviceManager midiDeviceManager = new MidiDeviceManager();
            try
            {
                midiDeviceManager.createInputDevice();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.Pastel(ConsoleColor.Red));
                restart = WaitForKeyPress();
                continue;
            }


            MidiFileManager midiFileManager = new MidiFileManager();
            string filePath;

#if DEBUG
            filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Songs\Fur_Elise_Easy_Piano.mid");
#else
            filePath = @"Songs\Fur_Elise_Easy_Piano.mid";
#endif

            midiFileManager.MidiFile = MidiFile.Read(filePath);
            midiFileManager.ReadFile();
            Console.Clear();
            Console.WriteLine(midiFileManager.Song.ToString());

            MidiEventHandler midiEventHandler = new MidiEventHandler(new MidiChecker(midiFileManager.Song));

            midiDeviceManager.InputDevice.EventReceived += midiEventHandler.OnEventReceived;
            midiDeviceManager.InputDevice.StartEventsListening();

            // Keep the program running until the user presses a key
            restart = WaitForKeyPress();

            // Stop listening for events
            midiDeviceManager.InputDevice.StopEventsListening();
        } while (restart);
    }

    private static bool WaitForKeyPress()
    {
        Console.WriteLine("Press 'R' to restart or any other key to exit");

        ConsoleKeyInfo keyInfo = Console.ReadKey();
        return keyInfo.Key == ConsoleKey.R;
    }
}
