using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using JianpuReader.Midi;
using Pastel;

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleManager.MaximizeConsoleWindow();


        MidiDeviceManager midiDeviceManager = new MidiDeviceManager();
        try
        {
            midiDeviceManager.createInputDevice();
        } catch (Exception ex)
        {
            Console.WriteLine(ex.Message.Pastel(ConsoleColor.Red));
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            return;
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

        Console.WriteLine(midiFileManager.Song.ToString());

        MidiEventHandler midiEventHandler = new MidiEventHandler(new MidiChecker(midiFileManager.Song));

        midiDeviceManager.InputDevice.EventReceived += midiEventHandler.OnEventReceived;
        midiDeviceManager.InputDevice.StartEventsListening();


        

        

        // Keep the program running until the user presses a key
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
        // Stop listening for events
        midiDeviceManager.InputDevice.StopEventsListening();

    }
}