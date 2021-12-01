using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDP_Server {
    public class DistanceSensorData : IData {
        public int OmrådeID { get; set; }
        public int ParkeringsType { get; set; }
        public int Pladser { get; set; }
        public int OptagedePladser { get; set; }

        public object[] Data {
            get {
                return new object[] { OmrådeID, ParkeringsType, Pladser, OptagedePladser };
            }
        }

        public DistanceSensorData(int områdeID, int parkeringsType, int pladser, int optagedePladser) {
            this.OmrådeID = områdeID;
            this.ParkeringsType = parkeringsType;
            this.Pladser = pladser;
            this.OptagedePladser = optagedePladser;
        }
    }
}
