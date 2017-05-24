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

namespace ProjectManager.Views.ProjectPageTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OverviewTab : ContentPage
    {
        public OverviewTab(Project projectData)
        {
            InitializeComponent();
            pageTitle.Text = projectData.Name;
            taskDatabase = ViewModels.ProjTaskDatabaseViewModel.Instance();
            finishedTasks = taskDatabase.GetProjectTasks(projectData.ID).Count;
            unfinishedTasks = taskDatabase.GetUnfinishedTasks(projectData.ID).Count;

            projectDataDisplay.Text = String.Format("Start date: {0}\nEnd date: {1}\nUnfinished tasks: {2}\nFinished tasks: {3}",
                projectData.StartDate,
                projectData.EndDate,
                unfinishedTasks,
                finishedTasks
                );

            if (unfinishedTasks != 0 || finishedTasks != 0)
            {
                projectProgress.ProgressTo(finishedTasks / unfinishedTasks, 250, Easing.Linear);
            }
        }

        ViewModels.ProjTaskDatabaseViewModel taskDatabase;
        int finishedTasks;
        int unfinishedTasks;

        public async void backButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
