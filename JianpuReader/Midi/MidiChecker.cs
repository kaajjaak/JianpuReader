using JianpuReader.MusicElements;
using JianpuReader.NoteConversion;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using MyProject.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    public class MidiChecker
    {
        private Song? _song;

        public Song? Song { get => _song; set => _song = value; }

        public void NotePlayed(NoteOnEvent note)
        {
            if (_song == null)
            {
                throw new NullReferenceException("Please set the Song property before calling NotePlayed.");
            }
            if (Util.isNoteRight(note.NoteNumber))
            {
                Measure measure = _song.RightMeasures.Find(x => !x.IsCompleted);
                HandedNote handedNote = measure.HandedNotes.Find(x => !x.isCompleted);

                if (handedNote != null)
                {
                    handedNote.isCompleted = true;
                    handedNote.isCorrect = Util.ConvertToRelativeNoteNumber(note.NoteNumber, true) == handedNote.JianpuNote;
                    if (measure.HandedNotes.Find(x => !x.isCompleted) == null)
                    {
                        measure.IsCompleted = true;
                    }
                }
                else
                {
                    measure.IsCompleted = true;
                }
            }

            // Move the cursor position to the beginning of the console
            Console.SetCursorPosition(0, 0);

            // Write the updated song representation to the console
            Console.WriteLine(_song.ToString());
            Console.WriteLine();
        }

    }

}
