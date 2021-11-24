using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server
{
    public class DirectionChecker
    {
        // Denne class skal anvendes til at checke, om bilerne kører ind eller ud af parkeringspladsen.
        
        private static IPAddress ip = IPAddress.Any;
        private static int port = 7161;

        /// <summary>
        /// Denne metode returnerer tid og bilens retning, repræsenteret i en string.
        /// Vi forventer hvis der står 1 i vores string, er det yderste målepunkt - hvorimod står der 2 i vores string, forventer vi det er inderste målepunkt.
        /// Hvis vores string (med tidligste DateTime) starter med 1, fortæller det os, at bilen kører ind - og vice versa med 2 (udkørsel).
        /// 
        /// "1: " Forventer vi kommer før DateTime på det yderste målepunkt.
        /// "2: " Forventer vi kommer før DateTime på det inderste målepunkt.
        /// </summary>
        /// <returns></returns>
        public static string CarRegistration()
        {
            string priorSensorRegistration = null;

            while (true)
            {
                string currentSensorRegistration = ReadFromPort();

                if (priorSensorRegistration == null || !RelevantRegistration(priorSensorRegistration, currentSensorRegistration))
                {
                    priorSensorRegistration = currentSensorRegistration;
                    continue;
                }

                DateTime priorTime = DateTime.Parse(priorSensorRegistration.Remove(0, 3).Remove(26));
            }
            return null;
        }

        private static bool RelevantRegistration(string prior, string current)
        {
            return false;
        }

        public static string ReadFromPort()
        {
            while (true)
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
                }
            }
        }
    }
}
