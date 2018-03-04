using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using LeagueSelfEvolver.Model;
using Windows.System;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Core;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

namespace LeagueSelfEvolver
{
    public sealed partial class MainPage : Page
    {
        GoalModel goalModel;

        public MainPage()
        {
            this.InitializeComponent();
            goalModel = new GoalModel();
            DataContext = goalModel;
            ScrollToLastRow(null, null);
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

        private void ScrollToLastRow(object sender, RoutedEventArgs e)
        {
            eventListGrid.ScrollItemIntoView(goalModel.EventList.Last());
        }

        private async void ShowTagPage(object sender, RoutedEventArgs e)
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
