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
    public partial class EditTab : ContentPage
    {
        public EditTab(Project projectData)
        {
            InitializeComponent();
            pageTitle.Text = "Edit project";
            proj = projectData;
            projectDatabase = new ViewModels.ProjectDatabaseViewModel();
            projectEditUI();
        }

        public EditTab(ProjTask taskData)
        {
            InitializeComponent();
            pageTitle.Text = "Edit task";
            projTask = taskData;
            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            taskEditUI();
        }

        private Project proj;
        private ProjTask projTask;
        private ViewModels.ProjectDatabaseViewModel projectDatabase;
        private ViewModels.ProjTaskDatabaseViewModel taskDatabase;

        /// <summary>
        /// Method for changing UI elements to editting specific item.
        /// </summary>
        private void taskEditUI()
        {
            nameEntryLabel.Text = "Task name:";
            itemName.Placeholder = "Task name...";

            itemStartDate.Date = Convert.ToDateTime(projTask.StartDate);
            itemEndDate.Date = Convert.ToDateTime(projTask.EndDate);

            statusLayout.IsVisible = true;
            if (projTask.IsCompleted == 1)
            {
                taskStatus.IsToggled = true;
            }
            else
            {
                taskStatus.IsToggled = false;
            }
        }

        /// <summary>
        /// Method for changing UI elements to editting specific item.
        /// </summary>
        private void projectEditUI()
        {
            nameEntryLabel.Text = "Project name:";
            itemName.Placeholder = "Project name...";

            itemStartDate.Date = Convert.ToDateTime(proj.StartDate);
            itemEndDate.Date = Convert.ToDateTime(proj.EndDate);
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            if (proj != null)
            {
                if (DisplayActionSheet("Remove project", "Cancel", null, "Delete project", "Keep project").IsCanceled.Equals("Delete project"))
                {
                    MainPage.projects.Remove(proj);
                    await projectDatabase.DeleteItem(proj);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                await taskDatabase.DeleteItem(projTask);
            }
        }
    }
}
