using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Models
{
    public class ProjectResource : Interfaces.IDatabaseTemplate
    {
        [PrimaryKey, AutoIncrement]
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

        private string _type;
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private int _cost;
        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }

    }
}
