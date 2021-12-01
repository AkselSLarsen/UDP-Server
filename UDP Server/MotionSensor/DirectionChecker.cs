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
        private static string priorSensorRegistration = null;

        #region business values

        private static TimeSpan registrationConnectionLength = new TimeSpan(0, 3, 0);
        #endregion business values

        /// <summary>
        /// Denne metode returnerer tid og bilens retning, repræsenteret i en string.
        /// Vi forventer hvis der står 1 i vores string, er det yderste målepunkt - hvorimod står der 2 i vores string, forventer vi det er inderste målepunkt.
        /// Hvis vores string (med tidligste DateTime) starter med 1, fortæller det os, at bilen kører ind - og vice versa med 2 (udkørsel).
        /// 
        /// "1: " Forventer vi kommer før DateTime på det yderste målepunkt.
        /// "2: " Forventer vi kommer før DateTime på det inderste målepunkt.
        /// </summary>
        /// <returns></returns>
        public static void CarRegistration(string currentSensorRegistration)
        {

#warning delete line below
Console.WriteLine("MotionSensor:" + currentSensorRegistration);

            bool DrivingIn = false;

            DateTime priorTime = DateTime.MinValue;
            DateTime currentTime = DateTime.MinValue;
            if (priorSensorRegistration != null) {
                priorTime = DateTime.Parse(priorSensorRegistration.Remove(0, 3).Remove(19));
                currentTime = DateTime.Parse(currentSensorRegistration.Remove(0, 3).Remove(19));
            }

            if (priorSensorRegistration == null || !RelevantRegistration(priorSensorRegistration, currentSensorRegistration, priorTime, currentTime))
            {
                priorSensorRegistration = currentSensorRegistration;
                return;
            }

            if(currentTime - priorTime > TimeSpan.Zero) {
                if(currentSensorRegistration.StartsWith('1') && priorSensorRegistration.StartsWith('2')) {
                    DrivingIn = false;
                } else if(currentSensorRegistration.StartsWith('2') && priorSensorRegistration.StartsWith('1')) {
                    DrivingIn = true;
                } else {
#warning please make proper exception.
                    throw new Exception("Something went wrong.");
                }

                string message = "";
                if(DrivingIn) {
                    message += "In;";
                } else {
                    message += "Out;";
                }

                message += currentTime;

                SendMotionData(message);
            }
        }

        /// <summary>
        /// A method designed to determine that the given registrations are relevant and make sense with each others context.
        /// </summary>
        /// <param name="prior"></param>
        /// <param name="current"></param>
        /// <param name="priorTime"></param>
        /// <param name="currentTime"></param>
        /// <returns></returns>
        private static bool RelevantRegistration(string prior, string current, DateTime priorTime, DateTime currentTime)
        {
            //If the sensors are different.
            bool priorIs1 = prior.StartsWith('1');
            bool currentIs2 = current.StartsWith('2');
            if (priorIs1 == currentIs2) {
                if(currentTime - priorTime < registrationConnectionLength)
                {
                    return true;
                }
            }

            return false;
        }

        private static void SendMotionData(string input) {

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
