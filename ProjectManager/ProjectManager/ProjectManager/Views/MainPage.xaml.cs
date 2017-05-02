using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Collections.ObjectModel;
using ProjectManager.Models;
using ProjectManager.ViewModels;
using Xamarin.Forms.Xaml;

namespace ProjectManager.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            projStartDate.MinimumDate = DateTime.Now;
            projEndDate.MinimumDate = DateTime.Now;

            projectDatabase = new ProjectDatabaseViewModel();
            projects = new ObservableCollection<Project>(projectDatabase.LoadData());
            projectsList.ItemsSource = projects;
        }

        Project proj;
        ObservableCollection<Project> projects;
        private ProjectDatabaseViewModel projectDatabase;

        //Method for adding projects.
        private async void AddProject(object sender, EventArgs e)
        {
            if (CheckProjectSubmitForm())
            {
                //Setting new instance of project class.
                proj = new Project();
                proj.Name = projName.Text;
                proj.IsCompleted = 0;
                proj.StartDate = $"{projStartDate.Date.Day}.{projStartDate.Date.Month}.{projStartDate.Date.Year}";
                proj.EndDate = $"{projEndDate.Date.Day}.{projEndDate.Date.Month}.{projEndDate.Date.Year}";

                //Adding proj. instance to observable coll.             
                projects.Add(proj);
                DisableUI();
                ClearProjectForm();
                await projectDatabase.SaveItem(proj);
                await DisplayAlert("Add a project", "Project was successfully added.", "Ok");
                EnableUI();
            }
            else
            {
                await DisplayAlert("Error", "Fill form correctly...", "Ok");
            }
        }
        /// <summary>
        /// Method for disabling buttons and listview on page.
        /// </summary>
        private void DisableUI()
        {
            displayPopup.IsEnabled = false;
            closePopupButton.IsEnabled = false;
            submitProjectButton.IsEnabled = false;
        }
        /// <summary>
        /// Method for enabling buttons and listview on page.
        /// </summary>
        private void EnableUI()
        {
            displayPopup.IsEnabled = true;
            closePopupButton.IsEnabled = true;
            submitProjectButton.IsEnabled = true;
        }

        /// <summary>
        /// Method for validating form for submitting project.
        /// </summary>
        /// <returns></returns>
        private bool CheckProjectSubmitForm()
        {
            if (string.IsNullOrEmpty(projName.Text).Equals(false) && projEndDate != null && projStartDate != null && (projEndDate.Date.CompareTo(projStartDate.Date).Equals(1) || projEndDate.Date.CompareTo(projStartDate.Date).Equals(0)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Metod for clearing inputs in form for adding projects.
        /// </summary>
        private void ClearProjectForm()
        {
            addProjPopup.IsVisible = false;
            projName.Text = "";
            projStartDate.Date = DateTime.Now;
            projEndDate.Date = DateTime.Now;
            displayPopup.IsVisible = true;
            displayPopup.HeightRequest = 45;
        }

        //Display popup
        private void displayPopup_Clicked(object sender, EventArgs e)
        {
            addProjPopup.IsVisible = true;
            displayPopup.IsVisible = false;
            displayPopup.HeightRequest = 0;
        }
        //Close popup
        private void closePopupButton_Clicked(object sender, EventArgs e)
        {
            addProjPopup.IsVisible = false;
            displayPopup.IsVisible = true;
            displayPopup.HeightRequest = 45;
        }

        private void projectsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (projectsList.SelectedItem != null)
            {
                var tempObj = projectsList.SelectedItem as Project;
                Navigation.PushModalAsync(new ProjectPage(tempObj));
                projectsList.SelectedItem = null;
            }
        }

    }
}
