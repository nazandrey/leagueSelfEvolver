using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LeagueSelfEvolver.ViewModel;

namespace LeagueSelfEvolver
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GoalViewModel goalViewModel;

        public MainWindow()
        {
            InitializeComponent();
            goalViewModel = new GoalViewModel();
            DataContext = goalViewModel;
        }

        private void Save_Xml(object sender, EventArgs e)
        {
            goalViewModel.SaveToXml();
        }
    }
}
