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
            return ProjectDb.GetItemsAsync<Project>().Result;
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
