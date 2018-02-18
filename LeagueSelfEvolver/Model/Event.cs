using LeagueSelfEvolver.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LeagueSelfEvolver.Model
{
    public class Event : INotifyPropertyChanged
    {
        private string _goalComment;
        private string _generalComment;

        private ObservableCollection<Conclusion> _goalConclusions;
        private ObservableCollection<Conclusion> _generalConclusions;

        public Event() : this("", "", new ObservableCollection<Conclusion>(), new ObservableCollection<Conclusion>()) { }
        public Event(XElement eventEl) : this(
            eventEl.Element("GoalComment").Value, 
            eventEl.Element("GeneralComment").Value, 
            ConclusionDictExtensions.InitConclusions(eventEl, "GoalConclusions"), 
            ConclusionDictExtensions.InitConclusions(eventEl, "GeneralConclusions")
            ) { }

        public Event(string goalComment, string generalComment, ObservableCollection<Conclusion> goalConclusions, ObservableCollection<Conclusion> generalConclusions)
        {
            _goalComment = goalComment;
            _generalComment = generalComment;
            _goalConclusions = goalConclusions;
            _generalConclusions = generalConclusions;
            AddGoalConclusionCommand = new RelayCommand(() => AddGoalConclusion());
            AddGeneralConclusionCommand = new RelayCommand(() => AddGeneralConclusion());
            DeleteGoalConclusionCommand = new RelayCommand<Conclusion>((conclusion) => DeleteGoalConclusion(conclusion));
            DeleteGeneralConclusionCommand = new RelayCommand<Conclusion>((conclusion) => DeleteGeneralConclusion(conclusion));
        }             
        
        public ObservableCollection<Conclusion> GoalConclusions {
            get { return _goalConclusions; }
            set { _goalConclusions = value; }
        }

        public ObservableCollection<Conclusion> GeneralConclusions
        {
            get { return _generalConclusions; }
            set { _generalConclusions = value; }
        }

        public string GoalComment
        {
            get { return _goalComment; }
            set
            {
                _goalComment = value;
                OnPropertyChanged("GoalComment");
            }
        }

        public string GeneralComment
        {
            get { return _generalComment; }
            set
            {
                _generalComment = value;
                OnPropertyChanged("GeneralComment");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public RelayCommand AddGoalConclusionCommand { get; private set; }
        public RelayCommand AddGeneralConclusionCommand { get; private set; }
        public RelayCommand<Conclusion> DeleteGoalConclusionCommand { get; private set; }
        public RelayCommand<Conclusion> DeleteGeneralConclusionCommand { get; private set; }

        private void DeleteGoalConclusion(Conclusion item)
        {
            _goalConclusions.Remove(item);
        }

        private void DeleteGeneralConclusion(Conclusion item)
        {
            _generalConclusions.Remove(item);
        }

        private void AddGoalConclusion()
        {
            _goalConclusions.Add(new Conclusion());
        }

        private void AddGeneralConclusion()
        {
            _generalConclusions.Add(new Conclusion());
        }

        public XElement ToXml()
        {
            return new XElement("Event",
                new XElement("GoalComment", GoalComment),
                new XElement("GeneralComment", GeneralComment),
                new XElement("GoalConclusions", _goalConclusions.ToXml()),
                new XElement("GeneralConclusions", _generalConclusions.ToXml())
            );
        }
    }
}
