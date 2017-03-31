using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProjectManager.UWP;
using ProjectManager.Interfaces;
using Windows.Storage;
using System.IO;

[assembly: Dependency(typeof(FileHelper))]
namespace ProjectManager.UWP
{
    class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)     
        {                           
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename); 
        }
    }
}
