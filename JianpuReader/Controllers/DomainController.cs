using JianpuReader.Midi;
using JianpuReader.MusicElements;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using Pastel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Controller
{
    internal class DomainController
    {
        private InputDevice _inputDevice;
        private List<InputDevice> _devices;
        public static Song song;

        public List<string> getDeviceList()
        {
            StringBuilder sb = new StringBuilder();
            _devices = MidiDeviceManager.getAvailableDevices().ToList();
            return _devices.Select(device => device.Name).ToList();
        }

        public void pickDevice(int device)
        {
            _inputDevice = _devices[device - 1];
        }

        public void selectFile(string filePath)
        {
            song = MidiFileManager.ReadFile(filePath);
        }

        public void addHandlers()
        {
            if (_inputDevice == null)
            {
                throw new NullReferenceException("Please set the input device before adding handlers");
            }
            _inputDevice.EventReceived += EventHandler.OnEventReceived;
            _inputDevice.StartEventsListening();
        }
    }
}
