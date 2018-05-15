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
using System.Windows;
using System.Diagnostics;

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

        Project currentProject;
        public Project CurrentProject
        {
            get { return currentProject; }
            set
            {
                Set(ref currentProject, value);
            }
        }

        int focusId;
        public int FocusId   //не лучшее решение, потом переделать
        {
            get { return focusId; }
            set
            {
                Set(ref focusId, value);
            }
        }


        public MainViewModel()
        {
            path = "C:\\Users\\PC\\source\\repos";
            scanner = new ScanDirectory(path);
            Projects = scanner.GetSolutions();
            SortProjects("По дате");
        }

        #region Command

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
            if (obj == "По имени")
            {
                Projects = new ObservableCollection<Project>(Projects.OrderBy(i => i.Name));
            }
            else Projects = new ObservableCollection<Project>(Projects.OrderByDescending(i => i.AccessTime));
        }

        RelayCommand<string> openFolder;
        public ICommand OpenFolder
        {
            get
            {
                if (openFolder == null)
                {
                    openFolder = new RelayCommand<string>(OpenFolderAction);  //Добавить отключение кнопок если не выбран проект
                }
                return openFolder;
            }
        }

        private void OpenFolderAction(string obj)
        {
            if (obj == null)
                MessageBox.Show("Выберите проект");
            foreach (var item in Projects[focusId].NestedProjects)
            {
                if (item.ProjectName == obj)  //изза того что в этой коллекции лежат не только файлы но и папки приходится колхозить
                {
                    string Fpath = item.AbsolutePath;
                    for (int i = Fpath.Length - 7; i > 0; i--)
                    {
                        if (Fpath[i] == '\\')
                        {
                            Fpath = Fpath.Substring(0, i);
                            Process.Start(Fpath);
                            break;
                        }
                    }

                }
            }
        }


        #endregion
    }
}
