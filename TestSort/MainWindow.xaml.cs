using System.Collections.ObjectModel;
using System.Windows;

namespace TestSort
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Collect { get; set; }
        int focusInd = 1;
        public int FocusInd
        {
            get
            {
                return focusInd;
            }
            set
            {
                focusInd = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Collect = new ObservableCollection<string> {"Первый", "Второй", "Третий", "Четвертый" };
            MV.DataContext = this;
        }
    }
}
