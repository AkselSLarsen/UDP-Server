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
    public class UdpRedirector
    {
        private static IPAddress ip = IPAddress.Any;
        private static int port = 7161;

        public static void UdpRecieve() 
        {
            while (true) 
            {
                Task.Run(() =>
                {
                    string message = ReadFromPort();

                    if (message.Contains(": "))
                    {
                        DirectionChecker.CarRegistration(message);
                    }
                    else if (message.Contains("q ")) 
                    {
                        ParkingManager.SpotRegistration(message);
                    }
                });
                Thread.Sleep(1);
            }
        }

        public static string ReadFromPort()
        {
            UdpClient socket = new UdpClient();
            string dataString = null;

            while(dataString == null) {
                try {
                    socket.Client.Bind(new IPEndPoint(ip, port));

                    IPEndPoint from = null;
                    byte[] data = socket.Receive(ref from);
                    dataString = Encoding.UTF8.GetString(data);

                } catch (Exception e) {
                    //Console.Error.WriteLine(e);
                }
            }
            return dataString;
        }
    }
}
