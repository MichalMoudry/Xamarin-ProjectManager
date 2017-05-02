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
            tasks = new ObservableCollection<ProjTask>(taskDatabase.GetProjectTasks(projectData.ID));
            taskList.ItemsSource = tasks;
        }

        ObservableCollection<ProjTask> tasks;
        private ViewModels.ProjTaskDatabaseViewModel taskDatabase { get; set; }

        private void taskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (taskList.SelectedItem != null)
            {
                var tempObj = taskList.SelectedItem as ProjTask;
                //DisplayAlert("Task info", $"Name: {tempObj.Name}\nStart date: {tempObj.StartDate}\nEnd date: {tempObj.EndDate}\nIs completed: {tempObj.IsCompleted}", "Ok");
            }
        }
    }
}
