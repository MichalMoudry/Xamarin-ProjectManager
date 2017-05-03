using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    class ProjectTaskConnectTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private int _projectID;
        public int ProjectID
        {
            get { return _projectID; }
            set { _projectID = value; }
        }

        private int _taskID;
        public int TaskID
        {
            get { return _taskID; }
            set { _taskID = value; }
        }

    }
}
