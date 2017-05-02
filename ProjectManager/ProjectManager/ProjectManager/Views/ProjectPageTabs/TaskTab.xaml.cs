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
    public partial class TaskTab : ContentPage
    {
        public TaskTab(Project projectData)
        {
            InitializeComponent();

            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            tasks = new ObservableCollection<ProjTask>(taskDatabase.GetProjectTasks(projectData.ID));
            taskList.ItemsSource = tasks;
            proj = projectData;

            taskStartDate.MinimumDate = DateTime.Now;
            taskEndDate.MinimumDate = DateTime.Now;
        }

        ObservableCollection<ProjTask> tasks;
        ProjTask projTask;
        Project proj;
        private ViewModels.ProjTaskDatabaseViewModel taskDatabase { get; set; }

        //Click methods.
        private void addTaskButton_Clicked(object sender, EventArgs e)
        {
            addTaskPopup.IsVisible = true;
            addTaskButton.IsVisible = false;
            addTaskButton.HeightRequest = 0;
        }
        private void closePopupButton_Clicked(object sender, EventArgs e)
        {
            addTaskPopup.IsVisible = false;
            addTaskButton.IsVisible = true;
            addTaskButton.HeightRequest = 45;
        }
        private async void submitTaskButton_Clicked(object sender, EventArgs e)
        {
            if (taskFormValidation())
            {
                projTask = new ProjTask();
                projTask.Name = taskName.Text;
                projTask.StartDate = $"{taskStartDate.Date.Day}.{taskStartDate.Date.Month}.{taskStartDate.Date.Year}";
                projTask.EndDate = $"{taskEndDate.Date.Day}.{taskEndDate.Date.Month}.{taskEndDate.Date.Year}";
                projTask.ProjectID = proj.ID;
                projTask.IsCompleted = false;
                tasks.Add(projTask);
                ClearForm();
                await taskDatabase.SaveItem(projTask);
                await DisplayAlert("Add a task", "Task was successfully added.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Fill form correctly...", "Ok");
            }
        }

        /// <summary>
        /// Method for checking form inputs.
        /// </summary>
        /// <returns>T if form is filled properly, F if  not.</returns>
        private bool taskFormValidation()
        {
            if (string.IsNullOrEmpty(taskName.Text).Equals(false) && taskStartDate != null && taskEndDate.Date != null && (taskEndDate.Date.CompareTo(taskStartDate.Date).Equals(1) || taskEndDate.Date.CompareTo(taskStartDate.Date).Equals(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method for clearing form for adding tasks.
        /// </summary>
        private void ClearForm()
        {
            taskName.Text = "";
            taskStartDate.Date = DateTime.Now;
            taskEndDate.Date = DateTime.Now;
        }

        private void taskList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (taskList.ItemSelected != null)
            {
                var tempObj = taskList.ItemSelected as ProjTask;
            }
        }
    }
}
