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
            //projectState.Progress = Convert.ToDouble((unfinishedTasks + finishedTasks) / 100);

            projectName.Text = projectData.Name;
            var temp = Convert.ToDateTime(projectData.StartDate);
            var temp2 = Convert.ToDateTime(projectData.EndDate);
            projData.Text = $"Start date: {temp.Day}.{temp.Month}.{temp.Year}\nEnd Date: {temp.Day}.{temp.Month}.{temp.Year}\nFinished tasks: {finishedTasks}\nUnfinished tasks: {unfinishedTasks}";
        }

        private ViewModels.ProjTaskDatabaseViewModel taskDatabase { get; set; }
        public int unfinishedTasks { get; private set; }
        public int finishedTasks { get; private set; }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
