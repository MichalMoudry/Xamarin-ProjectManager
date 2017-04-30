using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;
using Xamarin.Forms;
using ProjectManager.Interfaces;

namespace ProjectManager.ViewModels
{
    public class ProjectDatabaseViewModel : Interfaces.IDatabaseViewModel<Project>
    {
        private static Database<Project> _projectDatabase;
        private static Database<Project> ProjectDatabase
        {
            get
            {
                if (_projectDatabase != null)
                {
                    _projectDatabase = new Database<Project>(DependencyService.Get<IFileHelper>().GetLocalFilePath("ProjectDatabase.db3"));
                }
                return _projectDatabase;
            }
        }

        public async Task SaveItem(Project item)
        {
            await ProjectDatabase.SaveItemAsync(item);
        }

        public List<Project> LoadData()
        {
            return ProjectDatabase.GetItemsAsync<Project>().Result;
        }

        public async Task DeleteItem(Project item)
        {
            await ProjectDatabase.DeleteItemAsync(item);
        }

        public async Task<Project> GetItem(int id)
        {
            var temp = await ProjectDatabase.GetItemAsync<Project>(id);
            return temp;
        }
    }
}
