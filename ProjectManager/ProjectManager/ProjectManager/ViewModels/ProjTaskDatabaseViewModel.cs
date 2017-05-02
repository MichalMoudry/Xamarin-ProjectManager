using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using Xamarin.Forms;

namespace ProjectManager.ViewModels
{
    class ProjTaskDatabaseViewModel : Interfaces.IDatabaseViewModel<ProjTask>
    {
        private static Database<ProjTask> _taskDatabase;
        private static Database<ProjTask> TaskDatabase
        {
            get
            {
                if (_taskDatabase == null)
                {
                    _taskDatabase = new Database<ProjTask>(DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("TaskDatabase.db3"));
                }
                return _taskDatabase;
            }
        }

        public async Task SaveItem(ProjTask item)
        {
            await TaskDatabase.SaveItemAsync(item);
        }

        public List<ProjTask> LoadData()
        {
            return TaskDatabase.GetItemsAsync<ProjTask>().Result;
        }

        public async Task DeleteItem(ProjTask item)
        {
            await TaskDatabase.DeleteItemAsync(item);
        }

        public async Task<ProjTask> GetItem(int id)
        {
            var temp = await TaskDatabase.GetItemAsync<ProjTask>(id);
            return temp;
        }

        public List<ProjTask> GetProjectTasks(int projectID)
        {
            return TaskDatabase.GetCompletedTasksByID<ProjTask>(projectID).Result;
        }

        public List<ProjTask> GetUnfinishedTasks(int projectID)
        {
            return TaskDatabase.GetUnfinishedTasksByID<ProjTask>(projectID).Result;
        }
    }
}
