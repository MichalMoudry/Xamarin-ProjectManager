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
using System.Diagnostics;

namespace ProjectManager.Views.ProjectPageTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewTab : ContentPage
    {
        public OverviewTab(Project projectData)
        {
            InitializeComponent();
            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            finishedTasks = taskDatabase.GetProjectTasks(projectData.ID).Count;
            unfinishedTasks = taskDatabase.GetUnfinishedTasks(projectData.ID).Count;
            //projectState.Progress = Convert.ToDouble((unfinishedTasks + finishedTasks) / 100);

            projectName.Text = projectData.Name;
            projData.Text = $"Start date: {projectData.StartDate}\nEnd Date: {projectData.EndDate}\nUnfinished tasks: {unfinishedTasks}\nFinished tasks: {finishedTasks}\nProgress: ";
        }

        private ViewModels.ProjTaskDatabaseViewModel taskDatabase { get; set; }
        public int unfinishedTasks { get; set; }
        public int finishedTasks { get; set; }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
            //DisplayAlert("Test", $"{finishedTasks * ((unfinishedTasks + finishedTasks) / 2)}","Ok");
        }
    }
}
