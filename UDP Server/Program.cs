using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server {
    public class Program {

        public static void Main(string[] args) {
            while (true) {
                string data = DirectionChecker.CarRegistration();

                if (data != null) {
                    SendData(data);
                }
            }
        }
        //public static void Main(string[] args)
        //{
        //    while (true)
        //    {
        //        string data = null;

        //        SendData(data);
        //        Thread.Sleep(60000);
        //    }
        //}

        // Test af DirectionChecker
        //public static void Main(string[] args)
        //{
        //    string expected = $"1: {DateTime.Now} Lorem Ipsum Dolor Sit Amet";
        //    DateTime priorTime = DateTime.Parse(expected.Remove(0, 3).Remove(19));

        //    Console.WriteLine(priorTime);
        //}

        private static void SendData(string input) {

            MotionSensorData data = null;

            if (input == null) { //For testing purposes
                data = new MotionSensorData(DateTime.Now, RandomDirection(), RandomDownfall(), RandomTemperature(), RandomWindspeed());
            } else {
                data = DataIntepreter.ProcessData(input);
            }

            Console.WriteLine(data);

            bool success = DatabaseAccess.UploadDataToDatabase(data, new MotionSensorTableInfo());
            if (success) {
                Console.WriteLine("Successfully uploaded data from sensor");
            } else {
                Console.WriteLine("Failed to upload data from sensor");
            }
        }

        public static int RandomWindspeed() {
            Random r = new Random();
            return r.Next(0, 12);
        }

        public static int RandomTemperature() {
            Random r = new Random();
            return r.Next(-25, 75);
        }

        public static int RandomDownfall() {
            Random r = new Random();
            return r.Next(0, 800);
        }

        private static bool RandomDirection() {
            Random r = new Random();
            if(r.Next() % 2 == 0) {
                return true;
            } else {
                return false;
            }
        }
    }
}
