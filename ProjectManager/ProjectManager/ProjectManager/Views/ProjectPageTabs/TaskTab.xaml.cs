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
    public partial class TaskTab : ContentPage
    {
        public TaskTab(Project projectData)
        {
            InitializeComponent();
        }

        private void addTaskButton_Clicked(object sender, EventArgs e)
        {
            addTaskPopup.IsVisible = true;
        }
    }
}
