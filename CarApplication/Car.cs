using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication
{
    internal class Car
    {
        private string _make;
        private double _engine;
        private string _fuel;

        public string Make
        {
            get { return _make; }
            set { _make = value; }
        }

        public double Engine
        {
            get { return _engine; }
            set { _engine = value; }
        }
        public string Fuel
        {
            get { return _fuel; }
            set { _fuel = value; }
        }

        public Car(string make, double engine, string fuel)
        {
            Make = make;
            Engine = engine;
            Fuel = fuel;
        }

        public string ToDataString()
        {
            return $"{Make};{Engine};{Fuel}";
        }

        //public override string ToString()
        //{
        //    return string.Format("{1} model car with a {2} engine, fuel: {3}", this.Make, this.Engine, this.Fuel);
        //}
    }


}
