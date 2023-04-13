using MyProject.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.MusicElements
{
    internal class Measure
    {
        private List<HandedNote> handedNotes;

        

        public Measure()
        {
            HandedNotes = new List<HandedNote>();
        }

        public List<HandedNote> HandedNotes { get => handedNotes; set => handedNotes = value; }

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

            if (handedNotes.Count == 0)
            {
                return sb.Append("|").ToString();
            }

            double totalWidth = 30;
            double fixedNoteWidth = totalWidth / handedNotes.Count;

            foreach (HandedNote note in HandedNotes)
            {
                string noteString = note.JianpuNote;
                sb.Append(noteString);

                // Pad the space after the note to maintain a fixed width for each note
                int paddingLength = (int)Math.Round(fixedNoteWidth - noteString.Length);
                sb.Append(' ', paddingLength);
            }

            sb.Append("|");

            return sb.ToString();
        }

    }
}
