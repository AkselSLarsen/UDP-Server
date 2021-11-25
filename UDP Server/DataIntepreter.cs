using System;

namespace UDP_Server {
    public class DataIntepreter {
        public static MotionSensorData ProcessData(string input) {

            DateTime Time = DateTime.MinValue;
            bool DrivingIn = false;
            int Temperature = 0;
            int Downfall = 0;
            int Windspeed = 0;

            string[] inputs = input.Split(";");
            
            if (inputs[0] == "In") { DrivingIn = true; }
            
            Time = DateTime.Parse(inputs[1]);

            Temperature = Program.RandomTemperature();
            Downfall = Program.RandomDownfall();
            Windspeed = Program.RandomWindspeed();

            return new MotionSensorData(Time, DrivingIn, Downfall, Temperature, Windspeed);
        }
    }
}