using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LeagueSelfEvolver.Model;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Navigation;

namespace LeagueSelfEvolver
{
    public sealed partial class TagCountStatPage : Page
    {        
        public TagCountStatPage()
        {
            this.InitializeComponent();            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter as GoalModel;
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            int GridBorderWidth = 6,
                ColumnBorderWidth = 2,
                columnBorderWidth = (tagCountListGrid.Columns.Count - 1) * ColumnBorderWidth,
                widthDelta = GridBorderWidth + columnBorderWidth;
            foreach (DataGridColumn column in tagCountListGrid.Columns)
            {
                column.Width = (tagCountListGrid.ActualWidth - widthDelta) / tagCountListGrid.Columns.Count;
            }
        }
    }  
}
