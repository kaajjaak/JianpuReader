using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

namespace MyProject.MusicTheory
{
    public class HandedNote
    {
        public string JianpuNote { get; set; }
        public bool IsRightHand { get; set; }
        public long NoteLength { get; set; }

        public HandedNote(string jianpuNote, bool isRightHand, long noteLength)
        {
            JianpuNote = jianpuNote;
            IsRightHand = isRightHand;
            NoteLength = noteLength;
        }

        public override string ToString()
        {
            return $"{JianpuNote} ({(IsRightHand ? "Right" : "Left")})";
        }

    }
}
