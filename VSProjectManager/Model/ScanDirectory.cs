using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Construction;

namespace VSProjectManager.Model
{
    class ScanDirectory
    {
        public string reposPath;

        public ScanDirectory(string Path)
        {
            reposPath = Path;
        }

        public ObservableCollection<Project> GetSolutions()
        {
            var Projects = new ObservableCollection<Project>();
            DirectoryInfo di = new DirectoryInfo(reposPath);
            foreach (var dir in di.GetDirectories())
            {
                foreach (var file in dir.GetFiles())
                {
                    if (file.Extension== ".sln")
                    {
                        Projects.Add(new Project(dir.Name, dir.FullName, LastData(dir)));
                    }
                }
            }
            return Projects;
        }

        private DateTime LastData(DirectoryInfo dif)  //+ Добавить упрощенное время
        {
            DateTime MaxLast = dif.LastWriteTime;
            foreach (var item in dif.GetFiles("*", SearchOption.AllDirectories))
            {
                if (MaxLast < item.LastWriteTime)
                {
                    MaxLast = item.LastWriteTime;
                }
            }

            return MaxLast;
        }

    }
}
