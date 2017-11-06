using System;
using System.Collections.Generic;
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

namespace LeagueSelfEvolver.Controls
{
    /// <summary>
    /// Логика взаимодействия для EditTextBox.xaml
    /// </summary>
    public partial class EditTextBox : UserControl
    {
        public EditTextBox()
        {
            InitializeComponent();

            editTextBox.Focusable = false;
            editTextBox.Cursor = Cursors.Arrow;
        }

        #region Methods
        private void editTextBox_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            editTextBox.Cursor = editTextBox.Focusable ? Cursors.IBeam : Cursors.Arrow;
        }

        private void editTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            editTextBox.Focusable = true;
            editTextBox.Focus();
            editTextBox.CaretIndex = editTextBox.Text.Length;
        }

        private void editTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            editTextBox.Focusable = false;
        }

        private void editTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            editTextBox.Focusable = true;
            editTextBox.Focus();
            editTextBox.CaretIndex = editTextBox.Text.Length;
        }

        private void editTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (editTextBox != null)
            {
                if (e.Key == Key.Return)
                {
                    if (e.Key == Key.Enter)
                    {
                        Keyboard.ClearFocus();
                        editTextBox.Focusable = false;
                    }
                }
            }
        }
        #endregion

        #region Value DP
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(EditTextBox), new PropertyMetadata(null));
        #endregion
    }
}
