using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjectManager.Models;
using ProjectManager.Views.ResourcePageTabs;

namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcePage : TabbedPage
    {
        public ResourcePage(ProjectResource resourceData)
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#2c4659");
            BarTextColor = Color.White;
            Children.Add(new OverviewTab(resourceData));
            Children.Add(new EditTab(resourceData));
        }
    }
}
