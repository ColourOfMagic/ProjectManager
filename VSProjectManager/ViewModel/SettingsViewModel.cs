using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Forms;
using System.Windows.Input;
using VSProjectManager.Model;

namespace VSProjectManager.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {
        string directoryPath;
        public string DirectoryPath
        {
            get
            {
                return directoryPath;
            }

            set
            {
                Set(ref directoryPath, value);
                IsChanged = true;
            }
        }

        bool isChanged;
        public bool IsChanged
        {
            get
            {
                return isChanged;
            }

            set
            {
                Set(ref isChanged, value);
            }
        }

        public SettingsViewModel()
        {
            MessengerInstance.Register<SettingMG>(this, ProcessingMG);
            
        }

        private void ProcessingMG(SettingMG obj)
        {
            DirectoryPath = obj.DirectoryPath;
            IsChanged = false;
        }

        RelayCommand selectPath;
        public ICommand SelectPath
        {
            get
            {
                if (selectPath == null)
                {
                    selectPath = new RelayCommand(SelectPathActivity);
                }
                return selectPath;
            }
        }

        private void SelectPathActivity()
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                DirectoryPath = fd.SelectedPath;
            }
        }
    }
}
