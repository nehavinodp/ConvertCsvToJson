using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Xml;
using CsvHelper;
using Newtonsoft.Json;

namespace CsvToJsonConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Path to your CSV file
                string csvFilePath = "C:\\Path\\To\\Your\\Redirects.csv";

                // Path to output JSON file
                string jsonFilePath = "C:\\Path\\To\\Your\\OutputFile.json";

                // Read CSV file and convert it to JSON
                var data = ReadCsv(csvFilePath);
                string json = ConvertToJson(data);

                // Save JSON to a file
                File.WriteAllText(jsonFilePath, json);

                Console.WriteLine("CSV file successfully converted to JSON!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        // Method to read CSV using CsvHelper
        static Dictionary<string, object> ReadCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Read CSV and create a dictionary with the desired structure
                var result = new Dictionary<string, object>();
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    string source = csv.GetField("source").ToLower();
                    string destination = csv.GetField("destination").ToLower();
                    bool permanent = csv.GetField<bool>("permanent");

                    // Create the JSON structure
                    result[$"{source}"] = new
                    {
                        destination = destination,
                        permanent = permanent
                    };
                }

                return result;
            }
        }

        // Define a class to represent each route
        public class Route
        {
            public string source { get; set; }
            public string destination { get; set; }
            public bool permanent { get; set; }
        }

        // Method to read CSV using CsvHelper
        static List<Route> ReadCsvForNextConfig(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var routes = new List<Route>();
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var route = new Route
                    {
                        source = csv.GetField("source").ToLower(),
                        destination = csv.GetField("destination").ToLower(),
                        permanent = csv.GetField<bool>("permanent")
                    };

                    routes.Add(route);
                }

                return routes;
            }
        }
        // Method to convert the dictionary to JSON
        static string ConvertToJson(Dictionary<string, object> data)
        {
            return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
        }

        // Method to convert the list of routes to JSON
        static string ConvertToJsonForNextConfig(List<Route> routes)
        {
            // Convert the list to JSON using Newtonsoft.Json
            return JsonConvert.SerializeObject(routes, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
