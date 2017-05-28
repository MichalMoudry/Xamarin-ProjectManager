using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManager.ViewModels;
using ProjectManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcesPage : ContentPage
    {
        public ResourcesPage()
        {
            InitializeComponent();
            resources = new ObservableCollection<ProjectResource>(
                ProjectResourceDatabaseViewModel.Instance().LoadData().OrderByDescending(i => i.Type)    
            );
            resourceTypePicker.Items.Add("Material");
            resourceTypePicker.Items.Add("Work");
            resourceTypePicker.Items.Add("Cost");
            resourceList.ItemsSource = resources;

            ChangeUIToOS();
        }

        public static ObservableCollection<ProjectResource> resources;
        ProjectResource resource;

        private void displayPopup_Clicked(object sender, EventArgs e)
        {
            addResourcePopup.IsVisible = true;
            displayPopup.IsVisible = false;
            displayPopup.HeightRequest = 0;
        }

        private void closePopupButton_Clicked(object sender, EventArgs e)
        {
            addResourcePopup.IsVisible = false;
            displayPopup.IsVisible = true;
            displayPopup.HeightRequest = 45;
        }

        private async void submitResourceButton_Clicked(object sender, EventArgs e)
        {
            if (FormValidation())
            {
                resource = new ProjectResource();
                resource.Name = resourceName.Text;
                resource.Type = resourceTypePicker.SelectedItem as string;
                resource.Cost = Convert.ToInt32(resourceCost.Text);
                await ProjectResourceDatabaseViewModel.Instance().SaveItem(resource);
                resources.Add(resource);
                resourceName.Text = "";
                resourceTypePicker.SelectedIndex = -1;
                resourceCost.Text = "0";
                await DisplayAlert("Add a resource", "Resource was successfully added.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Fill form correctly...", "Ok");
            }
        }

        private bool FormValidation()
        {
            bool resourceCostCheck = int.TryParse(resourceCost.Text, out int num);
            if (string.IsNullOrEmpty(resourceName.Text).Equals(false) &&
                string.IsNullOrEmpty(resourceTypePicker.SelectedItem as string).Equals(false) &&
                string.IsNullOrEmpty(resourceCost.Text).Equals(false) &&
                resourceCostCheck.Equals(true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ChangeUIToOS()
        {
            if (Device.RuntimePlatform.Equals("Android"))
            {
                resourceName.TextColor = Color.White;
                resourceName.PlaceholderColor = Color.White;
                resourceCost.TextColor = Color.White;
                resourceCost.PlaceholderColor = Color.White;
                resourceTypePicker.Title = "Resource type";
                resourceTypePicker.TextColor = Color.White;
            }
        }

        private async void resourceList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (resourceList.SelectedItem != null)
            {
                var tempObj = resourceList.SelectedItem as ProjectResource;
                await Navigation.PushModalAsync(new ResourcePage(tempObj));
                resourceList.SelectedItem = null;
            }
        }
    }
}
