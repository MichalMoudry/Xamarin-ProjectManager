using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.ViewModels
{
    class ProjectResourceDatabaseViewModel : Interfaces.IDatabaseViewModel<ProjectResource>
    {
        private static ProjectResourceDatabaseViewModel _instance;
        public static ProjectResourceDatabaseViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ProjectResourceDatabaseViewModel();
            }
            return _instance;
        }

        private static ProjectResourceDatabase _taskResourceDB;
        private static ProjectResourceDatabase TaskResourceDB
        {
            get
            {
                if (_taskResourceDB == null)
                {
                    _taskResourceDB = new ProjectResourceDatabase(Xamarin.Forms.DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("TaskResources.db3"));
                }
                return _taskResourceDB;
            }
        }

        public async Task SaveItem(ProjectResource item)
        {
            await TaskResourceDB.SaveItemAsync(item);
        }

        public List<ProjectResource> LoadData()
        {
            return TaskResourceDB.GetItemsAsync<ProjectResource>().Result;
        }

        public async Task DeleteItem(ProjectResource item)
        {
            await TaskResourceDB.DeleteItemAsync(item);
        }

        public async Task<ProjectResource> GetItem(int id)
        {
            var temp = await TaskResourceDB.GetItemAsync<ProjectResource>(id);
            return temp;
        }

        public List<ProjectResource> GetItemsByID(int id)
        {
            return TaskResourceDB.GetItemsByID(id).Result;
        }

    }
}
