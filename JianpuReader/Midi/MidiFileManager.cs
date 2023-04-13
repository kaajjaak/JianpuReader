﻿using JianpuReader.MusicElements;
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
    internal class MidiFileManager
    {
        private MidiFile _midiFile;

        public MidiFile MidiFile { get => _midiFile; set => _midiFile = value; }

        public void ReadFile()
        {
            IEnumerable<Note> notes = _midiFile.GetNotes();
            double measureLength = calculateMeasureLength();
            List<(Measure left, Measure right)> measures = SeparateNotesIntoMeasures(notes, measureLength);

            foreach ((Measure left, Measure right) measure in measures)
            {
                Console.WriteLine("Measure:");
                Console.WriteLine(measure.right.ToString());
                Console.WriteLine(measure.left.ToString());
                Console.WriteLine();
            }
        }

        public double calculateMeasureLength() {
            var tempoMap = _midiFile.GetTempoMap();
            var timeSignature = tempoMap.GetTimeSignatureAtTime(new MetricTimeSpan(0));
            var tempo = tempoMap.GetTempoAtTime(new MetricTimeSpan(0));

            int numerator = timeSignature.Numerator;
            int denominator = timeSignature.Denominator;

            double beatsPerMinute = tempo.BeatsPerMinute;
            double beatsPerSecond = beatsPerMinute / 60;

            double measureLengthInSeconds = (numerator / (double)denominator) * 4 / beatsPerSecond;
            double measureLengthInMilliseconds = measureLengthInSeconds * 1000;

            return measureLengthInMilliseconds;
        }

        public List<(Measure left, Measure right)> SeparateNotesIntoMeasures(IEnumerable<Note> notes, double measureLength)
        {
            List<(Measure left, Measure right)> measures = new List<(Measure left, Measure right)>();
            Measure leftMeasure = new Measure();
            Measure rightMeasure = new Measure();
            double currentMeasureTime = 0;

            foreach (Note note in notes)
            {
                double noteTime = note.Time;
                while (noteTime >= currentMeasureTime + measureLength)
                {
                    measures.Add((leftMeasure, rightMeasure));
                    leftMeasure = new Measure();
                    rightMeasure = new Measure();
                    currentMeasureTime += measureLength;
                }

                if (Util.isNoteRight(note.NoteNumber))
                {
                    rightMeasure.AddHandedNote(new HandedNote(Util.ConvertToRelativeNoteNumber(note.NoteNumber), true, note.Length));
                }
                else
                {
                    leftMeasure.AddHandedNote(new HandedNote(Util.ConvertToRelativeNoteNumber(note.NoteNumber), false, note.Length));
                }
            }

            if (leftMeasure.HandedNotes.Count > 0 || rightMeasure.HandedNotes.Count > 0)
            {
                measures.Add((leftMeasure, rightMeasure));
            }

            return measures;
        }

    }
}
