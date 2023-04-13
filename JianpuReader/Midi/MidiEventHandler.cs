using JianpuReader.NoteConversion;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    internal class MidiEventHandler
    {
        private MidiChecker _midiChecker;

        public MidiEventHandler(MidiChecker? midiChecker)
        {
            _midiChecker = midiChecker;
        }

        public void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
        {
            MidiDevice midiDevice = (MidiDevice)sender;
            MidiEvent midiEvent = e.Event;

            if (midiEvent.EventType == MidiEventType.NoteOn)
            {
                NoteOnEvent noteOnEvent = (NoteOnEvent)midiEvent;
                if (noteOnEvent.Velocity > 0)
                {
                    NoteOn(midiDevice, noteOnEvent);
                }
                else
                {
                    /*this.NoteOff(midiDevice, midiEvent);*/
                }
            }
            /*else if (midiEvent.EventType == MidiEventType.NoteOn)
            {
                this.NoteOff(midiDevice, midiEvent);
            }*/
        }

        private void NoteOff(MidiDevice midiDevice, MidiEvent midiEvent)
        {
            // Console.WriteLine(midiEvent.ToString());
        }

        private void NoteOn(MidiDevice midiDevice, NoteOnEvent midiEvent)
        {
            /*Console.WriteLine(Util.ConvertToRelativeNoteNumber(midiEvent.NoteNumber, true));*/
            _midiChecker.NotePlayed(midiEvent);
        }
    }
}
