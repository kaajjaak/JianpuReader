using JianpuReader.NoteConversion;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    internal class MidiFileManager
    {
        private MidiFile _midiFile;

        public MidiFile MidiFile { get => _midiFile; set => _midiFile = value; }

        

        public void ReadFile()
        {
            IEnumerable<Note> notes = _midiFile.GetNotes();
            foreach (Note note in notes)
            {
                Console.WriteLine(Util.DetermineHand(note.NoteNumber));
                Console.WriteLine(Util.ConvertToRelativeNoteNumber(note.NoteNumber));
            }
        }
    }
}
