using JianpuReader.MusicElements;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using MyProject.MusicTheory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.NoteConversion
{
    internal static class Util
    {
        public static string ConvertPianoKeyNumberToLetter(int keyNumber)
        {
            string[] noteNames = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
            int noteIndex = keyNumber % 12;
            int octave = keyNumber / 12 - 1;
            return noteNames[noteIndex] + octave.ToString();
        }

        public static string ConvertToRelativeNoteNumberLeftHand(int keyNumber, bool MinusInFront = false)
        {
            int middleC = 60; // middle C is note number 60
            int noteValue = keyNumber - middleC;
            int octave = noteValue / 12;
            int noteNumber = noteValue % 12;
            if (noteNumber < 0)
            {
                noteNumber += 12;
                octave -= 1;
            }
            string relativeNote = "";
            switch (noteNumber)
            {
                case 0:
                    relativeNote = "5";
                    break;
                case 1:
                    relativeNote = "4#";
                    break;
                case 2:
                    relativeNote = "4";
                    break;
                case 3:
                    relativeNote = "3#";
                    break;
                case 4:
                    relativeNote = "3";
                    break;
                case 5:
                    relativeNote = "2";
                    break;
                case 6:
                    relativeNote = "2#";
                    break;
                case 7:
                    relativeNote = "1";
                    break;
                case 8:
                    relativeNote = "1#";
                    break;
                case 9:
                    relativeNote = octave < -1 ? "2" : "1*";
                    break;
                case 10:
                    relativeNote = octave < -1 ? "2#" : "1*#";
                    break;
                case 11:
                    relativeNote = octave < -1 ? "1" : "1**d";
                    break;
            }
            if (octave > 0)
            {
                relativeNote += "+" + new string('+', octave - 1);
            }
            else if (octave < -1)
            {
                if (MinusInFront)
                    relativeNote = new string('-', -octave) + relativeNote;
                else
                    relativeNote += new string('-', -octave);
            }
            return relativeNote;
        }

        public static string ConvertToRelativeNoteNumberRightHand(int keyNumber, bool MinusInFront = false)
        {
            int middleC = 60; // middle C is note number 60
            int noteValue = keyNumber - middleC;
            int octave = noteValue / 12;
            int noteNumber = noteValue % 12;
            if (noteNumber < 0)
            {
                noteNumber += 12;
                octave -= 1;
            }
            string relativeNote = "";
            switch (noteNumber)
            {
                case 0:
                    relativeNote = "1";
                    break;
                case 1:
                    relativeNote = "1#";
                    break;
                case 2:
                    relativeNote = "2";
                    break;
                case 3:
                    relativeNote = "2#";
                    break;
                case 4:
                    relativeNote = "3";
                    break;
                case 5:
                    relativeNote = "4";
                    break;
                case 6:
                    relativeNote = "4#";
                    break;
                case 7:
                    relativeNote = "5";
                    break;
                case 8:
                    relativeNote = "5#";
                    break;
                case 9:
                    relativeNote = "6";
                    break;
                case 10:
                    relativeNote = "6#";
                    break;
                case 11:
                    relativeNote = "7";
                    break;
            }
            if (octave > 0)
            {
                relativeNote += "+" + new string('+', octave - 1);
            }
            else if (octave < 0)
            {
                if (MinusInFront)
                    relativeNote = new string('-', -octave) + relativeNote;
                else
                    relativeNote += new string('-', -octave);
            }
            return relativeNote;
        }

        public static string ConvertToRelativeNoteNumber(int keyNumber, bool MinusInFront = false)
        {
            if (isNoteRight(keyNumber))
            {
                return ConvertToRelativeNoteNumberRightHand(keyNumber, MinusInFront);
            }
            else
            {
                return ConvertToRelativeNoteNumberLeftHand(keyNumber, MinusInFront);
            }
        }
        public static Boolean isNoteRight(int noteNumber)
        {
            int threshold = 60; // Middle C
            return noteNumber >= threshold;
        }

        public static List<(Measure left, Measure right)> SeparateNotesIntoMeasures(IEnumerable<Note> notes, double measureLength)
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

                if (isNoteRight(note.NoteNumber))
                {
                    rightMeasure.AddHandedNote(new HandedNote(ConvertToRelativeNoteNumber(note.NoteNumber, true), true, note.Length));
                }
                else
                {
                    leftMeasure.AddHandedNote(new HandedNote(ConvertToRelativeNoteNumber(note.NoteNumber, true), false, note.Length));
                }
            }

            if (leftMeasure.HandedNotes.Count > 0 || rightMeasure.HandedNotes.Count > 0)
            {
                measures.Add((leftMeasure, rightMeasure));
            }

            return measures;
        }

        public static double calculateMeasureLength(TempoMap tempoMap)
        {
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
    }


}
