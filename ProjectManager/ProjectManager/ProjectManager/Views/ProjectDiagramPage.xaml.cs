using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ProjectManager.Models;
using ProjectManager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectDiagramPage : ContentPage
    {
        public ProjectDiagramPage(Project projectData)
        {
            InitializeComponent();
            tasks = new List<ProjTask>(
                ProjTaskDatabaseViewModel.Instance().GetUnfinishedTasks(projectData.ID).OrderBy(i => i.ID)
            );
            pageTitle.Text = $"{projectData.Name} - Calendar";
            calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(DateTime.Now) { BackgroundColor = Color.FromHex("#c6cdcf"), Selectable = true });
            
            //Adding colors to calendar.
            foreach (ProjTask item in tasks)
            {
                temp = item.StartDate.Split('.');
                calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])))
                    { BackgroundColor = Color.FromHex("#4db2ff"), Selectable = true }
                );
                temp = item.EndDate.Split('.');
                calendar.SpecialDates.Add(new XamForms.Controls.SpecialDate(new DateTime(Convert.ToInt32(temp[2]), Convert.ToInt32(temp[1]), Convert.ToInt32(temp[0])))
                    { BackgroundColor = Color.FromHex("#FFFF3838"), Selectable = true }
                );
            }
        }

        string[] temp;
        List<ProjTask> tasks;

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void calendar_DateClicked(object sender, XamForms.Controls.DateTimeEventArgs e)
        {
            if (calendar.SelectedDate != null)
            {
                var tempObj = calendar.SelectedDate;
                string events = "";
                foreach (ProjTask item in tasks.Where(i => i.StartDate == $"{tempObj.Value.Day}.{tempObj.Value.Month}.{tempObj.Value.Year}"))
                {
                    events += $" {item.IDinProject}. {item.Name}  - Start date\n";
                }
                foreach (ProjTask item in tasks.Where(i => i.EndDate == $"{tempObj.Value.Day}.{tempObj.Value.Month}.{tempObj.Value.Year}"))
                {
                    events += $" {item.IDinProject}. {item.Name}  - End date\n";
                }
                if (events.Equals("").Equals(false))
                {
                    DisplayAlert($"{tempObj.Value.Day}.{tempObj.Value.Month}.{tempObj.Value.Year}", events, "Cancel");
                }
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            calendar.SpecialDates.Clear();
        }
    }
}
