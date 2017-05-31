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

namespace ProjectManager.Views.ResourcePageTabs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTab : ContentPage
    {
        public EditTab(ProjectResource resourceData)
        {
            InitializeComponent();
            ChangeUIToOS();
            resourceNameEntry.Text = resourceData.Name;

            resourceTypePicker.Items.Add("Material");
            resourceTypePicker.Items.Add("Work");
            resourceTypePicker.Items.Add("Cost");
            resourceTypePicker.SelectedItem = resourceData.Type;
            resourceProjectEntry.Text = $"{resourceData.ProjectID}";
            resourceCostEntry.Text = $"{resourceData.Cost}";
            projResource = resourceData;
        }

        ProjectResource projResource;

        private void ChangeUIToOS()
        {
            if (Device.RuntimePlatform.Equals("Android"))
            {
                resourceNameEntry.TextColor = Color.White;
                resourceTypePicker.TextColor = Color.White;
                resourceTypePicker.Title = "Resource type";
                resourceProjectEntry.TextColor = Color.White;
                resourceCostEntry.TextColor = Color.White;
            }
        }

        private bool FormValidation()
        {
            bool numCheck = int.TryParse(resourceCostEntry.Text, out int num);
            if (string.IsNullOrEmpty(resourceNameEntry.Text).Equals(false) &&
                resourceTypePicker.SelectedItem != null &&
                string.IsNullOrEmpty(resourceProjectEntry.Text).Equals(false) &&
                string.IsNullOrEmpty(resourceCostEntry.Text).Equals(false) &&
                numCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private async void editButton_Clicked(object sender, EventArgs e)
        {
            if (FormValidation())
            {
                
                projResource.Name = resourceNameEntry.Text;
                projResource.Type = resourceTypePicker.SelectedItem as string;
                projResource.ProjectID = Convert.ToInt32(resourceProjectEntry.Text);
                projResource.Cost = Convert.ToInt32(resourceCostEntry.Text);
                await ViewModels.ProjectResourceDatabaseViewModel.Instance().SaveItem(projResource);
                await Navigation.PopModalAsync();
                ResourcesPage.resources.Remove(projResource);
                ResourcesPage.resources.Add(projResource);
            }
            else
            {
                await DisplayAlert("Error", "Fill form correctly...", "Ok");
            }
        }

        private async void deleteButton_Clicked(object sender, EventArgs e)
        {
            var res = await DisplayAlert("Delete resource","Do you want to delete this resource?", "Delete", "Cancel");
            if (res.Equals(true))
            {
                await ViewModels.ProjectResourceDatabaseViewModel.Instance().DeleteItem(projResource);
                ResourcesPage.resources.Remove(projResource);
                ProjectPageTabs.ResourcesPage.assignedResources.Remove(projResource);
                await Navigation.PopModalAsync();
            }
        }
    }
}
