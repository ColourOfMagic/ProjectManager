using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using VSProjectManager.Model;

namespace VSProjectManager.ViewModel
{
    class SettingsViewModel : ViewModelBase
    {

        SettingMG settings;
        public SettingMG Settings
        {
            get
            {
                return settings;
            }

            set
            {
                Set(ref settings, value);
            }
        }

        public SettingsViewModel()
        {
            MessengerInstance.Register<SettingMG>(this, ProcessingMG);
        }

        private void ProcessingMG(SettingMG obj)
        {
            Settings = obj;
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
                Settings.DirectoryPath = fd.SelectedPath;
            }
        }

        RelayCommand sendSetting;
        public ICommand SendSetting
        {
            get
            {
                if (sendSetting == null)
                {
                    sendSetting = new RelayCommand(SendMessage,()=>(Settings!=null && Settings.IsChanged));
                }
                return sendSetting;
            }
        }
        
        private void SendMessage()
        {
            MessengerInstance.Send<SettingMG>(Settings);
            Settings.IsChanged = false;
        }

        RelayCommand saveSettings;
        public ICommand SaveSettings
        {
            get
            {
                if (saveSettings == null)
                {
                    saveSettings = new RelayCommand(()=>SettingsManager.SetSettings(Settings));
                }
                return saveSettings;
            }
        }
    }
}
