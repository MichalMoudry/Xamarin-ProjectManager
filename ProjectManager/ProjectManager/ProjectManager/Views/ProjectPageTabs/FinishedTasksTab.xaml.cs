using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ProjectManager.Views.ProjectPageTabs
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishedTasksTab : ContentPage
    {
        public FinishedTasksTab(Project projectData)
        {
            InitializeComponent();
            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            tasks = new ObservableCollection<ProjTask>(
                taskDatabase.GetProjectTasks(projectData.ID).OrderByDescending(projTask => projTask.StartDate)
            );
            taskList.ItemsSource = tasks;
        }

        public static ObservableCollection<ProjTask> tasks;
        private ProjTask projTask;
        private ViewModels.ProjTaskDatabaseViewModel taskDatabase { get; set; }

        private void taskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (taskList.SelectedItem != null)
            {
                var tempObj = taskList.SelectedItem as ProjTask;
                Navigation.PushModalAsync(new EditTab(tempObj));
                taskList.SelectedItem = null;
            }
        }
    }
}
