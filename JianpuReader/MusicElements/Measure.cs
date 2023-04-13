using MyProject.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.MusicElements
{
    public class Measure
    {
        private List<HandedNote> _handedNotes;

        

        public Measure()
        {
            HandedNotes = new List<HandedNote>();
        }

        public List<HandedNote> HandedNotes { get => _handedNotes; set => _handedNotes = value; }

        public void AddHandedNote(HandedNote handedNote)
        {
            HandedNotes.Add(handedNote);
        }
        public List<HandedNote> GetHandedNotes()
        {
            return HandedNotes;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("|");

            double totalWidth = 30;

            if (_handedNotes.Count == 0)
            {
                sb.Append(' ', (int)totalWidth);
                sb.Append("|");
                return sb.ToString();
            }

            List<string> noteStrings = new List<string>();

            // Create note strings
            foreach (HandedNote note in HandedNotes)
            {
                string noteString = note.JianpuNote;
                noteStrings.Add(noteString);
            }

            // Calculate total note strings length
            int totalNoteStringsLength = noteStrings.Sum(x => x.Length);

            // Calculate total padding length required to achieve fixed total width
            int totalPaddingLength = (int)(totalWidth - totalNoteStringsLength);

            // Calculate padding width for each note
            int paddingWidth = totalPaddingLength / _handedNotes.Count;
            int extraPadding = totalPaddingLength % _handedNotes.Count;

            // Append note strings and padding
            for (int i = 0; i < noteStrings.Count; i++)
            {
                sb.Append(noteStrings[i]);
                int currentPaddingWidth = paddingWidth + (i < extraPadding ? 1 : 0);
                sb.Append(' ', currentPaddingWidth);
            }

            sb.Append("|");

            return sb.ToString();
        }



    }
}
