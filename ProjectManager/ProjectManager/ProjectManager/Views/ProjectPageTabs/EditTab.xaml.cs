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
            projectDatabase = new ViewModels.ProjectDatabaseViewModel();
            projectEditUI();
        }

        public EditTab(ProjTask taskData)
        {
            InitializeComponent();
            pageTitle.Text = "Edit task";
            projTask = taskData;
            taskDatabase = new ViewModels.ProjTaskDatabaseViewModel();
            taskEditUI();
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
            itemName.Placeholder = "Task name...";

            itemStartDate.Date = Convert.ToDateTime(projTask.StartDate);
            itemEndDate.Date = Convert.ToDateTime(projTask.EndDate);
        }

        /// <summary>
        /// Method for changing UI elements to editting specific item.
        /// </summary>
        private void projectEditUI()
        {
            nameEntryLabel.Text = "Project name:";
            itemName.Placeholder = "Project name...";

            itemStartDate.Date = Convert.ToDateTime(proj.StartDate);
            itemEndDate.Date = Convert.ToDateTime(proj.EndDate);
        }

    }
}
