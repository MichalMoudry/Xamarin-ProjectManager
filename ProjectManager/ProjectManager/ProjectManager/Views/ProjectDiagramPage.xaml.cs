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
            projectDataDisplay.Text = $"Project: {projectData.Name}";
            projectTasks = ProjTaskDatabaseViewModel.Instance().GetProjTasks(projectData.ID);

            foreach (ProjTask item in projectTasks)
            {
                //Adding items to diagram.
                /*diagram.Children.Add(new Label {
                    TextColor = Color.White,
                    FontSize = 16,
                    Text = $"{item.Name}"
                });*/
            }
        }
        private List<ProjTask> projectTasks = new List<ProjTask>();
    }
}
