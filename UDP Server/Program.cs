using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server {
    public class Program {

        public static void Main(string[] args) {
            UdpRedirector.UdpRecieve();
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

        public static void SendMotionData(string input) {

            MotionSensorData data = DataIntepreter.ProcessData(input);

            Console.WriteLine(data);

            bool success = DatabaseAccess.InsertToDatabase(data, new MotionSensorTableInfo());
            if (success) {
                Console.WriteLine("Successfully uploaded data from sensor");
            } else {
                Console.WriteLine("Failed to upload data from sensor");
            }
        }
    }
}
