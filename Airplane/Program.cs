using System;
using System.Collections.Generic;
using System.Device.Location;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airplane
{
    internal class Program
    {
        static List<Airport> airportList = new List<Airport>();
        const string DataFileName = @"..\..\data.txt";
        static void Main(string[] args)
        {
            ReadDataFromFile();
            Console.WriteLine("Data loaded");

            while (true)
            {
                int choice = GetMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddAirport();
                        break;
                    case 2:
                        ListAllAirportInfo();
                        break;
                    case 3:
                        FindNearestAirport();
                        break;
                    case 4:
                        StdDeviation();
                        break;
                    case 5:
                        LoggingOption();
                        break;
                    case 0:
                        WriteDataToFile();
                        Console.WriteLine("Data saved into the file");
                        return;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        private static void WriteDataToFile()
        {
            try
            {
                using (StreamWriter fileOutput = new StreamWriter(DataFileName))
                {
                    foreach (Airport airport in airportList)
                    {
                        fileOutput.WriteLine(airport.ToDataString());
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error Writng file: " + ex.Message);
            }

        }

        private static void LoggingOption()
        {
            Console.Write(
@"1. Logging to console
2. Logging to file
Enter your choices, comma-separated, empty for none: ");
            string choiceStr = Console.ReadLine();
            if (choiceStr.Equals("1"))
            {
                Airport.LogFailSet += LogToConsole;
                Console.WriteLine("Logging to console enabled");
            }
            if (choiceStr.Equals("2"))
            {
                Airport.LogFailSet += LogToFile;

                Console.WriteLine("Logging to File enabled");
            }
            if (choiceStr.Equals("1,2"))
            {
                Airport.LogFailSet += LogToConsole;
                Airport.LogFailSet += LogToFile;
     

                Console.WriteLine("Logging to console enabled");
                Console.WriteLine("Logging to File enabled");

            }
        }

        private static void LogToConsole(string reason)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss " + reason));

        }

        private static void LogToFile(string reason)
        {
            try
            {
                File.AppendAllText(@"..\..\event.log", DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss " + reason + "\n"));

            }catch(IOException exc)
            {
                Console.WriteLine("Error Writng file: " + exc.Message);

            }

        }


        static void StdDeviation()
        {
            double sum = 0;
            int count = 0;
            List<int> elevationList = new List<int>();
            foreach (Airport airport in airportList)
            {
                elevationList.Add(airport.ElevationMeters);
                sum += airport.ElevationMeters;
                count++;
            }
            if (count > 0)
            {
                double avg = sum / count;
                double sumOfSquares = 0;
                foreach (int elevation in elevationList)
                {
                    double squareOfDiff = (elevation - avg) * (elevation - avg);
                    sumOfSquares += squareOfDiff;
                }
                double stdDev = Math.Sqrt(sumOfSquares / elevationList.Count);
                Console.WriteLine("For all airports the standard deviation of their elevation is {0}", stdDev);
            }
            else
            {
                Console.WriteLine("Error: reading the related data");
            }

        }


        private static void FindNearestAirport()
        {
            Console.WriteLine("Enter the code of Airport");
            string airlineCode = Console.ReadLine();
            var airport = airportList.Find(ap => ap.Code.Equals(airlineCode));
            GeoCoordinate coordinate = new GeoCoordinate(airport.Latitude, airport.Longitude);

            Airport nearestAirport = (from ap in airportList
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

        private static void ListAllAirportInfo()
        {
            Console.WriteLine(String.Join("\n", airportList));
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
            if(!double.TryParse(latStr, out lat))
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
                Airport airport = new Airport(code, city, lat, longitude, elevation);
                airportList.Add(airport);

            }
            catch(InvalidDataException exc)
            {
                Airport.LogFailSet?.Invoke("Error : adding airport");
                Console.WriteLine("Erro" + exc.Message);
            }
        }

        private static int GetMenuChoice()
        {
            while (true)
            {
                Console.Write(
@"1. Add Airport
2. List all airports
3. Find nearest airport by code
4. Find airport's elevation standard deviation
5. Change log delegates
0. Exit
Enter your choice: ");
                string choiceStr = Console.ReadLine();
                int choice;
                if (!int.TryParse(choiceStr, out choice) || choice < 0 || choice > 5)
                {
                    Console.WriteLine("Your Choice must be a number between 0-5");
                    continue;
                }
                return choice;
            }

        }

        private static void ReadDataFromFile()
        {
            if (File.Exists(DataFileName))
            {
                try
                {
                    string[] linesArray = File.ReadAllLines(DataFileName);
                    foreach(string line in linesArray)
                    {
                        try
                        {
                            Airport airport = new Airport(line);
                            airportList.Add(airport);
                        }
                        catch(InvalidDataException exception)
                        {
                            Console.WriteLine("Error happended" + exception.Message);
                        }
                    }

                }catch(IOException exc)
                {
                    Console.WriteLine("Error in reading file" + exc.ToString());
                }
            }
        }
    }
}
