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
    public partial class OverviewTab : ContentPage
    {
        public OverviewTab(ProjectResource resourceData)
        {
            InitializeComponent();
            pageTitle.Text = resourceData.Name;
            resourceDataDisplay.Text = $"Name: {resourceData.Name}\nType: {resourceData.Type}\nCost: {resourceData.Cost}\nAssigned to project: {resourceData.ProjectID}";
        }

        public async void backButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
