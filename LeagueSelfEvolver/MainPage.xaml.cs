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
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Core;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

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
            ScrollToLastRow();
        }

        private void Save_Xml(object sender, RoutedEventArgs e)
        {
            goalModel.SaveToXml();
        }

        private void Add_Row(object sender, RoutedEventArgs e)
        {
            goalModel.AddRow();
            ScrollToLastRow();
        }

        private void eventListGrid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            bool isEditing = e.OriginalSource.GetType().Name == "TextBox";
            if (e.Key == VirtualKey.Delete && !isEditing)
            {
                Event eventItem = eventListGrid.SelectedItem as Event;
                eventListGrid.CancelEdit();
                goalModel.RemoveRow(eventItem);
            };
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int GridBorderWidth = 6,
                ColumnBorderWidth = 2,
                columnBorderWidth = (eventListGrid.Columns.Count - 1) * ColumnBorderWidth,
                widthDelta = GridBorderWidth + columnBorderWidth;
            foreach (DataGridColumn column in eventListGrid.Columns)
            {
                column.Width = (eventListGrid.ActualWidth - widthDelta) / eventListGrid.Columns.Count;
            }
        }

        private void helpButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            helpPopup.IsOpen = !helpPopup.IsOpen;
        }

        private void helpPopup_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            helpContainer.Width = e.NewSize.Width;
        }

        private void Page_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            CoreVirtualKeyStates ctrlState = CoreWindow.GetForCurrentThread().GetKeyState(VirtualKey.Control);
            if (ctrlState == CoreVirtualKeyStates.Down && e.Key == VirtualKey.S)
            {
                goalModel.SaveToXml();
            }
        }

        private void ScrollToLastRow()
        {
            eventListGrid.ScrollItemIntoView(goalModel.EventList.Last());
        }

        private async void Show_Tag_Page(object sender, RoutedEventArgs e)
        {
            var viewId = 0;

            var newView = CoreApplication.CreateNewView();
            await newView.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    var frame = new Frame();
                    frame.Navigate(typeof(TagCountStatPage), goalModel);
                    Window.Current.Content = frame;

                    viewId = ApplicationView.GetForCurrentView().Id;
                    Window.Current.Activate();
                });

            var viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(viewId);
        }
    }  
}
