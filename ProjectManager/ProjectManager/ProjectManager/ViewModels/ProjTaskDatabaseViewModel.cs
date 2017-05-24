using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using Xamarin.Forms;

namespace ProjectManager.ViewModels
{
    public class ProjTaskDatabaseViewModel : Interfaces.IDatabaseViewModel<ProjTask>
    {
        //Singleton
        private static ProjTaskDatabaseViewModel _instance;
        public static ProjTaskDatabaseViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ProjTaskDatabaseViewModel();
            }
            return _instance;
        }

        protected ProjTaskDatabaseViewModel() { }

        private static TaskDatabase _taskDatabase;
        private static TaskDatabase TaskDb
        {
            get
            {
                if (_taskDatabase == null)
                {
                    _taskDatabase = new TaskDatabase(DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("TaskDatabase.db3"));
                }
                return _taskDatabase;
            }
        }

        public async Task SaveItem(ProjTask item)
        {
            await TaskDb.SaveItemAsync(item);
        }

        public List<ProjTask> LoadData()
        {
            return TaskDb.GetItemsAsync<ProjTask>().Result;
        }

        public async Task DeleteItem(ProjTask item)
        {
            await TaskDb.DeleteItemAsync(item);
        }

        public async Task<ProjTask> GetItem(int id)
        {
            var temp = await TaskDb.GetItemAsync<ProjTask>(id);
            return temp;
        }

        public List<ProjTask> GetProjectTasks(int projectID)
        {
            return TaskDb.GetCompletedTasksByID<ProjTask>(projectID).Result;
        }

        public List<ProjTask> GetUnfinishedTasks(int projectID)
        {
            return TaskDb.GetUnfinishedTasksByID<ProjTask>(projectID).Result;
        }

        public List<ProjTask> GetProjTasks(int projectID)
        {
            return TaskDb.GetProjectsTasks<ProjTask>(projectID).Result;
        }
    }
}
