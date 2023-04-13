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
        private bool _isCompleted;

        public Measure()
        {
            HandedNotes = new List<HandedNote>();
            IsCompleted = false;
        }

        public List<HandedNote> HandedNotes { get => _handedNotes; set => _handedNotes = value; }
        public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }

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
            List<string> noteWidthStrings = new List<string>();

            // Create note strings and note width strings
            foreach (HandedNote note in HandedNotes)
            {
                string noteString = note.ToString();
                string noteWidthString = note.JianpuNote;
                noteStrings.Add(noteString);
                noteWidthStrings.Add(noteWidthString);
            }

            // Calculate total note width strings length
            int totalNoteWidthStringsLength = noteWidthStrings.Sum(x => x.Length);

            // Calculate total padding length required to achieve fixed total width
            int totalPaddingLength = (int)(totalWidth - totalNoteWidthStringsLength);

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
