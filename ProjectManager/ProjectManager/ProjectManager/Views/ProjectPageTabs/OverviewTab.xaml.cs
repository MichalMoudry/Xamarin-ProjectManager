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
    public partial class OverviewTab : ContentPage
    {
        public OverviewTab(Project projectData)
        {
            InitializeComponent();
            projectName.Text = projectData.Name;
            projData.Text = $"ID: {projectData.ID}\nStart date: {projectData.StartDate}\nEnd Date: {projectData.EndDate}";
        }

        private void backButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}
