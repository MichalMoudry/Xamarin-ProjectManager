using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;   
using Xamarin.Forms;
using ProjectManager.Interfaces;
using ProjectManager.Models;

namespace ProjectManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new ProjectManager.MainPage();  
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }


        private static ProjectDatabase _projectDatabase; 
        public static ProjectDatabase ProjDatabase
        {
            get
            {
                if (_projectDatabase == null)
                {
                    _projectDatabase = new ProjectDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ProjectDatabase.db3"));
                }
                return _projectDatabase;
            }  
        }


    }
}
