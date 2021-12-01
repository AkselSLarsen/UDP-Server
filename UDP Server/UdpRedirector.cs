using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
                    else if (message.Contains("; ")) 
                    {
                        ParkingManager.SpotRegistration(message);
                    }

                });
            }
        }

        public static string ReadFromPort()
        {
            try
            {
                UdpClient socket = new UdpClient();
                socket.Client.Bind(new IPEndPoint(ip, port));

                IPEndPoint from = null;
                byte[] data = socket.Receive(ref from);
                string dataString = Encoding.UTF8.GetString(data);

                return dataString;
            }
            catch (Exception e)
            {
                //Console.Error.WriteLine(e);
                return "";
            }
        }
    }
}
