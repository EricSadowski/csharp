using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace air

    
{
    public class InvalidDataException : Exception
    {
        public InvalidDataException(string message) : base(message) { }
    }
     class Airport
    {
        public delegate void LoggerDelegate(string reason);
        public static LoggerDelegate LogFailSet;
        
		private string _code; //fields private
        private double _lat;
        private double _lng;
        private int _elevM;

        private string _city;

        public Airport(string code, double lat, double lng, int elevM, string city)
        {
            Code = code;
            Latitude = lat;
            Longitude = lng;
            ElevM = elevM;
            City = city;
        }


        public string Code
        {
            get { return _code; }
            set
            {
                string pattern = @"^[A-Z]{3}$";

                if (!Regex.IsMatch(value, pattern))
                {
                    LogFailSet?.Invoke("Code must be 3 uppercase letters");
                    throw new InvalidDataException("Code must be 3 uppercase letters");
                }

                _code = value;
            }
        }
        public double Latitude
        {
            get { return _lat; }
            set
            {
                if (value < -90 || value > 90)
                {
                    LogFailSet?.Invoke("Invalid Latitude");
                    throw new InvalidDataException("Invalid Latitude");
                }
                _lat = value;
            }
        }

        public double Longitude
        {
            get { return _lng; }
            set
            {
                if (value < -180 || value > 180)
                {
                    LogFailSet?.Invoke("Invalid Longitude");
                    throw new InvalidDataException("Invalid Longitude");
                }
                _lng = value;
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value.Length < 1 || value.Length > 50)
                {
                    LogFailSet?.Invoke("city must be between 1 and 50 chars");
                    throw new InvalidDataException("city must be between 1 and 50 chars");
                }
                _city = value;
            }
        }

        public int ElevM
        {
            get { return _elevM; }
            set
            {
                if (value < -1000 || value > 1000)
                {
                    LogFailSet?.Invoke("Invalid Elevation Meters");
                    throw new InvalidDataException("Invalid Elevation Meters");
                }
                _elevM = value;
            }
        }


        public override string ToString()
        {
            return $"Airport: Code={Code}, Latitude={Latitude}, Longitude={Longitude}, Elevation Meters={ElevM}, City={City}";
        }



    }
}
