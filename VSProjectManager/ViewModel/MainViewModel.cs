using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VSProjectManager.Model;

namespace VSProjectManager.ViewModel
{
    class MainViewModel : ViewModelBase
    {
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
        }

    }
}
