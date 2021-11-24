using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server {
    public class Program {

        private static IPAddress ip = IPAddress.Any;
        private static int port = 7161;

        public static void Main(string[] args) {
            while (true) {
                string data = null;

                //string data = ReadFromPort();
                //if (data != null) {
                    SendData(data);
                //}
                Thread.Sleep(60000);
            }
        }

        public static string ReadFromPort() {
            try {
                UdpClient socket = new UdpClient();
                socket.Client.Bind(new IPEndPoint(ip, port));

                IPEndPoint from = null;
                byte[] data = socket.Receive(ref from);
                string dataString = Encoding.UTF8.GetString(data);

                return dataString;
            } catch(Exception e) {
                //Console.Error.WriteLine(e);
            }
            return null;
        }

        private static void SendData(string input) {

            MotionSensorData data = null;

            if (input == null) {
                data = new MotionSensorData(DateTime.Now, RandomDirection(), RandomDownfall(), RandomTemperature(), RandomWindspeed());
            } else {
                data = DataIntepreter.ProcessData(input);
            }

            Console.WriteLine(data);

            bool success = DatabaseAccess.UploadDataToDatabaseAsync(data, new MotionSensorTableInfo());
            if (success) {
                Console.WriteLine("Successfully uploaded data from sensor");
            } else {
                Console.WriteLine("Failed to upload data from sensor");
            }
        }

        private static int RandomWindspeed() {
            Random r = new Random();
            return r.Next(0, 12);
        }

        private static int RandomTemperature() {
            Random r = new Random();
            return r.Next(-25, 75);
        }

        private static int RandomDownfall() {
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
