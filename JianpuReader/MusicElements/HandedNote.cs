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
        public bool Completed { get; set; }
        public bool Correct { get; set; }

        public HandedNote(string jianpuNote, bool isRightHand, long noteLength)
        {
            JianpuNote = jianpuNote;
            IsRightHand = isRightHand;
            NoteLength = noteLength;
            Completed = false;
            Correct = false;
        }

        public override string ToString()
        {
            return JianpuNote.Pastel(GetNoteColor());
        }

        private ConsoleColor GetNoteColor()
        {
            if (Completed)
            {
                return Correct ? ConsoleColor.Green : ConsoleColor.Red;
            }

            return ConsoleColor.White;
        }

    }
}
