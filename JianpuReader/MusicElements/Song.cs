﻿using JianpuReader.NoteConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.MusicElements
{
    internal class Song
    {
        private List<Measure> rightMeasures = new List<Measure>();
        private List<Measure> leftMeasures = new List<Measure>();

        public Song(List<Measure> rightMeasures, List<Measure> leftMeasures)
        {
            this.rightMeasures = rightMeasures;
            this.leftMeasures = leftMeasures;
        }

        public List<Measure> RightMeasures { get => rightMeasures; set => rightMeasures = value; }
        public List<Measure> LeftMeasures { get => leftMeasures; set => leftMeasures = value; }

        public override string? ToString()
        {
            // Create a single StringBuilder
            StringBuilder measuresOutput = new StringBuilder();

            // Save right hand measures in a single line
            measuresOutput.AppendLine("Right Hand Measures:");
            foreach (Measure rightHandMeasure in rightMeasures)
            {
                measuresOutput.Append(rightHandMeasure);
            }
            measuresOutput.AppendLine();

            // Save left hand measures in a single line
            measuresOutput.AppendLine("Left Hand Measures:");
            foreach (Measure leftHandMeasure in leftMeasures)
            {
                measuresOutput.Append(leftHandMeasure);
            }
            measuresOutput.AppendLine();
            ConsoleManager.SetConsoleWidth(measuresOutput.ToString().Length + 10);
            // Output the result
            return measuresOutput.ToString();

        }
    }
}
