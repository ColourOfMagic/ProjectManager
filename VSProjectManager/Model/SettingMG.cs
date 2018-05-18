using GalaSoft.MvvmLight;

namespace VSProjectManager.Model
{
    class SettingMG : ViewModelBase
    {
        string directoryPath;
        int scan;
        int sort;
        int topMost;
        bool isChanged;

        public string DirectoryPath
        {
            get => directoryPath;
            set
            {
                Set(ref directoryPath, value);
                IsChanged = true;
            }
        }
        public int Scan
        {
            get => scan;
            set
            {
                Set(ref scan, value);
                IsChanged = true;
            }
        }
        public int Sort
        {
            get => sort;
            set
            {
                Set(ref sort, value);
                IsChanged = true;
            }
        }
        public int TopMost
        {
            get => topMost;
            set
            {
                Set(ref topMost, value);
                IsChanged = true;
            }
        }
        public bool IsChanged
        {
            get => isChanged;
            set
            {
                Set(ref isChanged, value);
            }
        }


        public SettingMG(string directoryPath, int scan, int sort, int topmost, bool isChanged=false)
        {
            DirectoryPath = directoryPath;
            Scan = scan;
            Sort = sort;
            TopMost = topmost;
            IsChanged = isChanged;
        }
    }
}
