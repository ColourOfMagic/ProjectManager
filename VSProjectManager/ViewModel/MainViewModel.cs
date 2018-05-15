using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VSProjectManager.Model;
using Microsoft.Build.Construction;
using System.Windows.Input;

namespace VSProjectManager.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        ScanDirectory scanner;
        string path;   //Продумать движуху с настройками и всяким таким

        ObservableCollection<Project> projects;
        public ObservableCollection<Project> Projects
        {
            get
            {
                return projects;
            }
            set
            {
                // OnPropertyChanged("Projects");  //заменить потом аналогом через MVVM light
                Set(ref projects, value);
            }

        }

        

        public MainViewModel()
        {
            Projects = new ObservableCollection<Project>
            {
                new Project("Dir1","dd//rr.tct",DateTime.Now,true),
                new Project("Dir2","ht//rk.tat",DateTime.Now,true)
            };
            path = "C:\\Users\\PC\\source\\repos";

            scanner = new ScanDirectory(path);
            Projects = scanner.GetSolutions();
        }


        RelayCommand<string> sort;
        public ICommand Sort
        {
            get
            {
                if (sort == null)
                {
                    sort = new RelayCommand<string>(SortProjects);
                }
                return sort;
            }
        }

        private void SortProjects(string obj)
        {
            if (obj== "По имени")
            {
                Projects = new ObservableCollection<Project>(Projects.OrderBy(i => i.Name));
            } else Projects = new ObservableCollection<Project>(Projects.OrderByDescending(i => i.AccessTime));
        }
    }
}
