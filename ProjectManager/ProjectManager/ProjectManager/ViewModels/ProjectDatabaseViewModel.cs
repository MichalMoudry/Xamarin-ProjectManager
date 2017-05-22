using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using Xamarin.Forms;

namespace ProjectManager.ViewModels
{
    public class ProjectDatabaseViewModel : Interfaces.IDatabaseViewModel<Project>
    {
        //Singleton
        private static ProjectDatabaseViewModel _instance;
        public static ProjectDatabaseViewModel Instance()
        {
            if (_instance == null)
            {
                _instance = new ProjectDatabaseViewModel();
            }
            return _instance;
        }

        protected ProjectDatabaseViewModel() { }

        private static ProjectDatabase _projectDatabase;
        private static ProjectDatabase ProjectDb
        {
            get
            {
                if (_projectDatabase == null)
                {
                    _projectDatabase = new ProjectDatabase(DependencyService.Get<Interfaces.IFileHelper>().GetLocalFilePath("ProjectDatabase.db3"));
                }
                return _projectDatabase;
            }
        }

        private Project proj = new Project();

        public async Task SaveItem(Project item)
        {
            await ProjectDb.SaveItemAsync(item);
        }

        public List<Project> LoadData()
        {
            return ProjectDb.GetUnfinishedProjects<Project>().Result;
        }

        public List<Project> GetFinishedProjs()
        {
            return ProjectDb.GetFinishedProjects<Project>().Result;
        }

        public async Task DeleteItem(Project item)
        {
            await ProjectDb.DeleteItemAsync(item);
        }

        public async Task<Project> GetItem(int id)
        {
            var temp = await ProjectDb.GetItemAsync<Project>(id);
            return temp;
        }
    }
}
