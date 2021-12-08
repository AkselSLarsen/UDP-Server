using System;
using System.Net;

namespace UDP_Server {
    public class DataIntepreter {
        public static MotionSensorData ProcessData(string input) {

            DateTime Time = DateTime.MinValue;
            bool DrivingIn = false;
            int Temperature = GetTemperature();
            int Downfall = GetDownfall();
            int Windspeed = GetWindspeed();

            string[] inputs = input.Split(";");
            
            if (inputs[0] == "In") { DrivingIn = true; }
            
            Time = DateTime.Parse(inputs[1]);

            return new MotionSensorData(Program.OmrådeID, Time, DrivingIn, Downfall, Temperature, Windspeed);
        }

#warning needs work to work.
        public static int GetWindspeed() {
            return 0;
            /*
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) + "/" + GetDateForURL(DateTime.Now) + "&" + "parameterId=wind_speed";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.', ','));

            return Convert.ToInt32(re);
            */
        }

#warning needs work to work.
        public static int GetTemperature() {
            return 0;
            /*
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) +"/"+ GetDateForURL(DateTime.Now) +"&" +"parameterId=temp_mean_past1h";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.',','));

            return Convert.ToInt32(re);
            */
        }

#warning needs work to work.
        public static int GetDownfall() {
            return 0;
            /*
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) + "/" + GetDateForURL(DateTime.Now) + "&" + "parameterId=precip_past1h";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.', ','));

            return Convert.ToInt32(re);
            */
        }

        public static string GetDateForURL(DateTime dateTime) {
            return $"{dateTime.Year}-{GetMonth(dateTime)}-{GetDay(dateTime)}T{dateTime.ToString("HH:mm:ss")}Z";
        }

        public static string GetMonth(DateTime dateTime) {
            if(dateTime.Month.ToString().Length == 1) {
                return "0" + dateTime.Month;
            }
            return dateTime.Month.ToString();
        }

        public static string GetDay(DateTime dateTime) {
            if (dateTime.Day.ToString().Length == 1) {
                return "0" + dateTime.Day;
            }
            return dateTime.Day.ToString();
        }
    }
}