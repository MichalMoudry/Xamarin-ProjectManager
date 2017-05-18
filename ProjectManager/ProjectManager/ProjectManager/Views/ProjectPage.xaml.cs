using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Views.ProjectPageTabs;
using ProjectManager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectPage : TabbedPage
    {
        public ProjectPage(Project projectData)
        {
            InitializeComponent();

            BarBackgroundColor = Color.FromHex("#2c4659");
            BarTextColor = Color.White;
            Children.Add(new OverviewTab(projectData));
            Children.Add(new TaskTab(projectData));
            Children.Add(new FinishedTasksTab(projectData));
            Children.Add(new EditTab(projectData));
        }
    }
}
