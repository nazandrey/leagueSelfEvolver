using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LeagueSelfEvolver.Model;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LeagueSelfEvolver
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
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
