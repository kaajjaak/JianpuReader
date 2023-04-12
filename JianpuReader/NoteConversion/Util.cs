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

        public static string ConvertToRelativeNoteNumber(int keyNumber, bool MinusInFront = false)
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
    }
}
