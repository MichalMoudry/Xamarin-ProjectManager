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
            projectDatabase = ViewModels.ProjectDatabaseViewModel.Instance();
            projectEditUI();
            ChangeUIToOS();
        }

        public EditTab(ProjTask taskData)
        {
            InitializeComponent();
            pageTitle.Text = "Edit task";
            projTask = taskData;
            taskDatabase = ViewModels.ProjTaskDatabaseViewModel.Instance();
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
            backButton.IsVisible = true;

            if (projTask.IsCompleted == 1)
            {
                itemStatus.IsToggled = true;
            }
            else
            {
                itemStatus.IsToggled = false;
            }
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
        }

        /// <summary>
        /// Method for changing UI to device OS.
        /// </summary>
        private void ChangeUIToOS()
        {
            //Android UI.
            if (Device.RuntimePlatform.Equals("Android"))
            {
                itemName.TextColor = Color.White;
                itemStartDate.TextColor = Color.White;
                itemEndDate.TextColor = Color.White;
                //Project date
                if (proj != null)
                {
                    var temp1 = proj.StartDate.Split('.');
                    itemStartDate.Date = Convert.ToDateTime($"{temp1[0]}.{temp1[1]}.{temp1[2]}");
                    temp1 = proj.EndDate.Split('.');
                    itemEndDate.Date = Convert.ToDateTime($"{temp1[0]}.{temp1[1]}.{temp1[2]}");
                }
                //Task date
                else if (projTask != null)
                {
                    var temp1 = projTask.StartDate.Split('.');
                    itemStartDate.Date = Convert.ToDateTime($"{temp1[0]}.{temp1[1]}.{temp1[2]}");
                    temp1 = projTask.EndDate.Split('.');
                    itemEndDate.Date = Convert.ToDateTime($"{temp1[0]}.{temp1[1]}.{temp1[2]}");
                }
            }
            //Windows UI.
            else if(Device.RuntimePlatform.Equals("Windows"))
            {
                //Project date
                if (proj != null)
                {
                    var temp1 = proj.StartDate.Split('.');
                    itemStartDate.Date = Convert.ToDateTime($"{temp1[1]}.{temp1[0]}.{temp1[2]}");
                    temp1 = proj.EndDate.Split('.');
                    itemEndDate.Date = Convert.ToDateTime($"{temp1[1]}.{temp1[0]}.{temp1[2]}"); ;
                }
                //Task date
                else if (projTask != null)
                {
                    var temp1 = projTask.StartDate.Split('.');
                    itemStartDate.Date = Convert.ToDateTime($"{temp1[1]}.{temp1[0]}.{temp1[2]}");
                    temp1 = projTask.EndDate.Split('.');
                    itemEndDate.Date = Convert.ToDateTime($"{temp1[1]}.{temp1[0]}.{temp1[2]}");
                }
            }
        }

        /// <summary>
        /// Method for button to delete project or its task.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event arguments.</param>
        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            if (proj != null)
            {
                //Deleting project.
                bool res = await DisplayAlert("Delete project", "Do you want to delete this project?", "Delete", "Cancel");
                if (res.Equals(true))
                {
                    if (proj.IsCompleted == 0)
                    {
                        MainPage.projects.Remove(proj);
                    }
                    else
                    {
                        FinishedProjectsPage.finishedProjs.Remove(proj);
                    }
                    await projectDatabase.DeleteItem(proj);
                    await ViewModels.ProjTaskDatabaseViewModel.Instance().DeleteTasks(proj.ID);
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                //Deleting task.
                bool res = await DisplayAlert("Delete task", "Do you want to delete this task?", "Delete", "Cancel");
                if (res.Equals(true))
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
            //Editing project
            if (proj != null && projTask == null)
            {
                if (FormValidation())
                {
                    proj.Name = itemName.Text;
                    proj.StartDate = $"{itemStartDate.Date.Day}.{itemStartDate.Date.Month}.{itemStartDate.Date.Year}";
                    proj.EndDate = $"{itemEndDate.Date.Day}.{itemEndDate.Date.Month}.{itemEndDate.Date.Year}";
                    
                    if (itemStatus.IsToggled)
                    {
                        proj.IsCompleted = 1;
                        MainPage.projects.Remove(proj);
                        FinishedProjectsPage.finishedProjs.Remove(proj);
                        FinishedProjectsPage.finishedProjs.Add(proj);
                    }
                    else
                    {
                        proj.IsCompleted = 0;
                        MainPage.projects.Remove(proj);
                        MainPage.projects.Add(proj);
                        FinishedProjectsPage.finishedProjs.Remove(proj);
                    }
                    await DisplayAlert("Editing project", "Project was successfully edited.", "Ok");
                    await projectDatabase.SaveItem(proj);
                }
                else
                {
                    await DisplayAlert("Error", "Fill form correctly...", "Ok");
                }
            }
            //Editing task
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
                        if (TaskTab.unfinishedTasks.Contains(projTask))
                        {
                            FinishedTasksTab.tasks.Add(projTask);
                            TaskTab.unfinishedTasks.Remove(projTask);
                        }
                        else if (FinishedTasksTab.tasks.Contains(projTask))
                        {
                            FinishedTasksTab.tasks.Remove(projTask);
                            FinishedTasksTab.tasks.Add(projTask);
                        }
                    }
                    else
                    {
                        projTask.IsCompleted = 0;
                        if (FinishedTasksTab.tasks.Contains(projTask))
                        {
                            FinishedTasksTab.tasks.Remove(projTask);
                            TaskTab.unfinishedTasks.Add(projTask);
                        }
                        else if (TaskTab.unfinishedTasks.Contains(projTask))
                        {
                            TaskTab.unfinishedTasks.Remove(projTask);
                            TaskTab.unfinishedTasks.Add(projTask);
                        }
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

        /// <summary>
        /// Method for validating inputs in form.
        /// </summary>
        /// <returns>T if form is correct, F if not.</returns>
        private bool FormValidation()
        {
            if (string.IsNullOrEmpty(itemName.Text).Equals(false) && 
                itemStartDate != null && itemEndDate != null && 
                (itemStartDate.Date.CompareTo(itemEndDate.Date).Equals(-1) || itemStartDate.Date.CompareTo(itemEndDate.Date).Equals(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async void backButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        
    }
}
