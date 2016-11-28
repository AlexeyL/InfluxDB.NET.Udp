using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using InfluxDB.NET.Udp.Models;

namespace InfluxDB.NET.Udp
{
    public class InfluxDbUdpClient
    {
        private string ConnectionIpString { get; set; }
        private int ConnectionPort { get; set; }

        public InfluxDbUdpClient(string connectionIpString, int connectionPort)
        {
            if (string.IsNullOrEmpty(connectionIpString) || string.IsNullOrWhiteSpace(connectionIpString))
                throw new ArgumentException("ConnectionIPString could not by empty.");

            this.ConnectionIpString = connectionIpString;
            this.ConnectionPort = connectionPort;
        }

        public void WritePoint(Point point)
        {
            this.WritePoint(new[] { point });
        }

        public void WritePoint(IEnumerable<Point> points)
        {
            string data = String.Join("\n", points.Select(p => p.ToString()));

            WriteData(data);
        }

        private void WriteData(string data)
        {
            IPAddress ipAddress = IPAddress.Parse(this.ConnectionIpString);
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, this.ConnectionPort);

            using (UdpClient client = new UdpClient())
            {
                byte[] sendBytes = Encoding.UTF8.GetBytes(data);
                client.Send(sendBytes, sendBytes.Length, ipEndPoint);
            }
        }

    }
}
