using JianpuReader.MusicElements;
using JianpuReader.NoteConversion;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MyProject.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    internal static class MidiFileManager
    {
        public static Song ReadFile(string filePath)
        {
            MidiFile midiFile = MidiFile.Read(filePath);
            IEnumerable<Note> notes = midiFile.GetNotes();
            double measureLength = Util.calculateMeasureLength(midiFile.GetTempoMap());
            List<(Measure left, Measure right)> measures = Util.SeparateNotesIntoMeasures(notes, measureLength);

            List<Measure> rightHandMeasures = new List<Measure>();
            List<Measure> leftHandMeasures = new List<Measure>();

            rightHandMeasures = measures.Select(x => x.right).ToList();
            leftHandMeasures = measures.Select(x => x.left).ToList();

            return new Song(rightHandMeasures, leftHandMeasures);
        }

        

    }
}
