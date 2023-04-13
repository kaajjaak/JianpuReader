using System;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using JianpuReader.Midi;
using Pastel;
using JianpuReader.Controller;
using JianpuReader.Application;

internal class Program
{
    private static void Main(string[] args)
    {
        new CUI(new DomainController()).start();
    }

    
}
