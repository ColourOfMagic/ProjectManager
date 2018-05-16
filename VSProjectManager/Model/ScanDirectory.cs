using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace VSProjectManager.Model
{
    class ScanDirectory
    {
        public string reposPath;

        public ScanDirectory(string Path)
        {
            reposPath = Path;
        }

        public ObservableCollection<Solution> GetSolutions()  //добавить проекты из решения
        {
            var Projects = new ObservableCollection<Solution>();
            DirectoryInfo di = new DirectoryInfo(reposPath);
            foreach (var dir in di.GetDirectories())
            {
                foreach (var file in dir.GetFiles())
                {
                    if (file.Extension== ".sln")  //Можно подключить Microsoft.Buid, но так получилось эффективнее
                    {
                        Projects.Add(new Solution(dir.Name, dir.FullName, LastData(dir),GetProjects(dir)));
                        break;
                    }
                }
            }
            return Projects;
        }

        public List<Project> GetProjects(DirectoryInfo directory)
        {
            var Projects = new List<Project>();
            foreach (var dir in directory.GetDirectories())
            {
                foreach (var item in dir.GetFiles())
                {
                    if (item.Extension == ".csproj")
                    {
                        Projects.Add(new Project(item.FullName, item.Name.Substring(0, item.Name.Length - 7)));

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
