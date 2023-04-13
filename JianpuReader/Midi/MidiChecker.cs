using JianpuReader.Controller;
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
    public static class MidiChecker
    {
        public static void NotePlayed(NoteOnEvent note)
        {
            if (DomainController.song == null)
            {
                throw new NullReferenceException("Please set the DomainController song property before calling NotePlayed.");
            }
            if (Util.isNoteRight(note.NoteNumber))
            {
                Measure measure;
                HandedNote handedNote;
                measure = DomainController.song.RightMeasures.Find(x => !x.IsCompleted);
                handedNote = measure.HandedNotes.Find(x => !x.isCompleted);
                



                if (handedNote != null && measure != null)
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
        }

    }

}
