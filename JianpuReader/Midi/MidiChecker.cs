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
                Measure correspondingLeftMeasure = DomainController.song.LeftMeasures[DomainController.song.RightMeasures.FindIndex(x => x.Equals(measure))];                if (handedNote != null && measure != null)
                {
                    handedNote.isCompleted = true;
                    handedNote.isCorrect = Util.ConvertToRelativeNoteNumber(note.NoteNumber, true) == handedNote.JianpuNote;
                    if (measure.HandedNotes.Find(x => !x.isCompleted) == null)
                    {
                        measure.IsCompleted = true;
                        if (correspondingLeftMeasure != null)
                        {
                            foreach (HandedNote leftHandedNote in correspondingLeftMeasure.HandedNotes)
                            {
                                if (!leftHandedNote.isCompleted)
                                {
                                    leftHandedNote.isCorrect = false;
                                    leftHandedNote.isCompleted = true;
                                }
                            }
                            correspondingLeftMeasure.IsCompleted = true;
                        }
                    }
                }
                else if (measure != null)
                {
                    measure.IsCompleted = true;
                    if (correspondingLeftMeasure.HandedNotes.Find(x => !x.isCompleted) == null)
                    {
                        correspondingLeftMeasure.IsCompleted = true;
                    } else
                    {
                        foreach (HandedNote leftHandedNote in correspondingLeftMeasure.HandedNotes)
                        {
                            if (!leftHandedNote.isCompleted)
                            {
                                leftHandedNote.isCorrect = false;
                                leftHandedNote.isCompleted = true;
                            }
                        }
                        correspondingLeftMeasure.IsCompleted = true;
                    }
                }
            }
            else
            {
                Measure measure;
                HandedNote handedNote;
                measure = DomainController.song.LeftMeasures.Find(x => !x.IsCompleted);
                handedNote = measure.HandedNotes.Find(x => !x.isCompleted);
                Measure correspondingRightMeasure = DomainController.song.RightMeasures[DomainController.song.LeftMeasures.FindIndex(x => x.Equals(measure))];
                if (handedNote != null && measure != null)
                {
                    handedNote.isCompleted = true;
                    handedNote.isCorrect = Util.ConvertToRelativeNoteNumber(note.NoteNumber, false) == handedNote.JianpuNote;
                    Console.WriteLine(Util.ConvertToRelativeNoteNumber(note.NoteNumber, false));
                    if (measure.HandedNotes.Find(x => !x.isCompleted) == null && correspondingRightMeasure.HandedNotes.Find(x => !x.isCompleted) == null)
                    {
                        measure.IsCompleted = true;
                        correspondingRightMeasure.IsCompleted = true;
                    }
                }
                else if (measure != null)
                {
                    if (correspondingRightMeasure.HandedNotes.Find(x => !x.isCompleted) == null)
                    {
                        measure.IsCompleted = true;

                    }
                }
            }
        }

    }

}
