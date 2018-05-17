using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using VSProjectManager.Model;
using System.Windows.Input;
using System.Diagnostics;
using System.IO;
using VSProjectManager.View;

namespace VSProjectManager.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        ScanDirectory scanner;
        string path;   //Продумать движуху с настройками и всяким таким
        ObservableCollection<Solution> solutions;
        Solution currentProject;
        Project selectProject;
        int focusId;

        public ObservableCollection<Solution> Solutions
        {
            get
            {
                return solutions;
            }
            set
            {
                Set(ref solutions, value);
            }

        }

        public Solution CurrentProject
        {
            get { return currentProject; }
            set
            {
                Set(ref currentProject, value);
            }
        }

        public int FocusId   //не лучшее решение, потом переделать
        {
            get { return focusId; }
            set
            {
                Set(ref focusId, value);
            }
        }

        public Project SelectProject
        {
            get { return selectProject; }
            set
            {
                Set(ref selectProject, value);
            }
        }

        public MainViewModel()
        {
            path = "C:\\Users\\PC\\source\\repos";
            scanner = new ScanDirectory(path);
            Solutions = scanner.GetSolutions();
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
                Solutions = new ObservableCollection<Solution>(Solutions.OrderBy(i => i.Name));
            }
            else Solutions = new ObservableCollection<Solution>(Solutions.OrderByDescending(i => i.AccessTime));
        }

        RelayCommand openFolder;
        public ICommand OpenFolder
        {
            get
            {
                if (openFolder == null)
                {
                    openFolder = new RelayCommand(
                        () => Process.Start(SelectProject.DirectoryPath),
                        () => SelectProject != null && Directory.Exists(SelectProject.DirectoryPath));
                }
                return openFolder;
            }
        }

        RelayCommand openDebugFolder;
        public ICommand OpenDebugFolder
        {
            get
            {
                if (openDebugFolder == null)
                {
                    openDebugFolder = new RelayCommand(
                        () => Process.Start(SelectProject.DebugPath),
                        () => SelectProject != null && Directory.Exists(SelectProject.DebugPath));
                }
                return openDebugFolder;
            }
        }

        RelayCommand openReleaseFolder;
        public ICommand OpenReleaseFolder
        {
            get
            {
                if (openReleaseFolder == null)
                {
                    openReleaseFolder = new RelayCommand(
                        () => Process.Start(SelectProject.ReleasePath),
                        () => SelectProject != null && Directory.Exists(SelectProject.ReleasePath));
                }
                return openReleaseFolder;
            }
        }

        RelayCommand startProject;
        public ICommand StartProject
        {
            get
            {
                if (startProject == null)
                {
                    startProject = new RelayCommand(() => Process.Start(SelectProject.Path),() => SelectProject != null);
                }
                return startProject;
            }
        }

        RelayCommand update;
        public ICommand Update
        {
            get
            {
                if (update == null)
                {
                    update = new RelayCommand(UpdateCollection);  //Добавить отключение кнопок если не выбран проект
                }
                return update;
            }
        }

        private void UpdateCollection()
        {
            Solutions = scanner.GetSolutions();
        }

        RelayCommand openSettings;
        public ICommand OpenSettings
        {
            get
            {
                if (openSettings == null)
                {
                    openSettings = new RelayCommand(OpenSettingsWindow);
                }
                return openSettings;
            }
        }

        private void OpenSettingsWindow()  //Догадываюсь другое окно в MVVM открывается не так
        {
            new SettingsWindow().Show();
            MessengerInstance.Send<SettingMG>(new SettingMG(path));
        }

        #endregion
    }
}
