using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ProjectManager.Interfaces
{
    public interface IDatabaseTemplate
    {
        [PrimaryKey, AutoIncrement, Indexed]
        int ID
        {
            get;
        }
        string Name
        {
            get; set;
        }
    }
}
