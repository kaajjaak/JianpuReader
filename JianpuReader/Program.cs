using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using JianpuReader.Midi;

internal class Program
{
    private static void Main(string[] args)
    {
        /* MidiDeviceManager midiDeviceManager = new MidiDeviceManager();

         midiDeviceManager.createInputDevice();

         MidiEventHandler midiEventHandler = new MidiEventHandler();

         midiDeviceManager.InputDevice.EventReceived += midiEventHandler.OnEventReceived;
         midiDeviceManager.InputDevice.StartEventsListening();

         // Keep the program running until the user presses a key
         Console.WriteLine("Press any key to exit");
         Console.ReadKey();

         // Stop listening for events
         midiDeviceManager.InputDevice.StopEventsListening();*/

        MidiFileManager midiFileManager = new MidiFileManager();
        string filePath;

        #if DEBUG
                filePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Songs\Fur_Elise_Easy_Piano.mid");
        #else
            filePath = @"Songs\Fur_Elise_Easy_Piano.mid";
        #endif

        midiFileManager.MidiFile = MidiFile.Read(filePath);
        midiFileManager.ReadFile();

    }
}