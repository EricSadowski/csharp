using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTravel
{
    internal class Trip
    {

        private string _destination;
        private string _name;
        private string _passport;
        private string _departure;
        private string _re_turn;

        public class InvalidDataException : Exception
        {
            public InvalidDataException(string message) : base(message) { }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Destination
        {
            get { return _destination; }
            set { _destination = value; }
        }

        public string Passport
        {
            get { return _passport; }
            set { _passport = value; }
        }

        public string Departure
        {
            get { return _departure; }
            set { _departure = value; }
        }

        public string Return
        {
            get { return _re_turn; }
            set { _re_turn = value; }
        }

        public Trip(string destination, string name, string passport, string departure, string re_turn)
        {
            Destination = destination;
            Name = name;
            Passport = passport;
            Departure = departure;
            Return = re_turn;
        }

        public string ToDataString()
        {
            return $"{Destination};{Name};{Passport};{Departure};{Return}";
        }


        public override string ToString()
        {
            return string.Format("{1} flying to {0} Passport:{2}, Departure: {3}, Return: {4}", this.Destination, this.Name, this.Passport, this.Departure, this.Return);
        }
    }
}
