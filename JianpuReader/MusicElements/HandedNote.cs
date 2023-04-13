using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Pastel;

namespace MyProject.MusicTheory
{
    public class HandedNote
    {
        public string JianpuNote { get; set; }
        public bool IsRightHand { get; set; }
        public long NoteLength { get; set; }
        public bool isCompleted { get; set; }
        public bool isCorrect { get; set; }

        public HandedNote(string jianpuNote, bool isRightHand, long noteLength)
        {
            JianpuNote = jianpuNote;
            IsRightHand = isRightHand;
            NoteLength = noteLength;
            isCompleted = false;
            isCorrect = false;
        }

        public override string ToString()
        {
            return JianpuNote.Pastel(GetNoteColor());
        }

        private ConsoleColor GetNoteColor()
        {
            if (isCompleted)
            {
                return isCorrect ? ConsoleColor.Green : ConsoleColor.Red;
            }

            return ConsoleColor.White;
        }

    }
}
