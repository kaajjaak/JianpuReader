using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    internal static class MidiDeviceManager
    {

        public static List<InputDevice> getAvailableDevices()
        {
            List<InputDevice> devices = InputDevice.GetAll().ToList();

            if (devices.Count() == 0)
            {
                throw new Exception("No input devices found");
            }

            return devices;
        }
    }
}
