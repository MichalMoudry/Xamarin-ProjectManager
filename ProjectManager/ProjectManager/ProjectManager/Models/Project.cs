using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    public class Project : Interfaces.IDatabaseTemplate
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _isCompleted;
        public int IsCompleted
        {
            get { return _isCompleted; }
            set { _isCompleted = value; }
        }

        private string _startDate;
        public string StartDate
        {
            get {
                var temp = _startDate.Split('.');
                return $"{temp[0]}.{temp[1]}.{temp[2]}";
            }
            set { _startDate = value; }
        }

        private string _endDate;
        public string EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

    }
}
