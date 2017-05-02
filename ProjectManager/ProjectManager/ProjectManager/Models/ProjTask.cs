using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    public class ProjTask : Interfaces.IDatabaseTemplate
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _projectID;
        public int ProjectID
        {
            get { return _projectID; }
            set { _projectID = value; }
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
            get { return _startDate; }
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
