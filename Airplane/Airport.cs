using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Airplane
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
    class Airport
    {
        public delegate void LoggerDelegate(string reason);
        public static LoggerDelegate LogFailSet;

        public Airport(string code, string city, double lat, double lng, int elevM)
        {
            Code = code;
            City = city;
            Latitude = lat;
            Longitude = lng;
            ElevationMeters = elevM;
        }

        public Airport(string dataLine)
        {
            string[] data = dataLine.Split(';');
            if(data.Length != 5)
            {
                LogFailSet?.Invoke("Line has invalid number of fields");
                throw new InvalidDataException("Line has invalid number of fields");
            }
            Code = data[0];
            City = data[1];
            double lat;
            if (!double.TryParse(data[2], out lat))
            {
                LogFailSet?.Invoke("Latitude must be a number");
                throw new InvalidDataException("Latitude must be a number");
            }
            Latitude = lat;

            double lon;
            if (!double.TryParse(data[3], out lon))
            {
                LogFailSet?.Invoke("Longitude must be a number");
                throw new InvalidDataException("Longitude must be a number");
            }
            Longitude = lon;

            int elev;
            if (!int.TryParse(data[4], out elev))
            {
                LogFailSet?.Invoke("ElevationMeters  must be a number");
                throw new InvalidDataException("ElevationMeters  must be a number");
            }
            ElevationMeters = elev;
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                //https://regex101.com/
                string pattern = @"^[A-Z]{3}$";
                Regex rg = new Regex(pattern);
                if (!rg.IsMatch(value))
                {
                    LogFailSet?.Invoke("Code must be exactly 3 uppercase letters");
                    throw new InvalidDataException("Code must be exactly 3 uppercase letters");
                }
                _code = value;
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                //https://regex101.com/
                string pattern = @"^[A-Za-z 0-9,\-.]{1,50}$";
                Regex rg = new Regex(pattern);
                if (!rg.IsMatch(value))
                {
                    LogFailSet?.Invoke("Code must be exactly 3 uppercase letters");
                    throw new InvalidDataException("City must be 1-50 characters, match the pattern");
                }
                _city = value;
            }
        }

        private double _latitude;
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (value < -90 || value > 90)
                {
                    LogFailSet?.Invoke("Code must be exactly 3 uppercase letters");
                    throw new InvalidDataException("Latitude must be between -90 and 90");
                }
                _latitude = value;
            }

        }
        private double _longitude;
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (value < -180 || value > 180)
                {
                    LogFailSet?.Invoke("Code must be exactly 3 uppercase letters");
                    throw new InvalidDataException("Longitude must be between -180 and 180");
                }
                _longitude = value;
            }

        }




        private int _elevationMeters;
        public int ElevationMeters
        {
            get { return _elevationMeters; }
            set
            {
                if (value < -1000 || value > 1000)
                {
                    LogFailSet?.Invoke("Code must be exactly 3 uppercase letters");
                    throw new InvalidDataException("ElevationMeters must be between -180 and 180");
                }
                _elevationMeters = value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0} in {1} at {2} lat / {3} lng at {4}m eleveation", Code, City, Latitude, Longitude, ElevationMeters);
        }

        public string ToDataString()
        {
            return string.Format("{0};{1};{2};{3};{4}", Code, City, Latitude, Longitude, ElevationMeters);
        }
    }
}