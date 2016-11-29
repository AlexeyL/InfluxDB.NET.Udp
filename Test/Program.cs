using System;
using System.Collections.Generic;
using InfluxDB.NET.Udp;
using InfluxDB.NET.Udp.Models;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Init InfluxDbUdpClient
            InfluxDbUdpClient client = new InfluxDbUdpClient("172.0.0.1", 8083);

            WriteOnePoint(client);
            WriteBunchOfPoint(client);

        }

        private static void WriteOnePoint(InfluxDbUdpClient client)
        {
            //Init one point with two tags and two fields
            Point point = new Point() { Measurement = "TestMeasure" };
            Dictionary<string, object> tags = new Dictionary<string, object>();
            Dictionary<string, object> fields = new Dictionary<string, object>();

            tags.Add("Tag1", "TagValue1");
            tags.Add("Tag2", 1);

            fields.Add("Field1", "FieldValue1");
            fields.Add("Field2", 100);

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
            string measurement = "TestMeasure";
            
            //Init one point with two tags and two fields
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                Point point = new Point() {Measurement = measurement};
                Dictionary<string, object> tags = new Dictionary<string, object>();
                Dictionary<string, object> fields = new Dictionary<string, object>();

                for (int j = i; j < i + 5; j++)
                {
                    tags.Add("Tag1", "TagValue" + j);
                    tags.Add("Tag2", j);

                    fields.Add("Field1", "Field" + j);
                    fields.Add("Field2", j);
                }

                points.Add(point);
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
