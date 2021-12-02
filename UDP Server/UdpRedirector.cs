using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server
{
    /// <summary>
    /// Please don't touch, we have no idea how it was fixed, and thus no idea how to fix it...
    /// </summary>
    public class UdpRedirector
    {
        private static IPAddress ip = IPAddress.Any;
        private static int port = 7161;

        private static Queue<string> messages = new Queue<string>();
        private static int maxLength = 20;

        public static void UdpRecieve() {
            while (true) {

                Task task = new Task(() => {
                    string message = ReadFromPort();

                    if(message != null) {

                        if(!messages.Contains(message)) {
                            messages.Enqueue(message);

                            if(messages.Count > maxLength) {
                                messages.Dequeue();
                            }

                            if (message.Contains(": ")) {
                                DirectionChecker.CarRegistration(message);
                            } else if (message.Contains("q ")) {
                                ParkingManager.SpotRegistration(message);
                            }
                        }
                    }
                });

                task.Start();
                Thread.Sleep(1000);
            }
        }

        public static string ReadFromPort()
        {
            try {
                using (UdpClient socket = new UdpClient()) {
                    socket.Client.Bind(new IPEndPoint(ip, port));

                    IPEndPoint from = null;
                    byte[] data = socket.Receive(ref from);
                    string dataString = Encoding.UTF8.GetString(data);

                    return dataString;
                }
            } catch (Exception e) {
                //Console.Error.WriteLine(e);
                return null;
            }
        }
    }
}
