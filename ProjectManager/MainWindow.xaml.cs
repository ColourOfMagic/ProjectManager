using System.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Diagnostics;
using System.Linq;
using System.ComponentModel;
using System;

namespace ProjectManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
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
                projects = value;
                OnPropertyChanged("Projects");
            }
            
        }
        public MainWindow()
        {
            InitializeComponent();
            Projects = new ObservableCollection<Project>();
            Initialization();
            MainWind.DataContext = this;
        }

        void Initialization()
        {
            DirectoryInfo di = new DirectoryInfo("C:\\Users\\PC\\source\\repos");
            foreach (var item in di.GetDirectories())
            {
                if (IsProj(item.FullName))
                    Projects.Add(new Project(item.Name, item.FullName,LastData(item.FullName), IsProj(item.FullName)));
            }
            Projects = new ObservableCollection<Project>(Projects.OrderByDescending(i => i.AccessTime));
        }

        private DateTime LastData(string path)
        {
            var dif = new DirectoryInfo(path);
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

        private void DoubleClickCollect(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Project pr = (Project)((ListBox)sender).SelectedValue;
            MessageBox.Show(pr.Name+'\n'+pr.Path + '\n' +pr.IsProject.ToString()+'\n'+pr.AccessTime);
        }

        public bool IsProj(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (var item in directory.GetFiles())
            {
                if (item.Extension == ".sln") return true;
            }
            return false;
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            string path = Projects[LB.SelectedIndex].Path;
            Process.Start(path);
        }

        private void OpenDebugFolder(object sender, RoutedEventArgs e)
        {
            string path = Projects[LB.SelectedIndex].Path + "\\" + Projects[LB.SelectedIndex].Name + "\\bin\\Debug";
            if (new DirectoryInfo(path).Exists)
                Process.Start(path);
        }

        private void OpenReleaseFolder(object sender, RoutedEventArgs e)
        {
            string path = Projects[LB.SelectedIndex].Path + "\\" + Projects[LB.SelectedIndex].Name + "\\bin\\Release";
            if (new DirectoryInfo(path).Exists)
                Process.Start(path);
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            Process.Start(Projects[LB.SelectedIndex].Path);
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Projects.Clear();
            Initialization();
        }
        
        private void Namesort(object sender, RoutedEventArgs e)
        {
            Projects = new ObservableCollection<Project>(Projects.OrderBy(i => i.Name));
        }

        private void Datesort(object sender, RoutedEventArgs e)
        {
            Projects = new ObservableCollection<Project>(Projects.OrderByDescending(i => i.AccessTime));
        }

        int focusId;
        public int FocusId
        {
            get
            {
                return focusId;
            }
            set
            {
                focusId = value;
                OnPropertyChanged("FocusId");
            }
        }

        




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Chanj(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            for(int i=0; i<Projects.Count; i++)
            {
                if (projects[i].Name.Contains(tb.Text))
                {
                    LB.SelectedIndex = i;
                    LB.ScrollIntoView(projects[i]);
                    return;
                }
            }
        }
    }
}
