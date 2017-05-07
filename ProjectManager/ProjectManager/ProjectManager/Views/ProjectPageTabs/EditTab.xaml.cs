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
            ChangeUIToOS();
        }

        public EditTab(ProjTask taskData)
        {
            InitializeComponent();
            pageTitle.Text = "Edit task";
            projTask = taskData;
            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            taskEditUI();
            ChangeUIToOS();
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
            itemName.Text = $"{projTask.Name}";

            itemStartDate.Date = Convert.ToDateTime(projTask.StartDate);
            itemEndDate.Date = Convert.ToDateTime(projTask.EndDate);

            if (projTask.IsCompleted == 1)
            {
                itemStatus.IsToggled = true;
            }
            else
            {
                itemStatus.IsToggled = false;
            }
            backButton.IsVisible = true;
            pageTitleFrame.Margin = new Thickness(0,0,0,0);
        }

        /// <summary>
        /// Method for changing UI elements to editting specific item.
        /// </summary>
        private void projectEditUI()
        {
            nameEntryLabel.Text = "Project name:";
            itemName.Text = $"{proj.Name}";

            if (proj.IsCompleted == 1)
            {
                itemStatus.IsToggled = true;
            }
            else
            {
                itemStatus.IsToggled = false;
            }

            var temp1 = proj.StartDate.Split('.');
            var temp2 = proj.EndDate.Split('.');
            itemStartDate.Date = Convert.ToDateTime($"{temp1[1]}/{temp1[0]}/{temp1[2]}");
            itemEndDate.Date = Convert.ToDateTime($"{temp2[1]}/{temp2[0]}/{temp2[2]}");
        }

        /// <summary>
        /// Method for changing UI to device OS.
        /// </summary>
        private void ChangeUIToOS()
        {
            if (Device.OS.Equals(TargetPlatform.Android))
            {
                itemName.TextColor = Color.White;
                itemStartDate.TextColor = Color.White;
                itemEndDate.TextColor = Color.White;
            }
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            if (proj != null)
            {
                //Deleting of project.
                var res = await DisplayActionSheet("Remove project", "Cancel", null, "Delete project", "Keep project");
                if (res.Equals("Delete project"))
                {
                    MainPage.projects.Remove(proj);
                    await projectDatabase.DeleteItem(proj);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                //Deleting of task.
                var res = await DisplayActionSheet("Remove task", "Cancel", null, "Remove task", "Keep task");
                if (res.Equals("Remove task"))
                {
                    await taskDatabase.DeleteItem(projTask);
                    if (itemStatus.IsToggled)
                    {
                        FinishedTasksTab.tasks.Remove(projTask);
                    }
                    else
                    {
                        TaskTab.unfinishedTasks.Remove(projTask);
                    }
                    await Navigation.PopModalAsync();
                }
            }
        }

        private async void editButton_Clicked(object sender, EventArgs e)
        {
            if (proj != null)
            {
                if (FormValidation())
                {
                    proj.Name = itemName.Text;
                    proj.StartDate = $"{itemStartDate.Date.Day}.{itemStartDate.Date.Month}.{itemStartDate.Date.Year}";
                    proj.EndDate = $"{itemEndDate.Date.Day}.{itemEndDate.Date.Month}.{itemEndDate.Date.Year}";
                    if (itemStatus.IsToggled)
                    {
                        proj.IsCompleted = 1;
                    }
                    else
                    {
                        proj.IsCompleted = 0;
                    }
                    MainPage.projects.Remove(proj);
                    MainPage.projects.Add(proj);
                    await projectDatabase.SaveItem(proj);
                }
                else
                {
                    await DisplayAlert("Error", "Fill form correctly...", "Ok");
                }
            }
            else
            {
                if (FormValidation())
                {
                    projTask.Name = itemName.Text;
                    projTask.StartDate = $"{itemStartDate.Date.Day}.{itemStartDate.Date.Month}.{itemStartDate.Date.Year}";
                    projTask.EndDate = $"{itemEndDate.Date.Day}.{itemEndDate.Date.Month}.{itemEndDate.Date.Year}";
                    if (itemStatus.IsToggled)
                    {
                        projTask.IsCompleted = 1;
                        TaskTab.unfinishedTasks.Remove(projTask);
                        FinishedTasksTab.tasks.Add(projTask);
                    }
                    else
                    {
                        projTask.IsCompleted = 0;
                        TaskTab.unfinishedTasks.Add(projTask);
                        FinishedTasksTab.tasks.Remove(projTask);
                    }
                    await taskDatabase.SaveItem(projTask);
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Fill form correctly...", "Ok");
                }
            }
        }

        public async void deleteButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(itemName.Text).Equals(false) && itemStartDate != null && itemEndDate != null && (itemStartDate.Date.CompareTo(itemEndDate.Date).Equals(-1) || itemStartDate.Date.CompareTo(itemEndDate.Date).Equals(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
