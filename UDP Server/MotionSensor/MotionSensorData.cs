using System;

namespace UDP_Server {
    public class MotionSensorData : IData {
        public DateTime DateTime { get; set; }
        public bool DrivingIntoTheParkingLot { get; set; }
        public int Downfall { get; set; }
        public int Tempreture { get; set; }
        public int Windspeed { get; set; }

        public object[] Data {
            get {
                return new object[] { DateTime, DrivingIntoTheParkingLot, Downfall, Tempreture, Windspeed };
            }
        }

        public MotionSensorData(DateTime time, bool drivingIn, int downfall, int tempreture, int windspeed) {
            this.DateTime = time;
            this.DrivingIntoTheParkingLot = drivingIn;
            this.Downfall = downfall;
            this.Tempreture = tempreture;
            this.Windspeed = windspeed;
        }
    }
}