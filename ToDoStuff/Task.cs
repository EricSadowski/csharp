using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ToDoStuff
{
    public class Task
    {
        private string _name;
        private int _difficulty;
        private string _date;
        private string _status;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Difficulty
        {
            get { return _difficulty; }
            set { _difficulty = value; }
        }

        public string Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Task(string name, int difficulty, string date, string status)
        {
            Name = name;
            Difficulty = difficulty;
            Date = date;
            Status = status;
        }

        public string ToDataString()
        {
            return $"{Name};{Difficulty};{Date};{Status}";
        }


        public override string ToString()
        {
            return string.Format("{0} by {1}/{2}, {3}", this.Name, this.Date, this.Difficulty, this.Status);
        }
    }
}
