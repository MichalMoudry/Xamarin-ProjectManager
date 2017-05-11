using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ProjectManager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Projects : TabbedPage
    {
        public Projects()
        {
            InitializeComponent();
            BarBackgroundColor = Color.FromHex("#2c4659");
            Children.Add(new MainPage());
            Children.Add(new FinishedProjectsPage());
        }
    }
}
