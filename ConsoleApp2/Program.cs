using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace air
{
    class Program
    {
        static List<Airport> AirportsList = new List<Airport>();

        private static void Main(string[] args)
        {
            try
            {
                ReadAllDataFromFile();
                CallMenu();

            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine(ex.Message);
            }

            while (true)
            {
                string input = Console.ReadLine();

                // Execute the command
                ExecuteCommand(input);
            }

        }


        static void ReadAllDataFromFile()
        {
            try
            {
                //Create object of FileInfo for specified path            
                FileInfo fi = new FileInfo(@"..\..\..\data.txt");

                //Open file for Read\Write
                FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

                //Create object of StreamReader by passing FileStream object on which it needs to operates on
                StreamReader sr = new StreamReader(fs);


                while (!sr.EndOfStream)
                {
                    try
                    {
                        string thisLine = sr.ReadLine();
                        string[] info = thisLine.Split(new string[] { ";" }, StringSplitOptions.None);
                        AddAirportData(info[0], double.Parse(info[2]), double.Parse(info[3]), int.Parse(info[4]), info[1]);
                    }catch (InvalidDataException ex) {
                    Console.WriteLine("Error in reading file " + ex.ToString());
                    }
                }

                //Close StreamReader object after operation
                sr.Close();
                fs.Close();

            }
            catch (IOException ex)
            {
                Console.WriteLine("Error in reading file" + ex.ToString());
            }


        }

        private static void AddAirportData(string code, double lat, double lng, int elevM, string city)
        {
            Airport newPort = new Airport(code, lat, lng, elevM, city);
            AirportsList.Add(newPort);
        }

        private static void CallMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Add Airport");
            Console.WriteLine("2. List all airports");
            Console.WriteLine("3. Find nearest airport by code");
            Console.WriteLine("4. Find airport's elevation standard deviation");
            Console.WriteLine("5. Change log delegates");
            Console.WriteLine("0. Exit");
            Console.WriteLine("Enter your choice:");

        }

        private static void ListAllAirportsInfo()
        {
            //foreach (var airport in AirportsList)
            //{
            //    Console.WriteLine(airport.ToString());
            //}

            Console.WriteLine(String.Join("\n", AirportsList));
        }

        private static void FindAirportByCode(string search)
        {
            search = search.ToUpper();
            var matchingAirports = AirportsList.Where(airport => airport.Code.Equals(search));

            foreach (var airport in matchingAirports)
            {
                Console.WriteLine(airport.Code);
            }
        }


        static void ExecuteCommand(string command)
        {
            switch (command)
            {
                case "0":
                    Save();
                    Environment.Exit(0);
                    break;
                case "1":
                    //Console.WriteLine("Enter code");
                    //string code = Console.ReadLine();
                    //Console.WriteLine("Enter latitude");
                    //double lat = double.Parse(Console.ReadLine());
                    //Console.WriteLine("Enter longitude");
                    //double lng = double.Parse(Console.ReadLine());
                    //Console.WriteLine("Enter elevation meters");
                    //int elevM = int.Parse(Console.ReadLine());
                    //Console.WriteLine("Enter city");
                    //string city = Console.ReadLine();

                    //AddAirportData(code,lat,lng,elevM, city);
                    AddAirport();
                    CallMenu();
                    break;
                case "2":
                    ListAllAirportsInfo();
                    CallMenu();
                    break;
                case "3":
                    //Console.WriteLine("Enter code to search");
                    //string input = Console.ReadLine();
                    //FindAirportByCode(input);
                    FindNearestAirport();
                    CallMenu();
                    break;
                case "4":
                  //  Console.WriteLine("Enter maximum age");
                   // int input2 = int.Parse(Console.ReadLine());
                   // FindPersonYoungerThan(input2);
                    CallMenu();
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    CallMenu();
                    break;
            }
        }

        private static void FindNearestAirport()
        {
            Console.WriteLine("Enter the code of Airport");
            string airlineCode = Console.ReadLine();
            var airport = AirportsList.Find(ap => ap.Code.Equals(airlineCode));
            GeoCoordinate coordinate = new GeoCoordinate(airport.Latitude, airport.Longitude);

            Airport nearestAirport = (from ap in AirportsList
                                      let geo = new GeoCoordinate { Latitude = ap.Latitude, Longitude = ap.Longitude }
                                      orderby geo.GetDistanceTo(coordinate)
                                      select ap).Take(2).ToList<Airport>()[1];

            double dist = GetDistance(coordinate.Latitude, nearestAirport.Latitude, coordinate.Longitude, nearestAirport.Longitude);

            Console.WriteLine("Found nearest airport to be {0}/{1} distance is {2}", nearestAirport.Code,
                nearestAirport.City, dist);
        }

        private static double GetDistance(double latitude1, double latitude2, double longitude1, double longitude2)
        {
            longitude1 = toRadians(longitude1);
            latitude1 = toRadians(latitude1);
            longitude2 = toRadians(longitude2);
            latitude2 = toRadians(latitude2);


            // Haversine formula  
            double dlon = longitude2 - longitude1;
            double dlat = latitude2 - latitude1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(latitude1) * Math.Cos(latitude2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in  
            // kilometers. Use 3956  
            // for miles 
            double r = 6371;

            // calculate the result     
            return (c * r);

        }

        private static double toRadians(double angleIn10thofaDegree)
        {
            return (angleIn10thofaDegree * Math.PI) / 180;

        }

        private static void AddAirport()
        {
            Console.WriteLine("Adding a airport.");
            Console.Write("Enter Code: ");
            string code = Console.ReadLine();
            Console.Write("Enter City: ");
            string city = Console.ReadLine();
            Console.Write("Enter Latitude: ");
            string latStr = Console.ReadLine();
            double lat;
            if (!double.TryParse(latStr, out lat))
            {
                Airport.LogFailSet?.Invoke("Eror: Latitude must be a double number");
                Console.WriteLine("Eror: Latitude must be a double number");
                return;
            }
            Console.Write("Enter Longitude: ");
            string longitudeStr = Console.ReadLine();
            double longitude;
            if (!double.TryParse(longitudeStr, out longitude))
            {
                Airport.LogFailSet?.Invoke("Error: longitude must be a valid Double.");
                Console.WriteLine("Error: longitude must be a valid Double.");
                return;
            }
            Console.Write("Enter elevation in meters ");
            string elevationStr = Console.ReadLine();
            int elevation;
            if (!int.TryParse(elevationStr, out elevation))
            {
                Airport.LogFailSet?.Invoke("Error: elevation must be a valid integer.");
                Console.WriteLine("Error: elevation must be a valid integer.");
                return;
            }

            try
            {
                Airport airport = new Airport(code, lat, longitude, elevation, city);
                AirportsList.Add(airport);

            }
            catch (InvalidDataException exc)
            {
                Airport.LogFailSet?.Invoke("Error : adding airport");
                Console.WriteLine("Erro" + exc.Message);
            }
        }

        static void Save()
        {
            using (StreamWriter writer = new StreamWriter(@"..\..\..\data.txt"))
            {
                foreach (var airport in AirportsList)
                {
                    writer.WriteLine($"{airport.Code};{airport.City};{airport.Latitude};{airport.Longitude};{airport.ElevM}");
                }
            }
        }
    }
}