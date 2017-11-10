using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LeagueSelfEvolver.Model;
using Windows.System;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LeagueSelfEvolver
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        GoalModel goalModel;

        public MainPage()
        {
            this.InitializeComponent();
            goalModel = new GoalModel();
            DataContext = goalModel;
        }

        private void Save_Xml(object sender, RoutedEventArgs e)
        {
            goalModel.SaveToXml();
        }

        private void Add_Row(object sender, RoutedEventArgs e)
        {
            goalModel.AddRow();
        }

        private void eventListGrid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Delete) {
                Event eventItem = eventListGrid.SelectedItem as Event;
                goalModel.RemoveRow(eventItem);
            };
        }
    }
}
