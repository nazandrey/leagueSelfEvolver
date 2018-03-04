using LeagueSelfEvolver.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Telerik.UI.Xaml.Controls.Data;
using Telerik.UI.Xaml.Controls.Grid;
using Telerik.UI.Xaml.Controls.Grid.Primitives;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static LeagueSelfEvolver.MainPage;

namespace LeagueSelfEvolver.UserControls
{
    public sealed partial class EditForm : UserControl, IGridExternalEditor, INotifyPropertyChanged
    {
        private Event _editItem;
        private Event _source;

        public EditForm()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }

        public event EventHandler EditCancelled;
        public event EventHandler EditCommitted;

        public void BeginEdit(object item, RadDataGrid owner)
        {
            Event eventItem = item as Event;
            EditItem = eventItem;
            _source = new Event
            {
                GoalComment = eventItem.GoalComment,
                GeneralComment = eventItem.GeneralComment,
                GoalConclusions = CopyConclusions(eventItem.GoalConclusions),
                GeneralConclusions = CopyConclusions(eventItem.GeneralConclusions)
            };
        }

        public void CancelEdit()
        {
            EditItem.GoalComment = _source.GoalComment;
            EditItem.GeneralComment = _source.GeneralComment;
            EditItem.GoalConclusions = new ObservableCollection<Conclusion>(_source.GoalConclusions);
            EditItem.GeneralConclusions = new ObservableCollection<Conclusion>(_source.GeneralConclusions);
            EditCancelled(this, null);
        }

        public void CommitEdit()
        {            
            EditCommitted(this, null);
        }

        public ExternalEditorPosition Position
        {
            get; set;
        }        

        public Event EditItem
        {
            get { return _editItem; }
            set
            {
                _editItem = value;
                OnPropertyChanged("EditItem");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            CommitEdit();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CancelEdit();
        }

        private ObservableCollection<Conclusion> CopyConclusions(ObservableCollection<Conclusion> conclusions) {
            var result = new ObservableCollection<Conclusion>();
            foreach (var conclusion in conclusions)
            {
                result.Add(new Conclusion(conclusion.Tag, conclusion.Description));
            }
            return result;
        }
    }
}
