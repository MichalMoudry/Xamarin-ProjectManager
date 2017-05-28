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
            proj = projectData;
            pageTitle.Text = projectData.Name;
            taskDatabase = ViewModels.ProjTaskDatabaseViewModel.Instance();
            finishedTasks = taskDatabase.GetProjectTasks(projectData.ID).Count;
            unfinishedTasks = taskDatabase.GetUnfinishedTasks(projectData.ID).Count;

            //Counting cost of project.
            projResourcesPerMonth = new List<ProjectResource>(
                ViewModels.ProjectResourceDatabaseViewModel.Instance().GetItemsByID(projectData.ID).Where(i => i.Type.Equals("Work"))
            );
            projOneTimeResourses = new List<ProjectResource>(
                ViewModels.ProjectResourceDatabaseViewModel.Instance().GetItemsByID(projectData.ID).Where(i => i.Type.Equals("Work").Equals(false))
            );
            foreach (ProjectResource item in projResourcesPerMonth)
            {
                costPerMonth += item.Cost;
            }
            foreach (ProjectResource item in projOneTimeResourses)
            {
                oneTimeCost += item.Cost;
            }

            projectDataDisplay.Text = String.Format("Start date: {0}\nEnd date: {1}\nUnfinished tasks: {2}\nFinished tasks: {3}\nCost per month: {4}\nOne time cost: {5}",
                projectData.StartDate,
                projectData.EndDate,
                unfinishedTasks,
                finishedTasks,
                costPerMonth,
                oneTimeCost
                );
            
        }

        ViewModels.ProjTaskDatabaseViewModel taskDatabase;
        Project proj;
        int finishedTasks;
        int unfinishedTasks;
        int costPerMonth;
        int oneTimeCost;
        List<ProjectResource> projResourcesPerMonth;
        List<ProjectResource> projOneTimeResourses;

        public async void backButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void viewDiagramButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ProjectDiagramPage(proj));
        }

        private async void resourcesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ResourcesPage(proj));
        }
    }
}
