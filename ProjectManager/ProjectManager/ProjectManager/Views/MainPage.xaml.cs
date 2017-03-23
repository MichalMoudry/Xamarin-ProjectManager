using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Collections.ObjectModel;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            addProjPopup.IsVisible = false;
            projStartDate.MaximumDate = DateTime.Now;
        }

        Project proj;

        private async void AddProject(object sender, EventArgs e)
        {
            if (CheckProjectSubmitForm())
            {
                proj = new Project();
                proj.Name = projName.Text;
                proj.StartDate = projStartDate.Date;
                proj.EndDate = projEndDate.Date;

                await App.ProjDatabase.SaveItemAsync(proj);
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
            if (string.IsNullOrEmpty(projName.Text).Equals(false) && projEndDate != null && projStartDate != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Display popup
        private void displayPopup_Clicked(object sender, EventArgs e)
        {
            addProjPopup.IsVisible = true;
        }
        //Close popup
        private void closePopupButton_Clicked(object sender, EventArgs e)
        {
            addProjPopup.IsVisible = false;
        }
    }
}
