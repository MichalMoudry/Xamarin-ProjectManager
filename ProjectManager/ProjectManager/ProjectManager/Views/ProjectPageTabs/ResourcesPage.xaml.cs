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
using ProjectManager.ViewModels;
using System.Collections.ObjectModel;

namespace ProjectManager.Views.ProjectPageTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcesPage : ContentPage
    {
        public ResourcesPage(Project projData)
        {
            InitializeComponent();
            pageTitle.Text = $"{projData.Name} - Resources";
            proj = projData;
            ChangeUIToOS();

            resourcePicker.ItemsSource = ProjectResourceDatabaseViewModel.Instance().GetItemsByID(0);
            assignedResources = new ObservableCollection<ProjectResource>(
                ProjectResourceDatabaseViewModel.Instance().GetItemsByID(projData.ID)
            );
            assignedResourceList.ItemsSource = assignedResources;
        }

        public static ObservableCollection<ProjectResource> assignedResources;
        Project proj;

        private void ChangeUIToOS()
        {
            if (Device.RuntimePlatform.Equals("Android"))
            {
                resourcePicker.TextColor = Color.White;
                resourcePicker.Title = "Project resource";
            }
        }

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void assignResourceButton_Clicked(object sender, EventArgs e)
        {
            if (resourcePicker.SelectedItem != null)
            {
                var selectedResource = resourcePicker.SelectedItem as ProjectResource;
                selectedResource.ProjectID = proj.ID;
                await ProjectResourceDatabaseViewModel.Instance().SaveItem(selectedResource);
                assignedResources.Add(selectedResource);
                resourcePicker.ItemsSource = ProjectResourceDatabaseViewModel.Instance().GetItemsByID(0);
                resourcePicker.SelectedIndex = -1;
                await DisplayAlert("Assigning a resource", "Resource was successfully assigned.", "Ok");
            }
            else
            {
                 await DisplayAlert("Error", "Pick a resource...", "Ok");
            }
        }

        private void closePopupButton_Clicked(object sender, EventArgs e)
        {
            assignResourcePopup.IsVisible = false;
            displayPopupButton.IsVisible = true;
            displayPopupButton.HeightRequest = 45;
        }

        private void displayPopupButton_Clicked(object sender, EventArgs e)
        {
            assignResourcePopup.IsVisible = true;
            displayPopupButton.IsVisible = false;
            displayPopupButton.HeightRequest = 0;
        }

        private async void assignedResourceList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (assignedResourceList.SelectedItem != null)
            {
                var tempObj = assignedResourceList.SelectedItem as ProjectResource;
                var result = await DisplayAlert("Edit resource", "What do you want with this resource?", "Edit", "Cancel");
                if (result.Equals(true))
                {
                    await Navigation.PushModalAsync(new ResourcePageTabs.EditTab(tempObj));
                }
                assignedResourceList.SelectedItem = null;
            }
        }
    }
}
