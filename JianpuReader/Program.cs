using System;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

var inputDevices = InputDevice.GetAll();

if (inputDevices.Count() == 0)
{
    Console.WriteLine("No input devices found");
    return;
}

Console.WriteLine("Available input devices:");
for (int i = 0; i < inputDevices.Count(); i++)
{
    Console.WriteLine($"{i + 1}. {inputDevices.ElementAt(i).Name}");
}

Console.Write("Enter the number of the input device you want to use: ");
var inputDeviceNumberString = Console.ReadLine();
if (!int.TryParse(inputDeviceNumberString, out int inputDeviceNumber) || inputDeviceNumber < 1 || inputDeviceNumber > inputDevices.Count())
{
    Console.WriteLine($"Invalid input device number '{inputDeviceNumberString}'");
    return;
}

var inputDevice = inputDevices.ElementAt(inputDeviceNumber - 1);
Console.WriteLine($"Selected input device '{inputDevice.Name}'");

inputDevice.EventReceived += OnEventReceived;
inputDevice.StartEventsListening();

// Keep the program running until the user presses a key
Console.WriteLine("Press any key to exit");
Console.ReadKey();

// Stop listening for events
inputDevice.StopEventsListening();


void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
{
    MidiDevice midiDevice = (MidiDevice)sender;
    MidiEvent midiEvent = e.Event;

    if (midiEvent.EventType == MidiEventType.NoteOn)
    {
        NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;
        if (noteOnEvent.Velocity > 0)
        {
            noteOn(midiDevice, noteOnEvent);
        }
        else
        {
            noteOff(midiDevice, midiEvent);
        }
    } else if (midiEvent.EventType == MidiEventType.NoteOn)
    {
        noteOff(midiDevice, midiEvent);
    }
}

void noteOff(MidiDevice midiDevice, MidiEvent midiEvent)
{
    // Console.WriteLine(midiEvent.ToString());
}

void noteOn(MidiDevice midiDevice, NoteOnEvent midiEvent)
{
    Console.WriteLine(ConvertToRelativeNoteNumber(midiEvent.NoteNumber));
    /*Console.WriteLine(ConvertToRelativeNoteNumber(midiEvent));*/
}

static string ConvertPianoKeyNumberToLetter(int keyNumber)
{
    string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
    int noteIndex = keyNumber % 12;
    int octave = (keyNumber / 12) - 1;
    return noteNames[noteIndex] + octave.ToString();
}

static string ConvertToRelativeNoteNumber(int keyNumber)
{
    int middleC = 60; // middle C is note number 60
    int noteValue = keyNumber - middleC;
    int octave = noteValue / 12;
    int noteNumber = noteValue % 12;
    if (noteNumber < 0)
    {
        noteNumber += 12;
        octave -= 1;
    }
    string relativeNote = "";
    switch (noteNumber)
    {
        case 0:
            relativeNote = "1";
            break;
        case 1:
            relativeNote = "1#";
            break;
        case 2:
            relativeNote = "2";
            break;
        case 3:
            relativeNote = "2#";
            break;
        case 4:
            relativeNote = "3";
            break;
        case 5:
            relativeNote = "4";
            break;
        case 6:
            relativeNote = "4#";
            break;
        case 7:
            relativeNote = "5";
            break;
        case 8:
            relativeNote = "5#";
            break;
        case 9:
            relativeNote = "6";
            break;
        case 10:
            relativeNote = "6#";
            break;
        case 11:
            relativeNote = "7";
            break;
    }
    if (octave > 0)
    {
        relativeNote += "+" + new string('+', octave - 1);
    }
    else if (octave < 0)
    {
        relativeNote += new string('-', -octave);
    }
    return relativeNote;
}