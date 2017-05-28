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
                ProjTaskDatabaseViewModel.Instance().GetProjTasks(projectData.ID).OrderBy(i => i.ID)
            );
            projectDataDisplay.Text = $"Project: {projectData.Name}";

            ColumnDefinition column = new ColumnDefinition
            {
                Width = GridLength.Auto
            };
            diagram.ColumnDefinitions.Add(column);
            for (int i = 0; i < ProjTaskDatabaseViewModel.Instance().GetProjTasks(projectData.ID).Count; i++)
            {
                //Adding row and column definitions.
                RowDefinition row = new RowDefinition
                {
                    Height = GridLength.Auto
                };
                
                diagram.RowDefinitions.Add(row);

                //Creating label for task.
                Label label = new Label();
                label.Text = $"{tasks[i].IDinProject}.{tasks[i].Name}\nStart date: {tasks[i].StartDate} • End date: {tasks[i].EndDate} • Is Completed: {Convert.ToBoolean(tasks[i].IsCompleted)}";
                label.TextColor = Color.White;
                label.FontSize = 14;
                label.HeightRequest = 55;
                label.VerticalTextAlignment = TextAlignment.Center;
                label.Margin = new Thickness(10 + (i * 20), 0, 10, 0);
                label.BackgroundColor = Color.FromHex("#6088a6");

                Grid.SetRow(label, i);
                Grid.SetColumn(label, 0);
                //Adding labels to grid.
                diagram.Children.Add(label);
            }
        }

        List<ProjTask> tasks;

        private async void backButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
