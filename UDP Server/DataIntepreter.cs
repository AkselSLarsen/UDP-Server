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

            return new MotionSensorData(Time, DrivingIn, Downfall, Temperature, Windspeed);
        }

        public static int GetWindspeed() {
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) + "/" + GetDateForURL(DateTime.Now) + "&" + "parameterId=wind_speed";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.', ','));

            return Convert.ToInt32(re);
        }

        public static int GetTemperature() {
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) +"/"+ GetDateForURL(DateTime.Now) +"&" +"parameterId=temp_mean_past1h";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.',','));

            return Convert.ToInt32(re);
        }

        public static int GetDownfall() {
            string json = "";
            using (WebClient wc = new WebClient()) {
                string url = "https://dmigw.govcloud.dk/v2/metObs/collections/observation/items?api-key=9c03456a-00ce-48db-a13b-907255c2eb73&stationId=06184&datetime=" + GetDateForURL(DateTime.Now.AddDays(-1)) + "/" + GetDateForURL(DateTime.Now) + "&" + "parameterId=precip_past1h";
                json = wc.DownloadString(url);
            }
            string[] values = json.Split("\"value\":");
            string value = values[1];
            double re = double.Parse(value.Split("}")[0].Replace('.', ','));

            return Convert.ToInt32(re);
        }

        public static string GetDateForURL(DateTime dateTime) {
            return $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}T{dateTime.ToString("HH:mm:ss")}Z";
        }
    }
}