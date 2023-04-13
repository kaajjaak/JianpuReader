using Force.DeepCloner;
using JianpuReader.Midi;
using JianpuReader.MusicElements;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Controller
{
    internal class DomainController
    {
        private InputDevice? _inputDevice;
        private List<InputDevice>? _devices;
        public static Song? song;
        private Song _song;

        public void loadDevices()
        {
            _devices = MidiDeviceManager.getAvailableDevices().ToList();
        }
        public List<string> getDeviceList()
        {
            if (_devices == null)
            {
                throw new NullReferenceException("Please load devices first!");
            }
            return _devices.Select(device => device.Name).ToList();
        }

        public void pickDevice(int device)
        {
            if (_devices == null)
            {
                throw new NullReferenceException("Please load devices first!");
            }
            _inputDevice = _devices[device - 1];
            _inputDevice.EventReceived += OnEventReceived;
            _inputDevice.StartEventsListening();
        }

        public void addHandler(EventHandler<MidiEventReceivedEventArgs> eventHandler)
        {
            if (song == null)
            {
                throw new NullReferenceException("Please select a song first!");
            }
            if (_inputDevice == null)
            {
                throw new NullReferenceException("Please select a device first!");
            }
            _inputDevice.EventReceived += eventHandler;
        }

        public void selectFile(string filePath)
        {
            song = MidiFileManager.ReadFile(filePath);
            _song = song.DeepClone();
        }

        public void resetSong()
        {
            song = _song.DeepClone();
        }

        private void OnEventReceived(object? sender, MidiEventReceivedEventArgs e)
        {
            /*MidiDevice midiDevice = (MidiDevice)sender;*/
            MidiEvent midiEvent = e.Event;

            if (midiEvent.EventType == MidiEventType.NoteOn)
            {
                if (song != null && song.isCompleted)
                {
                    stopListening();
                    return;
                }
                NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;
                if (noteOnEvent.Velocity > 0)
                {
                    MidiChecker.NotePlayed(noteOnEvent);
                }
                /*else
                {
                    this.NoteOff(midiDevice, midiEvent);
                }*/
            }
            /*else if (midiEvent.EventType == MidiEventType.NoteOn)
            {
                this.NoteOff(midiDevice, midiEvent);
            }*/
        }

        public void stopListening()
        {
            if (_inputDevice == null)
            {
                throw new NullReferenceException("Please set an InputDevice first!");
            }
            _inputDevice.StopEventsListening();
        }
    }
}
