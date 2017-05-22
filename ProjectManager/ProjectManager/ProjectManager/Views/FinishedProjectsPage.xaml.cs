using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectManager.Models;
using ProjectManager.ViewModels;

namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinishedProjectsPage : ContentPage
    {
        public FinishedProjectsPage()
        {
            InitializeComponent();
            //projectDatabase = projectDatabase.Instance;
            finishedProjs = new ObservableCollection<Project>(
                //projectDatabase.GetFinishedProjs().OrderByDescending(proj => proj.StartDate)   
            );
            finishedProjectsList.ItemsSource = finishedProjs;
        }

        Project proj;
        public static ObservableCollection<Project> finishedProjs;
        public ProjectDatabaseViewModel projectDatabase { get; set; }

        private async void finishedProjectsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (finishedProjectsList.SelectedItem != null)
            {
                var tempObj = finishedProjectsList.SelectedItem as Project;
                bool res = await DisplayAlert($"{tempObj.Name}", $"Start date: {tempObj.StartDate}\nEnd date: {tempObj.EndDate}\nIs completed: {Convert.ToBoolean(tempObj.IsCompleted)}", "Edit", "Cancel");
                if (res.Equals(true))
                {
                    await Navigation.PushModalAsync(new ProjectPage(tempObj));
                }
                finishedProjectsList.SelectedItem = null;
            }
        }
    }
}
