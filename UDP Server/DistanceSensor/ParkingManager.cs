using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server
{
    public class ParkingManager
    {
        private static bool[][] spotsTaken = new bool[][] {
            new bool[2],
            new bool[2],
            new bool[2]
        };
        private static int lastCalculation = int.MinValue;
        private static DateTime lastUpdated = DateTime.MinValue;

        /// <summary>
        /// En metode som tager imod en streng fra en afstandssensor og bruger den til
        /// at udregne hvor mange pladser der er optagede.
        /// Dette gøres ved at holde en liste over alle parkeringspladser og så
        /// opdatere den givne parkeringsplads i listen når dens tilhørende sensor
        /// sender information til den.
        /// 
        /// Denne metode forventer at strengen fra sensoren starter med dens id
        /// efterfuldt af "; " så dens type id, igen efterfuldt af "; " og derefter
        /// tidspunktet af målingen med et '*' på enden og så afsluttet med sand eller
        /// falsk alt efter om der er, eller ikke er et objekt i måleområdet.
        /// Eksempel 1: "1q 0q 2021-12-01 10:55:17*true"
        /// Eksempel 2: "3q 2q 2021-12-03 10:55:52*false"
        /// </summary>
        /// <param name="message"></param>
        public static void SpotRegistration(string message) 
        {
#warning delete line below
Console.WriteLine("Distance Sensor:" + message);

            int id = int.Parse(message.Split("q ")[0]);
            int type = int.Parse(message.Split("q ")[1]);
            bool occuied = false;

            if(message.Split("*")[1].ToLowerInvariant().Contains("t")) {
                occuied = true;

                Occupy(type, id);
            } else if(message.Split("*")[1].ToLowerInvariant().Contains("f")) {
                //occuied = false; // not necessary

                Release(type, id);
            } else {
#warning Lav rigtig exception
                throw new Exception("Somthing went wrong!!!");
            }

            DateTime now = DateTime.Now;
            if (now > lastUpdated.AddMinutes(1)) {

                int count = CalculateAmountOfTakenSpots();
                if(count != lastCalculation) {
                    lastCalculation = count;
                    lastUpdated = now;

                    DatabaseAccess.RunCustomSQLScriptOnTable(
                        SQLDistanceSensorUpdate(type, CalculateAmountOfTakenSpots(type)),
                        new DistanceSensorTableInfo()
                    );
                }
            }
        }

        private static void Occupy(int type, int id) {
            if(spotsTaken[type].Length > id) {
                spotsTaken[type][id] = true;
            } else {
                IncreaseParkingSpace(type);
                Occupy(type, id);
            }
        }
        private static void Release(int type, int id) {
            if(spotsTaken.Length > id) {
                spotsTaken[type][id] = false;
            } else {
                IncreaseParkingSpace(type);
                Release(type, id);
            }
        }

        private static void IncreaseParkingSpace(int type) {
            bool[] tmp = new bool[spotsTaken[type].Length * 2];
            for (int i = 0; i < spotsTaken[type].Length; i++) {
                tmp[i] = spotsTaken[type][i];
            }
            spotsTaken[type] = tmp;
        }

        private static int CalculateAmountOfTakenSpots() {
            int count = 0;
            for(int type=0; type<spotsTaken.Length; type++) {
                for(int id=0; id<spotsTaken[type].Length; id++) {
                    if(spotsTaken[type][id]) {
                        count++;
                    }
                }
            }
            return count;
        }

        private static int CalculateAmountOfTakenSpots(int type) {
            int count = 0;
            for (int id = 0; id < spotsTaken[type].Length; id++) {
                if (spotsTaken[type][id]) {
                    count++;
                }
            }
            return count;
        }

        private static string SQLDistanceSensorUpdate(int type, int optagedePladser) {
            return $"UPDATE SpecielleParkeringsPladser SET OptagedePladser = {optagedePladser} WHERE OmrådeID = {Program.OmrådeID} AND ParkeringsType = {type};";
        }
    }
}
