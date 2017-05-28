using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Interfaces
{
    public interface IDatabaseViewModel<T>
    {
        Task SaveItem(T item);
        List<T> LoadData();
        Task DeleteItem(T item);
    }
}
