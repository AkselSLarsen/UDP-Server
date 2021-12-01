using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UDP_Server {
    public class Program {
        public static int OmrådeID = 0;

        public static void Main(string[] args) {
            UdpRedirector.UdpRecieve();
        }
    }
}
