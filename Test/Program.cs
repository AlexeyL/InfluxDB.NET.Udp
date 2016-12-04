using System;
using System.Collections.Generic;
using System.Threading;
using InfluxDB.NET.Udp;
using InfluxDB.NET.Udp.Models;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init InfluxDbUdpClient
            InfluxDbUdpClient client = new InfluxDbUdpClient("127.0.0.1", 8089);

            //WriteOnePoint(client);
            WriteBunchOfPoint(client);

        }

        private static void WriteOnePoint(InfluxDbUdpClient client)
        {
            //Init one point with two tags and two fields
            Point point = new Point() { Measurement = "Measure" };
            Dictionary<string, object> tags = new Dictionary<string, object>();
            Dictionary<string, object> fields = new Dictionary<string, object>();

            tags.Add("cpu", "intel");

            fields.Add("value", 100);
            fields.Add("value2", 75);

            point.Tags = tags;
            point.Fields = fields;
            point.Timestamp = DateTime.Now;

            //Write point
            try
            {
                client.WritePoint(point);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        private static void WriteBunchOfPoint(InfluxDbUdpClient client)
        {
            string measurement = "Measure";
            
            //Init one point with two tags and two fields
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                Point point = new Point() {Measurement = measurement};
                Dictionary<string, object> tags = new Dictionary<string, object>();
                Dictionary<string, object> fields;

                tags.Add("cpu", "intel");

                for (int j = i; j < i + 5; j++)
                {
                    fields = new Dictionary<string, object>();

                    fields.Add("value", j);
                    fields.Add("value2", j + 100);

                    point.Tags = tags;
                    point.Fields = fields;
                    point.Timestamp = DateTime.Now;

                    points.Add(point);
                    
                    Thread.Sleep(1000);
                }
            }

            //Write point
            try
            {
                client.WritePoint(points);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }
    }
}
