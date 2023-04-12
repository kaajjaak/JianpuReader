using Melanchall.DryWetMidi.Multimedia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JianpuReader.Midi
{
    internal class MidiDeviceManager
    {
        private InputDevice _inputDevice;

        public InputDevice InputDevice { get => _inputDevice; set => _inputDevice = value; }

        public void createInputDevice()
        {
            var inputDevices = InputDevice.GetAll();

            if (inputDevices.Count() == 0)
            {
                Console.WriteLine("No input devices found");
                return;
            }

            Console.WriteLine("Available input devices:");
            for (int i = 0; i < inputDevices.Count(); i++)
            {
                Console.WriteLine($"{i + 1}. {inputDevices.ElementAt(i).Name}");
            }

            Console.Write("Enter the number of the input device you want to use: ");
            var inputDeviceNumberString = Console.ReadLine();
            if (!int.TryParse(inputDeviceNumberString, out int inputDeviceNumber) || inputDeviceNumber < 1 || inputDeviceNumber > inputDevices.Count())
            {
                Console.WriteLine($"Invalid input device number '{inputDeviceNumberString}'");
                return;
            }

            InputDevice = inputDevices.ElementAt(inputDeviceNumber - 1);
            Console.WriteLine($"Selected input device '{InputDevice.Name}'");
        }


    }
}
